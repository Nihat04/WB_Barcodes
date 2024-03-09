using ExcelDataReader;
using System.Data;
using System.Net;
using System.Text;
using System.Text.Json;
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

        public static Tag GetTagFromRow(DataRow row)
        {
            if (row.ItemArray[0].ToString().StartsWith("Артикуль") || Equals(row.ItemArray[0].ToString(), ""))
                return null;

            var sellerArt = row.ItemArray[0].ToString();
            var jsonText = RestAPI.PostRequest(sellerArt);

            if (jsonText == HttpStatusCode.Unauthorized.ToString())
            {
                MessageBox.Show("Не удалось подключится к пользоателю WB. Проверьте токен подключения", 
                    "Ошибка подключения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new Tag { Error = true };
            }

            var jsonAsClass = JsonSerializer.Deserialize<Tag>(jsonText);

            if(jsonAsClass.Data.Count <= 0)
            {
                MessageBox.Show("Не удалось идентифицировать товар. Проверьте корректность в файле", 
                    "Ошибка файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new Tag { Error = true };
            }

            jsonAsClass.FilterData(sellerArt);

            jsonAsClass.FilterSize(row.ItemArray[3].ToString());
            if (jsonAsClass.Data[0].Sizes[0] == null)
            {
                MessageBox.Show("Не удалось идентифицировать размер товара. Проверьте корректность в файле",
                    "Ошибка файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new Tag { Error = true };
            }

            var productCount = row.ItemArray[4].ToString();
            var productColor = row.ItemArray[2].ToString();
            var cartboxId = row.ItemArray[5].ToString();

            if(productCount.Equals("") || !int.TryParse(productCount, out _))
            {
                MessageBox.Show("Неверно указано количество товара",
                    "Ошибка количества", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new Tag { Error = true };
            }
            jsonAsClass.Data[0].Count = int.Parse(productCount);

            if (!productColor.Equals(""))
                jsonAsClass.Data[0].Color = row.ItemArray[2].ToString();
            if(!cartboxId.Equals(""))
                jsonAsClass.Data[0].CartboxNumber = int.Parse(row.ItemArray[5].ToString());
            return jsonAsClass;
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
