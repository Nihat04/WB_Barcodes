using System.Text.Json;
using WildBerries_Barcodes.Scripts.JsonClasses;

namespace WildBerries_Barcodes
{
    public partial class InformationPage : Form
    {
        public InformationPage()
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
        private void ButtonOK_Click(object sender, EventArgs e)
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
