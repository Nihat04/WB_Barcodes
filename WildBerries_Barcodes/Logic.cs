using ExcelDataReader;
using System.Data;
using System.Text;
using System.Text.Json;

namespace WildBerries_Barcodes
{
    public class Logic
    {
        public static void GeneratePdfByExcel(Panel panel)
        {
            if (!Directory.Exists("temp"))
                Directory.CreateDirectory("temp");

            var temporaryExcelPath = @"temp\temporaryExcel.xlsx";

            using (OpenFileDialog dialog = new OpenFileDialog() { Filter = "Excel WorkBook|*.xlsx", ValidateNames = true })
            {
                if (!(dialog.ShowDialog() == DialogResult.OK))
                    return;

                var enc = CodePagesEncodingProvider.Instance.GetEncoding(1252);
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                
                File.Copy(dialog.FileName, temporaryExcelPath, true);
            }

            using (FileStream file = File.Open(temporaryExcelPath, FileMode.Open, FileAccess.Read))
            {
                PDF.CreateNew();

                var reader = ExcelReaderFactory.CreateReader(file);
                var excel = reader.AsDataSet();
                var sheet = excel.Tables[0];
                var rows = sheet.Select();

                var excelData = FilterExcelJson(rows);
                ApplyData(panel, excelData.ToArray());
                PDF.Save();
            }
            File.Delete(temporaryExcelPath);
        }

        private static void ApplyData(Panel panel, Dictionary<string, object>[] excelData)
        {
            var controls = panel.Controls;

            foreach(var row in excelData)
            {
                foreach(Control control in controls)
                {
                    var type = control.GetType().Name;

                    switch(type)
                    {
                        case "Label":
                            var controlAsLabel = control as Label;

                            if (!row.ContainsKey(controlAsLabel.Name))
                                continue;

                            if(controlAsLabel.Text.Contains(':'))
                                controlAsLabel.Text = $"{controlAsLabel.Text.Split(':')[0]}: {row[controlAsLabel.Name]}";
                            else
                                controlAsLabel.Text = row[controlAsLabel.Name].ToString();
                            break;

                        case "PictureBox":
                            var controlAsPictureBox = control as PictureBox;

                            if (controlAsPictureBox.Name.ToLower() != "barcodeimg")
                                break;

                            BarcodeImage(row["BarcodeDigits"].ToString(), controlAsPictureBox);
                            break;
                    }
                }

                PDF.CreatePage(panel, int.Parse(row["count"].ToString())*2);
                Excel.AddColumn(row["BarcodeDigits"].ToString(), int.Parse(row["count"].ToString()));
            }
        }

        private static List<Dictionary<string, object>> FilterExcel(DataRow[] rows)
        {
            var titles = rows[0].ItemArray;
            var allData = new List<Dictionary<string, object>>();
            var rowData = new Dictionary<string, object>();

            for (int i = 1; i < rows.Length; i++) 
            {
                var row = rows[i];

                if (row.ItemArray[0].ToString() == string.Empty) 
                    continue;

                for(int j = 0; j < row.ItemArray.Length; j++)
                {
                    rowData[titles[j].ToString()] = row.ItemArray[j];
                }

                allData.Add(new Dictionary<string, object>(rowData));
                rowData.Clear();
            }

            return allData;
        }

        private static List<Dictionary<string, object>> FilterExcelJson(DataRow[] rows)
        {
            var data = new List<Dictionary<string, object>>();

            foreach(var row in rows)
            {
                if (row.ItemArray[0].ToString().StartsWith("Артикуль") || Equals(row.ItemArray[0].ToString(), ""))
                    continue;

                var jsonText = RestAPI.PostRequest(row.ItemArray[1].ToString());

                if(Equals(jsonText, "Error: Unauthorized"))
                    return null;

                var jsonAsClass = JsonSerializer.Deserialize<DataWB>(jsonText);

                var itemData = jsonAsClass.data[0];
                if(jsonAsClass.data.Count > 1)
                {
                    foreach(var articul in jsonAsClass.data)
                    {
                        if(articul.vendorCode == row.ItemArray[1].ToString())
                        {
                            itemData = articul;
                            break;
                        }
                    }
                }

                var newData = new Dictionary<string, object>() {
                    {"BarcodeDigits", GetBarcode(row.ItemArray[3], itemData.sizes) },
                    {"count", row.ItemArray[4] },
                    {"Articul", itemData.nmID },
                    {"Color", row.ItemArray[2] },
                    {"Size", row.ItemArray[3] },
                    {"Type", itemData.@object },
                    {"Country", GetCountry(itemData.characteristics) }
                };
                data.Add(newData);
            }

            return data;
        }

        private static object GetCountry(List<Characteristic> characteristics)
        {
            foreach(Characteristic characteristic in characteristics)
            {
                if (!Equals(characteristic.Country, null))
                    return characteristic.Country[0];
            }
            return "Unknown";
        }

        public static void BarcodeImage(string barcodeText, PictureBox barcodeBox)
        {
            var barcodeNumbers = "888888888888";

            if (!(barcodeText.Length >= barcodeNumbers.Length && barcodeText.Length <= barcodeNumbers.Length + 1))
                return;

            var barcode = new BarcodeLib.Barcode();
            barcodeNumbers = barcodeText;

            barcodeBox.Image = barcode.Encode(BarcodeLib.TYPE.EAN13, barcodeNumbers, Color.Black, Color.Transparent, barcodeBox.Width, barcodeBox.Height);
        }

        private static async Task ApplyAsync(Panel panel, Dictionary<string, object>[] data)
        {
            await Task.Run(() =>
            {

            });
            
        }

        private static string GetBarcode(object requiredSize, List<Size> sizes)
        {
            foreach(var size in sizes)
            {
                if(Equals(requiredSize.ToString(), size.techSize.ToString()))
                    return size.skus[0].ToString();
            }
            return "unknown size";
        }

    }
}