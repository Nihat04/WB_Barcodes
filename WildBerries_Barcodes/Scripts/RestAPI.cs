using System.Net.Http.Headers;
using System.Text;

namespace WildBerries_Barcodes.Scripts
{
    public static class RestAPI
    {
        public static string Token
        {
            get
            {
                if (!File.Exists("token.txt"))
                    File.Create("token.txt");

                return File.ReadAllText("token.txt");
            }
            set
            {
                File.WriteAllText("token.txt", value);
            }
        }

        public static string PostRequest(string articul)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            client.BaseAddress = new Uri("https://suppliers-api.wildberries.ru");

            var json = "{  \"vendorCodes\": [    \"" + articul + "\"  ],  \"allowedCategoriesOnly\": true}";
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = client.PostAsync("/content/v1/cards/filter", content).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                return responseContent;
            }
            else
                return $"Error: {response.StatusCode}";
        }
    }
}
