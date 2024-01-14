using System.Data;
using System.Diagnostics;
using System.Text.Json;
using System.Windows.Forms;
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
            Process.Start("explorer.exe", $@"PDF's folder");
        }

        private async void ImportExcelButton_Click(object sender, EventArgs e)
        {
            var filePath = Excel.ChooseFile();
            var excelRows = Excel.ReadFile(filePath);

            PDF.CreateNew();

            progressBar1.Value = 0;
            progressBar1.Maximum = excelRows.Length;
            var progress = new Progress<int>(value =>
            {
                progressBar1.Increment(value);
            });

            await Task.Run( () =>
            {
                var panel = ClonePanel(ImagePanel);

                var progressAsIProgress = progress as IProgress<int>;

                foreach (var row in excelRows)
                {
                    progressAsIProgress.Report(1);

                    var tag = Excel.GetTagFromRow(row);

                    if (tag == null) continue;

                    if (tag.Data[0] == null)
                    {
                        MessageBox.Show("Some Error", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    Scripts.TagSize.RenderPanel(panel, tag);
                    Excel.AddColumn(tag.Data[0].Sizes[0].Barcode[0], tag.Data[0].Count, tag.Data[0].CartboxNumber);
                }

            });

            PDF.Save();
            File.Delete(filePath);
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
                    //clonedControl.Size = controlAsLabel.Size;
                    clonedControl.Name = controlAsLabel.Name;
                    clonedControl.BackColor = controlAsLabel.BackColor;
                    clonedControl.TextAlign = controlAsLabel.TextAlign;

                    //clonedControl.UseMnemonic = controlAsLabel.UseMnemonic;
                    clonedControl.AutoSize = controlAsLabel.AutoSize;

                    clonedPanel.Controls.Add(clonedControl);
                }
                else if (control.GetType().Name == "PictureBox")
                {
                    var controlAsPictureBox = control as PictureBox;
                    var clonedControl = new PictureBox();

                    clonedControl.Size = controlAsPictureBox.Size;
                    clonedControl.Name = controlAsPictureBox.Name;
                    clonedControl.BackColor = controlAsPictureBox.BackColor;
                    clonedControl.Location = controlAsPictureBox.Location;

                    if(controlAsPictureBox.Name == "EAC")
                    {
                        clonedControl.Image = Resources.EAC;
                    }

                    clonedPanel.Controls.Add(clonedControl);
                }
            }

            TagSize.Change(clonedPanel);
            return clonedPanel;
        }

        //private void ImportExcelButton_Click(object sender, EventArgs e)
        //{
        //    var path = Excel.ChooseFile();
        //    var rows = Excel.ReadFile(path);
        //    PDF.CreateNew();

        //    foreach (var row in rows)
        //    {
        //        var tag = Excel.GetTagFromRow(row);
        //        if (tag == null) continue;

        //        if (tag.Data[0] == null)
        //        {
        //            MessageBox.Show("Some Error", "Error",
        //            MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return;
        //        }

        //        Scripts.TagSize.RenderPanel(ImagePanel, tag);
        //        PDF.AddPage(ImagePanel, tag.Data[0].Count);
        //        Excel.AddColumn(tag.Data[0].Sizes[0].Barcode[0], tag.Data[0].Count, tag.Data[0].CartboxNumber);
        //    }

        //    PDF.Save();
        //    File.Delete(path);
        //}

        private void Button1_Click(object sender, EventArgs e)
        {
            DropDownTimer.Start();
        }

        private void Size58x40_Click(object sender, EventArgs e)
        {
            ImagePanel.Size = ImagePanel.MaximumSize;
            Scripts.TagSize.Change(ImagePanel);
        }

        private void Size58x30_Click(object sender, EventArgs e)
        {
            ImagePanel.Size = ImagePanel.MinimumSize;
            Scripts.TagSize.Change(ImagePanel);
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
            Barcode.GetImage("1", BarcodeIMG);
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