using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using WBBarcodes.Classes.JsonClasses;

namespace WBBarcodes.Api
{
    public static class WildBerriesAPI
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

        public static WbProduct getAllProducts(DateTime? upd = null, int? nmId = null)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            client.BaseAddress = new Uri("https://content-api.wildberries.ru");

            var json = @"
                {
                    ""settings"": {
                        ""cursor"": {
                            ""limit"": 100
                        },
                        ""filter"": {
                            ""withPhoto"": -1
                            }
                    }
                }";

            if (upd != null && nmId != null)
            {
                var updJson = JsonSerializer.Serialize(upd);
                json = @$"
                {{
                    ""settings"": {{
                        ""cursor"": {{
                            ""limit"": 100,
                            ""updatedAt"": {updJson},
                            ""nmID"": {nmId}
                        }},
                        ""filter"": {{
                            ""withPhoto"": -1
                            }}
                    }}
                }}";
            }

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = client.PostAsync("/content/v2/get/cards/list", content).Result;

            var responseJson = response.Content.ReadAsStringAsync().Result;

            var products = JsonSerializer.Deserialize<WbProduct>(responseJson);

            if (products == null) throw new Exception("Products was not found");

            return products;
        }
    }
}
