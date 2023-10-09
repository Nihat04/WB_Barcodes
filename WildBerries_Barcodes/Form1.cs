using System.Diagnostics;
using System.Text.Json;

namespace WildBerries_Barcodes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool expand = false;

        private void FolderOpenButton_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", $@"PDF's folder");
        }

        private void ImportExcelButton_Click(object sender, EventArgs e)
        {
            Logic.GeneratePdfByExcel(ImagePanel);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DropDownTimer.Start();
        }

        private void Size58x40_Click(object sender, EventArgs e)
        {
            ImagePanel.Size = ImagePanel.MaximumSize;
            TagSize.Change(ImagePanel);
        }

        private void Size58x30_Click(object sender, EventArgs e)
        {
            ImagePanel.Size = ImagePanel.MinimumSize;
            TagSize.Change(ImagePanel);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (expand)
            {
                TicketSizeContainer.Height -= 10;
                if(TicketSizeContainer.Height <= TicketSizeContainer.MinimumSize.Height)
                {
                    DropDownTimer.Stop();
                    expand = false;
                }
            }
            else
            {
                TicketSizeContainer.Height += 10;
                if(TicketSizeContainer.Height >= TicketSizeContainer.MaximumSize.Height)
                {
                    DropDownTimer.Stop();
                    expand = true;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form form = new Form2();
            DialogResult dialogResult = form.ShowDialog();
            if(dialogResult == DialogResult.OK) 
            {
                var controls = form.Controls;
                var textBox = controls.Find("textBox1", true)[0].Text;
                RestAPI.Token = textBox;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form form = new Form3();
            DialogResult dialogResult = form.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                UpdateInfo(About, Brand);
            }
        }

        private void FormLoad(object sender, EventArgs e)
        {
            UpdateInfo(About, Brand);
            Logic.BarcodeImage("1", BarcodeIMG);
            //TagSize.Change(ImagePanel);
        }

        private void UpdateInfo(Label about, Label brand)
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