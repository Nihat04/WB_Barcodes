using ExcelDataReader;
using PdfSharp.Charting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WildBerries_Barcodes.Scripts.JsonClasses;

namespace WildBerries_Barcodes.Scripts
{
    public class Excel
    {
        public static string ChooseFile()
        {
            if (!Directory.Exists("temp"))
                Directory.CreateDirectory("temp");

            var temporaryExcelPath = @"temp\temporaryExcel.xlsx";

            using (OpenFileDialog dialog = new OpenFileDialog() { Filter = "Excel WorkBook|*.xlsx", ValidateNames = true })
            {
                if (!(dialog.ShowDialog() == DialogResult.OK))
                    return "Error";

                var enc = CodePagesEncodingProvider.Instance.GetEncoding(1252);
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                File.Copy(dialog.FileName, temporaryExcelPath, true);
            }

            return temporaryExcelPath;
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

        public static List<Dictionary<string, object>> Format(DataRow[] rows)
        {
            var data = new List<Dictionary<string, object>>();

            foreach (var row in rows)
            {
                if (row.ItemArray[0].ToString().StartsWith("Артикуль") || Equals(row.ItemArray[0].ToString(), ""))
                    continue;

                var sellerArt = row.ItemArray[1].ToString();

                var jsonText = RestAPI.PostRequest(sellerArt);

                if(jsonText == HttpStatusCode.Unauthorized.ToString())
                {
                    MessageBox.Show("Не удалось подключится к пользоателю WB. Проверьте токен подключения", "Ошибка подключения", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                var jsonAsClass = JsonSerializer.Deserialize<DataWB>(jsonText);

                if(jsonAsClass.data.Count == 0)
                {
                    MessageBox.Show($"Не удалось найти товар с артикулем \"{sellerArt}\"", "Ошибка поиска",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                var itemData = jsonAsClass.data[0];
                if (jsonAsClass.data.Count > 1)
                {
                    foreach (var articul in jsonAsClass.data)
                    {
                        if (articul.vendorCode == row.ItemArray[1].ToString())
                        {
                            itemData = articul;
                            break;
                        }
                    }
                }

                var newData = new Dictionary<string, object>() {
                    {"BarcodeDigits", Logic.GetBarcode(row.ItemArray[3], itemData.sizes) },
                    {"count", row.ItemArray[4] },
                    {"Articul", itemData.nmID },
                    {"Color", row.ItemArray[2] },
                    {"Size", row.ItemArray[3] },
                    {"Type", itemData.@object },
                    {"Country", Logic.GetCountry(itemData.characteristics) }
                };
                data.Add(newData);
                Excel.AddColumn(Logic.GetBarcode(row.ItemArray[3], itemData.sizes), int.Parse(row.ItemArray[4].ToString()), int.Parse(row.ItemArray[5].ToString()));
            }

            return data;
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
