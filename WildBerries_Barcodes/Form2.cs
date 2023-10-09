using System.Diagnostics;

namespace WildBerries_Barcodes
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void GoToSite(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var sInfo = new ProcessStartInfo() {FileName = "https://seller.wildberries.ru/supplier-settings/access-to-api", UseShellExecute = true };

            Process.Start(sInfo);
        }
    }
}
