using System.Text;
using System.Text.Json;
using WBBarcodes.Classes.JsonClasses;

namespace WBBarcodes.Api
{
    internal class OzonApi
    {
        private static string FileName = "ozon.txt";

        public static string ClientInfo
        {
            get
            {
                if (!File.Exists(FileName))
                {
                    var file = File.Create(FileName);
                    file.Close();
                }

                return File.ReadAllText(FileName);
            }
            set
            {
                File.WriteAllText(FileName, value);
            }
        }

        public static OzonProducts getProducts(string[] sellerArticuls)
        {
            string[] clientInfo = ClientInfo.Split('\n');

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Client-Id", clientInfo[0].Trim());
            client.DefaultRequestHeaders.Add("Api-Key", clientInfo[1].Trim());

            var body = new { filter = new { offer_id = sellerArticuls }, limit = 1000 };
            Console.WriteLine(JsonSerializer.Serialize(body));

            var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var response = client.PostAsync("https://api-seller.ozon.ru/v3/products/info/attributes", content).Result;

            var responseJson = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(responseJson);

            var products = JsonSerializer.Deserialize<OzonProducts>(responseJson);

            return products;
        }
    }
}
