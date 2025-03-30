using ExcelDataReader;
using Spire.Xls;
using System.Data;
using System.Text;
using WBBarcodes.Classes.JsonClasses;
using WBBarcodes.Exceptions;

namespace WBBarcodes.Classes
{
    internal class Excel : File<DataTable>
    {
        private readonly bool CartonInclude;

        public Excel(bool cartonInclude = false)
        {
            Item = new DataTable();
            this.CartonInclude = cartonInclude;

            if (cartonInclude)
            {
                Item.Columns.Add("баркод товара", typeof(long));
                Item.Columns.Add("кол-во товаров", typeof(long));
                Item.Columns.Add("шк короба", typeof(long));
                Item.Columns.Add("срок годности");
            }
            else
            {
                Item.Columns.Add("баркод", typeof(long));
                Item.Columns.Add("количество", typeof(long));
            }
        }

        public void AddColumn(string barcode, int count, int CartonID = 0)
        {
            if (CartonInclude)
            {
                Item.Rows.Add(new object[] { barcode, count, CartonID });
            }
            else
            {
                Item.Rows.Add(new object[] { barcode, count });
            }
        }

        public void Clear()
        {
            Item.Clear();
        }
        public override void Save(string folderPath)
        {
            var book = new Workbook();

            for (int i = 0; i < book.Worksheets.Count; i++)
                book.Worksheets.Remove(i);

            //book.Worksheets.Add("лист1");
            var sheet = book.Worksheets[0];
            sheet.InsertDataTable(Item, true, 1, 1);

            var fileName = "template.xlsx";
            if (CartonInclude)
                fileName = "shk-excel.xlsx";

            book.SaveToFile(Path.Combine(folderPath, fileName));
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

        public static ExcelRow[] GetProductRows(DataRow[] rows)
        {
            ExcelRow[] excelRows = new ExcelRow[rows.Length];

            for (int i = 1;i < rows.Length;i++)
            {
                var row = rows[i];

                if (row.ItemArray[0] == DBNull.Value && row.ItemArray[1] == DBNull.Value) continue;

                excelRows[i] = new ExcelRow(row);
            }

            return excelRows;
        }

        public static ExcelRow[] GetProductRows(string path)
        {
            var dataRows = ReadFile(path);
            return GetProductRows(dataRows);
        }

        public static Card GetCardFromExcelRow(ExcelRow row, WbProduct productsList)
        {
            var productsCount = productsList.Cards.Count();
            var card = productsList.Cards.Find(card => card.NmID == row.Articul);

            while (card == null)
            {
                productsList.getMore();
                card = productsList.Cards.Find(card => card.NmID == row.Articul);

                if (productsList.Cards.Count <= productsCount) throw new OnRunException("Ошибка артикуля", $"Не удалось найти товар с арттикулем \"{row.Articul}\"");
                productsCount = productsList.Cards.Count;
            }

            card.RequiredSize = card.Sizes.Find(cardSize => cardSize.TechSize.ToLower().Equals(row.Size));
            card.TagsCount = row.Count;

            if (card.RequiredSize == null) throw new OnRunException("Ошибка размера", $"Не удалось найти размер {row.Size} у товара с артикулем \"{row.Articul}\"");

            if (!(row.BoxId == null || row.BoxId.Equals("")) && int.TryParse(row.BoxId, out _)) card.BoxId = int.Parse(row.BoxId);

            return card;
        }
    }
}
