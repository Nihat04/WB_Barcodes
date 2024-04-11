using ExcelDataReader;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.Json;
using WBBarcodes.Classes.JsonClasses;
using WBBarcodes.Exceptions;
using WildBerries_Barcodes.Scripts.JsonClasses;

namespace WildBerries_Barcodes.Scripts
{
    public class ExcelReader
    {
        /// <summary>
        /// open excel file choose dialog
        /// </summary>
        /// <returns>created temporary file path</returns>
        public static string ChooseFile()
        {
            if (!Directory.Exists("temp"))
                Directory.CreateDirectory("temp");

            const string temporaryExcelPath = @"temp\temporaryExcel.xlsx";

            using (OpenFileDialog dialog = new() { Filter = "Excel WorkBook|*.xlsx", ValidateNames = true })
            {
                if (!(dialog.ShowDialog() == DialogResult.OK))
                    return "Error";

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                File.Copy(dialog.FileName, temporaryExcelPath, true);
            }

            return temporaryExcelPath;
        }

        public static TagOld ConvertToTag(DataRow row)
        {
            if (row.ItemArray[0].ToString().StartsWith("Артикуль") || Equals(row.ItemArray[0].ToString(), ""))
                return null;

            var sellerArt = row.ItemArray[0].ToString();
            var response = RestAPI.RequestProduct(sellerArt);

            if (response.StatusCode.Equals(HttpStatusCode.NotFound))
                throw new OnRunException("Ошибка нахождения", $"Не удалось найти товар с артикулем \"{sellerArt}\"");

            if(response.StatusCode.Equals(HttpStatusCode.GatewayTimeout))
                throw new TimeoutException();

            if (response.StatusCode.Equals(HttpStatusCode.Unauthorized))
                throw new OnRunException("Ошибка авторизации", "Не удалось авторизироваться, проверьте токен подключения");

            var jsonAsClass = JsonSerializer.Deserialize<TagOld>(response.Content.ReadAsStringAsync().Result);
            if (jsonAsClass.Data.Count <= 0)
                throw new OnRunException("Ошибка товара", $"Не удалось найти товар: {sellerArt}");

            jsonAsClass.FilterData(sellerArt);

            var requiredSize = row.ItemArray[3].ToString();
            jsonAsClass.FilterSize(requiredSize);
            if (jsonAsClass.Data[0].Sizes[0] == null)
                throw new OnRunException("Неверный размер", $"Не удалось найти размер \"{requiredSize}\" у \"{sellerArt}\", проверьте файл");

            var productCount = row.ItemArray[4].ToString();
            var productColor = row.ItemArray[2].ToString();
            var cartboxId = row.ItemArray[5].ToString();

            if (productCount.Equals("") || !int.TryParse(productCount, out _))
                throw new OnRunException("Неверное количество", "Не удалось прочитать кол-во товара, проверьте файл");
            jsonAsClass.Data[0].Count = int.Parse(productCount);

            if (!productColor.Equals(""))
                jsonAsClass.Data[0].Color = row.ItemArray[2].ToString();
            if (!cartboxId.Equals(""))
                jsonAsClass.Data[0].CartboxNumber = int.Parse(row.ItemArray[5].ToString());
            return jsonAsClass;
        }

        public static Card getCardFromProducts(DataRow row, TagV2 productsList)
        {
            if (row.ItemArray[0].ToString().StartsWith("Артикуль") || Equals(row.ItemArray[0].ToString(), ""))
                return null;

            var articul = row.ItemArray[0].ToString();

            var card = productsList.Cards.Find(card => card.NmID.ToString().Equals(articul));

            card.TagsCount = int.Parse(row.ItemArray[2].ToString());

            card.RequiredSize = card.Sizes.Find(size => size.TechSize.Equals(row.ItemArray[1].ToString()));

            return card;
        }

        public static DataRow[] ReadFile(string path)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            FileStream file = File.Open(path, FileMode.Open, FileAccess.Read);
            var reader = ExcelReaderFactory.CreateReader(file);
            var excel = reader.AsDataSet();
            var sheet = excel.Tables[0];
            var rows = sheet.Select();
            file.Close();
            return rows;
        }
    }
}
