using System.Diagnostics;
using System.Text.Json;
using System.Windows.Forms;
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

        private void ImportExcelButton_Click(object sender, EventArgs e)
        {
            var path = Excel.ChooseFile();
            var rows = Excel.ReadFile(path);

            foreach (var row in rows)
            {
                var tag = Excel.GetTagFromRow(row);
                if (tag == null) continue;

                if (tag.Data[0] == null)
                {
                    MessageBox.Show("Some Error", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Scripts.TagSize.RenderPanel(ImagePanel, tag);
                PDF.AddPage(ImagePanel, tag.Data[0].Count);
                Excel.AddColumn(tag.Data[0].Sizes[0].Barcode[0], tag.Data[0].Count, tag.Data[0].CartboxNumber);
            }

            PDF.Save();
            File.Delete(path);
        }

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
                if(TicketSizeContainer.Height <= TicketSizeContainer.MinimumSize.Height)
                {
                    DropDownTimer.Stop();
                    Expand = false;
                }
            }
            else
            {
                TicketSizeContainer.Height += 10;
                if(TicketSizeContainer.Height >= TicketSizeContainer.MaximumSize.Height)
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
            if(dialogResult == DialogResult.OK) 
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
            Logic.BarcodeImage("1", BarcodeIMG);
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