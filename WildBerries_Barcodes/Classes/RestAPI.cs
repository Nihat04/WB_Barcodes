using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using WBBarcodes.Classes.JsonClasses;

namespace WildBerries_Barcodes.Scripts
{
    public static class RestAPI
    {
        public static string Token
        {
            get
            {
                if (!File.Exists("token.txt"))
                {
                    var file = File.Create("token.txt");
                    file.Close();
                }

                return File.ReadAllText("token.txt");
            }
            set
            {
                File.WriteAllText("token.txt", value);
            }
        }

        public static HttpResponseMessage RequestProduct(string articul)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            client.BaseAddress = new Uri("https://suppliers-api.wildberries.ru");

            var json = "{  \"vendorCodes\": [    \"" + articul + "\"  ],  \"allowedCategoriesOnly\": true}";
            var content = new StringContent(json, Encoding.UTF8, "application/json");


            var response = client.PostAsync("/content/v1/cards/filter", content).Result;

            return response;
        }

        public static HttpResponseMessage RequestProduct(string articul, int requestsAttempts)
        {
            for (int i = 0; i < requestsAttempts; i++)
            {
                var response = RequestProduct(articul);

                if(response.IsSuccessStatusCode) 
                    return response;
            }
            return new HttpResponseMessage(HttpStatusCode.GatewayTimeout);
        }

        public static TagV2 getAllProducts()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            client.BaseAddress = new Uri("https://suppliers-api.wildberries.ru");

            var json = @"
            {
                ""settings"": {
                    ""cursor"": {
                        ""limit"": 1000
                    },
                    ""filter"": {
                        ""withPhoto"": -1
                        }
                }
            }";
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = client.PostAsync("/content/v2/get/cards/list", content).Result;

            var responseJson = response.Content.ReadAsStringAsync().Result;

            var products = JsonSerializer.Deserialize<TagV2>(responseJson);

            return products;
        }
    }
}
