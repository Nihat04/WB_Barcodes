using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using WBBarcodes.Api;
using WBBarcodes.Classes;
using WBBarcodes.Properties;
using WildBerries_Barcodes.Scripts.JsonClasses;

namespace WildBerries_Barcodes
{
    public partial class MainPage : Form
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private bool Expand = false;

        private void FolderOpenButton_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Environment.CurrentDirectory);
        }

        private async void ImportExcelButton_Click(object sender, EventArgs e)
        {
            var excelPath = FilesManager.ChooseFile();
            if (excelPath == "Error") return;

            var excelRows = Excel.ReadFile(excelPath);

            progressBar1.Value = 0;
            progressBar1.Maximum = excelRows.Length;
            var progress = new Progress<int>(value =>
            {
                if (value == -1)
                    progressBar1.Value = 0;
                else
                    progressBar1.Increment(value);
            });

            var panel = FormsManager.ClonePanel(ImagePanel);

            await Task.Run(() => FilesManager.GenerateWbFiles(excelRows, progress, panel));

            File.Delete(excelPath);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DropDownTimer.Start();
            //var names = new List<string>() 
            //{
            //    "Fullstar 2000000003122",
            //    "Cracpot 2000000003894",
            //    "Supership 2000000004617",
            //    "Starking 2000000002606",
            //    "LONN 2000000004969",
            //    "NENS 2000000012650",
            //    "Kristar 2000000008684"
            //};

            //var pdf = new PDF();

            //foreach (var name in names)
            //{
            //    var splitName = name.Split(' ');
            //    pdf.AddPage(splitName[0], splitName[1]);
            //}

            //pdf.Save("mtsEvotor");
        }

        private void TokenButton_Click(object sender, EventArgs e)
        {
            Form form = new TokenPage();
            DialogResult dialogResult = form.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                var controls = form.Controls;
                var textBox = controls.Find("TokenBox", true)[0].Text;
                WildBerriesAPI.Token = textBox;
            }
        }

        private void InformationButton_Click(object sender, EventArgs e)
        {
            Form form = new InformationPage();
            DialogResult dialogResult = form.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                UpdateImageInfo();
            }
        }

        private void FormLoad(object sender, EventArgs e)
        {
            UpdateImageInfo();
            Scripts.TagSize.Change(ImagePanel);
        }

        private void UpdateImageInfo()
        {
            if (!File.Exists("info.json"))
            {
                var info = new Info() { About = About.Text, Brand = Brand.Text };
                var jsonText = JsonSerializer.Serialize(info);
                File.WriteAllText("info.json", jsonText);
            }

            using (var jsonFile = File.OpenRead("info.json"))
            {
                var info = JsonSerializer.Deserialize<Info>(jsonFile);
                About.Text = info.About;
                Brand.Text = info.Brand;
            }
        }

        private void ImagePanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void ExcelOzonButton_Click(object sender, EventArgs e)
        {
            var excelPath = FilesManager.ChooseFile();
            if (excelPath == "Error") return;

            var excelRows = Excel.ReadFile(excelPath);

            progressBar1.Value = 0;
            progressBar1.Maximum = excelRows.Length;
            var progress = new Progress<int>(value =>
            {
                if (value == -1)
                    progressBar1.Value = 0;
                else
                    progressBar1.Increment(value);
            });

            await Task.Run(() => FilesManager.GenerateOzonFiles(excelRows, progress));

            File.Delete(excelPath);
        }

        private void CombinePdfButton_Click(object sender, EventArgs e)
        {
            const string folderName = "Combined_files";

            if (!Directory.Exists(folderName))
                Directory.CreateDirectory(folderName);

            using (OpenFileDialog dialog = new() { Filter = "PDF File (*.pdf)|*.pdf", ValidateNames = true, Multiselect = true })
            {
                if (!(dialog.ShowDialog() == DialogResult.OK))
                    return;

                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                var pdf = new PdfDocument();

                for(int i = 0; i < dialog.FileNames.Length; i++)
                {
                    var file = dialog.FileNames[i];
                    var fileName = Path.GetFileName(file).Split('.')[0];
                    var readPdf = PdfReader.Open(file, PdfDocumentOpenMode.Import);
                    for(int j = 0; j < readPdf.PageCount; j++)
                    {
                        var page = readPdf.Pages[j];
                        var newPage = pdf.AddPage(page);

                        using(XGraphics gfx = XGraphics.FromPdfPage(newPage))
                        {
                            var font = new XFont("Arial", 10);
                            gfx.DrawString(fileName, font, XBrushes.Black, new XRect(page.Width / 2 - 5, page.Height - 10, 20, 0), XStringFormats.Center);
                        }
                    }

                }

                var finalFileName = DateTime.Now.ToString("dd.MM.yyyy_HH-mm");
                pdf.Save($"{folderName}\\{finalFileName}.pdf");
            }
        }
    }
}