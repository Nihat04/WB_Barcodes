using System.Text.Json;

namespace WildBerries_Barcodes
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        private void FormLoad(object sender, EventArgs e)
        {
            if (!File.Exists("info.json"))
                File.Create("info.json");

            using(var jsonFile = File.OpenRead("info.json"))
            {
                var jsonText = JsonSerializer.Deserialize<Info>(jsonFile);
                About.Text = jsonText.About;
                Brand.Text = jsonText.Brand;
            }
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            var data = new Info()
            {
                Brand = this.Brand.Text,
                About = this.About.Text,
            };

            var jsonText = JsonSerializer.Serialize(data);
            File.WriteAllText("info.json", jsonText);
            this.DialogResult = DialogResult.OK;
        }
    }
}
