using System;
using System.Data;
using System.Diagnostics;
using System.Text.Json;
using System.Windows.Forms;
using WBBarcodes.Properties;
using WBBarcodes.Scripts;
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
            Process.Start("explorer.exe", $@"PDF's folder");
        }

        private async void ImportExcelButton_Click(object sender, EventArgs e)
        {
            var excelPath = ExcelReader.ChooseFile();
            if (excelPath == "Error") return;

            var excelRows = ExcelReader.ReadFile(excelPath);

            progressBar1.Value = 0;
            progressBar1.Maximum = excelRows.Length;
            var progress = new Progress<int>(value =>
            {
                progressBar1.Increment(value);
            });

            await Task.Run(() => GenerateFiles(excelRows, progress));

            File.Delete(excelPath);
        }

        private void GenerateFiles(DataRow[] excelRows, IProgress<int> progress)
        {
            var pdf = new PDF();
            var template = new ExcelTemplate();
            var shkTemplate = new ExcelTemplate(true);

            var panel = ClonePanel(ImagePanel);

            foreach (var row in excelRows)
            {
                progress.Report(1);

                var tag = ExcelReader.GetTagFromRow(row);

                if (tag == null) continue;
                if (tag.Error) return;

                if (tag.Data[0] == null)
                {
                    MessageBox.Show("Some Error", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Scripts.TagSize.RenderPanel(panel, tag);
                pdf.AddPage(panel, tag.Data[0].Count);
                template.AddColumn(tag.Data[0].Sizes[0].Barcode[0], tag.Data[0].Count);
                shkTemplate.AddColumn(tag.Data[0].Sizes[0].Barcode[0], tag.Data[0].Count, tag.Data[0].CartboxNumber);
            }

            Save(template, shkTemplate, pdf);
        }

        private Panel ClonePanel(Panel panel)
        {
            var clonedPanel = new Panel();

            clonedPanel.Size = panel.Size;
            clonedPanel.BackColor = System.Drawing.Color.White;
            clonedPanel.BorderStyle = panel.BorderStyle;

            var panelControls = panel.Controls;

            foreach (Control control in panelControls)
            {
                if (control.GetType().Name == "Label")
                {
                    var controlAsLabel = control as Label;
                    var clonedControl = new Label();

                    clonedControl.Text = controlAsLabel.Text;
                    clonedControl.Font = controlAsLabel.Font;
                    clonedControl.Name = controlAsLabel.Name;
                    clonedControl.BackColor = controlAsLabel.BackColor;
                    clonedControl.TextAlign = controlAsLabel.TextAlign;

                    clonedControl.AutoSize = controlAsLabel.AutoSize;

                    clonedPanel.Controls.Add(clonedControl);
                }
                else if (control.GetType().Name == "PictureBox")
                {
                    var controlAsPictureBox = control as PictureBox;
                    var clonedControl = new PictureBox();

                    clonedControl.Size = controlAsPictureBox.Size;
                    clonedControl.MaximumSize = controlAsPictureBox.MaximumSize;
                    clonedControl.Name = controlAsPictureBox.Name;
                    clonedControl.BackColor = controlAsPictureBox.BackColor;
                    clonedControl.Location = controlAsPictureBox.Location;

                    if(clonedControl.Name == "EAC")
                    {
                        clonedControl.SizeMode = PictureBoxSizeMode.StretchImage;
                        clonedControl.Image = Resources.EAC;
                    }

                    clonedPanel.Controls.Add(clonedControl);
                }
            }

            TagSize.Change(clonedPanel);
            return clonedPanel;
        }

        private void Save(ExcelTemplate template1, ExcelTemplate template2, PDF pdf)
        {
            const string folderPath = @"PDF's folder";

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileFolderName = DateTime.Now.ToString("dd.MM.yyyy_HH-mm");
            Directory.CreateDirectory(Path.Combine(folderPath, fileFolderName));

            string fileFolder = Path.Combine(folderPath, fileFolderName);

            pdf.Save(fileFolder);
            template1.Save(fileFolder);
            template2.Save(fileFolder);

            Process.Start("explorer.exe", fileFolder);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DropDownTimer.Start();
        }

        private void Size58x40_Click(object sender, EventArgs e)
        {
            ImagePanel.Size = ImagePanel.MaximumSize;
            Scripts.TagSize.Change(ImagePanel);
            DropDownTimer.Start();
        }

        private void Size58x30_Click(object sender, EventArgs e)
        {
            ImagePanel.Size = ImagePanel.MinimumSize;
            Scripts.TagSize.Change(ImagePanel);
            DropDownTimer.Start();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (Expand)
            {
                TicketSizeContainer.Height -= 10;
                if (TicketSizeContainer.Height <= TicketSizeContainer.MinimumSize.Height)
                {
                    DropDownTimer.Stop();
                    Expand = false;
                }
            }
            else
            {
                TicketSizeContainer.Height += 10;
                if (TicketSizeContainer.Height >= TicketSizeContainer.MaximumSize.Height)
                {
                    DropDownTimer.Stop();
                    Expand = true;
                }
            }
        }

        private void TokenButton_Click(object sender, EventArgs e)
        {
            Form form = new TokenPage();
            DialogResult dialogResult = form.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                var controls = form.Controls;
                var textBox = controls.Find("TokenBox", true)[0].Text;
                RestAPI.Token = textBox;
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
    }
}