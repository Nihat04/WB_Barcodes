using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.Content;
using PdfSharp.Pdf.IO;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using WBBarcodes.Api;
using WBBarcodes.Classes;
using WBBarcodes.Properties;
using WildBerries_Barcodes.Scripts;
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
            var excelPath = FilesManager.ChooseExcelFile();
            if (excelPath == "Error") return;

            var excelRows = Excel.GetProductRows(excelPath);

            progressBar1.Value = 0;
            progressBar1.Maximum = excelRows.Length;
            var progress = new Progress<int>(value =>
            {
                if (value == -1)
                    progressBar1.Value = 0;
                else
                    progressBar1.Increment(value);
            });

            var iProgress = progress as IProgress<int>;

            var panel = FormsManager.ClonePanel(ImagePanel);

            await Task.Run(() => FilesManager.CreateWbFiles(excelRows, panel, () => iProgress.Report(1)));

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
            //Scripts.TagSize.Change(ImagePanel);
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
                About.Text = info?.About;
                Brand.Text = info?.Brand;
            }
        }

        private void ImagePanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void ExcelOzonButton_Click(object sender, EventArgs e)
        {
            var excelPath = FilesManager.ChooseExcelFile();
            if (excelPath == "Error") return;

            var excelRows = Excel.ReadFile(excelPath);

            progressBar1.Value = 0;
            progressBar1.Maximum = excelRows.Length * 2;
            var progress = new Progress<int>(value =>
            {
                if (value == -1)
                    progressBar1.Value = 0;
                else
                    progressBar1.Increment(value);
            });

            var panel = FormsManager.ClonePanel(ImagePanel);

            await Task.Run(() =>
            {
                List<string> articuls = Ozon.GetProductsArticuls(excelRows);
                var products = OzonApi.getProducts(articuls.ToArray());
                FilesManager.GenerateOzonFiles(excelRows, progress, panel, products);
            });

            File.Delete(excelPath);
        }

        private void CombinePdfButton_Click(object sender, EventArgs e)
        {
            const string folderName = "Combined_files";

            if (!Directory.Exists(folderName))
                Directory.CreateDirectory(folderName);

            var files = FilesManager.ChooselPdfFiles();

            if (files == null) return;

            var pdf = new PdfDocument();

            for (int i = 0; i < files.Length; i++)
            {
                var file = files[i];
                var fileName = Path.GetFileName(file).Split('.')[0];
                var pdfDoc = PdfReader.Open(file, PdfDocumentOpenMode.Import);
                for (int j = 0; j < pdfDoc.PageCount; j++)
                {
                    var page = pdfDoc.Pages[j];
                    var newPage = pdf.AddPage(page);

                    var tagName = fileName;
                    var content = ContentReader.ReadContent(page);

                    using (XGraphics gfx = XGraphics.FromPdfPage(newPage))
                    {
                        var font = new XFont("Arial", 8);
                        gfx.DrawString(tagName, font, XBrushes.Black, 5, page.Height - 30, XStringFormats.Default);
                    }
                }

            }

            var finalFileName = DateTime.Now.ToString("yyyy.MM.dd_HH-mm");
            pdf.Save($"{folderName}\\{finalFileName}.pdf");

            Process.Start("explorer.exe", folderName);
        }
    }
}