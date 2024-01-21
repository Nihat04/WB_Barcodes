using System.Runtime.CompilerServices;
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
                Brand.Text = jsonText.Brand;

                var aboutText = jsonText.About.Trim().Replace("\r", "");

                var nameUselessString = "Поставщик: ";
                var cityUselessString = "\nг. ";

                var nameStartIndex = aboutText.LastIndexOf(nameUselessString) + nameUselessString.Length;
                var nameEndIndex = aboutText.IndexOf(cityUselessString);

                var cityStartIndex = aboutText.LastIndexOf(cityUselessString) + cityUselessString.Length;

                BuissnesName.Text = aboutText.Substring(nameStartIndex, nameEndIndex - nameStartIndex);
                BuissnesCity.Text = aboutText.Substring(cityStartIndex);
            }
        }
        private void ButtonOK_Click(object sender, EventArgs e)
        {
            var data = new Info()
            {
                Brand = this.Brand.Text,
                About = $"Поставщик: {BuissnesName.Text}\nг. {BuissnesCity.Text}",
            };

            var jsonText = JsonSerializer.Serialize(data);
            File.WriteAllText("info.json", jsonText);
            this.DialogResult = DialogResult.OK;
        }
    }
}
