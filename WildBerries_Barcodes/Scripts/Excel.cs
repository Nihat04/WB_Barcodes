using ExcelDataReader;
using System.Data;
using System.Net;
using System.Text;
using System.Text.Json;
using WildBerries_Barcodes.Scripts.JsonClasses;

namespace WildBerries_Barcodes.Scripts
{
    public class Excel
    {
        /// <summary>
        /// open excel file choose dialog
        /// </summary>
        /// <returns>created temporary file path</returns>
        public static string ChooseFile()
        {
            if (!Directory.Exists("temp"))
                Directory.CreateDirectory("temp");

            var temporaryExcelPath = @"temp\temporaryExcel.xlsx";

            using (OpenFileDialog dialog = new() { Filter = "Excel WorkBook|*.xlsx", ValidateNames = true })
            {
                if (!(dialog.ShowDialog() == DialogResult.OK))
                    return "Error";

                //var enc = CodePagesEncodingProvider.Instance.GetEncoding(1252);
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                File.Copy(dialog.FileName, temporaryExcelPath, true);
            }

            return temporaryExcelPath;
        }

        public static Tag FormatRow(DataRow row)
        {
            if (row.ItemArray[0].ToString().StartsWith("Артикуль") || Equals(row.ItemArray[0].ToString(), "")) return null;

            var sellerArt = row.ItemArray[1].ToString();
            var jsonText = RestAPI.PostRequest(sellerArt);

            if (jsonText == HttpStatusCode.Unauthorized.ToString())
            {
                MessageBox.Show("Не удалось подключится к пользоателю WB. Проверьте токен подключения", "Ошибка подключения",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            var jsonAsClass = JsonSerializer.Deserialize<Tag>(jsonText);
            jsonAsClass.FilterData(sellerArt);
            jsonAsClass.FilterSize(row.ItemArray[3].ToString());
            jsonAsClass.Data[0].Count = int.Parse(row.ItemArray[4].ToString())*2;
            jsonAsClass.Data[0].Color = row.ItemArray[2].ToString();
            return jsonAsClass;
            //Excel.AddColumn(Logic.GetBarcode(row.ItemArray[3], itemData.sizes), int.Parse(row.ItemArray[4].ToString()), int.Parse(row.ItemArray[5].ToString()));
        }

        public static DataRow[] ReadFile(string path)
        {
            FileStream file = File.Open(path, FileMode.Open, FileAccess.Read);
            var reader = ExcelReaderFactory.CreateReader(file);
            var excel = reader.AsDataSet();
            var sheet = excel.Tables[0];
            var rows = sheet.Select();
            file.Close();
            return rows;
        }

        public static void AddColumn(string barcode, int count, int CartonID)
        {
            var path = "template.txt";
            var newText = $"{barcode} {count} {CartonID}\n";
            if (File.Exists(path))
            {
                var file = File.ReadAllText(path);
                newText = file + newText;
            }

            File.WriteAllText(path, newText);
        }
    }
}
