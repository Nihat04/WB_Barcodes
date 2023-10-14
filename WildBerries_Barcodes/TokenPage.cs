using System.Diagnostics;
using WildBerries_Barcodes.Scripts;

namespace WildBerries_Barcodes
{
    public partial class TokenPage : Form
    {
        public TokenPage()
        {
            InitializeComponent();
        }

        private void GoToSite(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var sInfo = new ProcessStartInfo() {FileName = "https://seller.wildberries.ru/supplier-settings/access-to-api", UseShellExecute = true };

            Process.Start(sInfo);
        }

        private void OnLoad(object sender, EventArgs e)
        {
            TokenBox.Text = RestAPI.Token;
        }
    }
}
