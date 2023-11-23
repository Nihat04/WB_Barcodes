using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WildBerries_Barcodes.Scripts.JsonClasses;
using WildBerries_Barcodes.Scripts;

namespace WBBarcodes.Scripts
{
    internal class Experimental
    {
        public static Task<String> AsyncPostRequest(string articul)
        {
            var task = new Task<string>(() =>
            {
                return RestAPI.PostRequest(articul);
            });
            task.Start();
            return task;
        }

        async public static void AsyncFormat(Panel panel)
        {
            var data = new List<Dictionary<string, object>>();

            for (int i = 172; i < 182; i++)
            {
                var jsonText = await AsyncPostRequest(i.ToString());

                if (jsonText == HttpStatusCode.Unauthorized.ToString())
                {
                    MessageBox.Show("Не удалось подключится к пользоателю WB. Проверьте токен подключения", "Ошибка подключения",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var jsonAsClass = JsonSerializer.Deserialize<DataWB>(jsonText);

                if (jsonAsClass.data.Count == 0)
                {
                    MessageBox.Show($"Не удалось найти товар с артикулем \"{i}\"", "Ошибка поиска",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var itemData = jsonAsClass.data[0];
                if (jsonAsClass.data.Count > 1)
                {
                    foreach (var articul in jsonAsClass.data)
                    {
                        if (articul.vendorCode == i.ToString())
                        {
                            itemData = articul;
                            break;
                        }
                    }
                }

                var newData = new Dictionary<string, object>() {
                    {"BarcodeDigits", Logic.GetBarcode("30", itemData.sizes) },
                    {"count", "1" },
                    {"Articul", itemData.nmID },
                    {"Color", "30" },
                    {"Size", "30" },
                    {"Type", itemData.@object },
                    {"Country", Logic.GetCountry(itemData.characteristics) }
                };
                data.Add(newData);
            }

            Logic.ApplyData(panel, data);
            PDF.Save();
        }
    }
}
