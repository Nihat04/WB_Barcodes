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

        public static Card getCardFromProducts(DataRow row, TagV2 productsList)
        {
            var articul = row.ItemArray[0].ToString();
            var size = row.ItemArray[1].ToString();
            var count = row.ItemArray[2].ToString();
            var cartBoxNumber = row.ItemArray[3].ToString();

            if (articul.StartsWith("Артикуль") || Equals(articul, ""))
                return null;

            var card = productsList.Cards.Find(card => card.NmID.ToString().Equals(articul));

            while(card == null)
            {
                productsList.getMore();
                card = productsList.Cards.Find(card => card.NmID.ToString().Equals(articul));
            }

            card.TagsCount = int.Parse(row.ItemArray[2].ToString());

            card.RequiredSize = card.Sizes.Find(size => size.TechSize.Equals(size));

            if (card.RequiredSize == null) throw new OnRunException("Ошибка размера", $"Не удалось найти размер {}");

            if (!row.ItemArray[3].ToString().Equals("") && int.TryParse(row.ItemArray[3].ToString(), out _))
            {
                card.BoxId = int.Parse(row.ItemArray[3].ToString());
            }

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
