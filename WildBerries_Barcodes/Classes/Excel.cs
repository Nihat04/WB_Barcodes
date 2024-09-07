using ExcelDataReader;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBBarcodes.Classes.JsonClasses;
using WBBarcodes.Exceptions;

namespace WBBarcodes.Classes
{
    internal class Excel : File<DataTable>
    {
        private bool CartonInclude;

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

        public static Card getCardFromProducts(DataRow row, WbProduct productsList)
        {
            var articul = row.ItemArray[0].ToString();

            if (articul.StartsWith("Артикуль") || Equals(articul, ""))
                return null;

            var size = row.ItemArray[1].ToString().ToLower();
            var count = 0;
            var countIsNumber = int.TryParse(row.ItemArray[2].ToString(), out count);
            var cartBoxNumber = row.ItemArray[3].ToString();
            var productsCount = productsList.Cards.Count();
            var card = productsList.Cards.Find(card => card.NmID.ToString().Equals(articul));

            while (card == null)
            {
                productsList.getMore();
                card = productsList.Cards.Find(card => card.NmID.ToString().Equals(articul));

                if (productsList.Cards.Count <= productsCount) throw new OnRunException("Ошибка артикуля", $"Не удалось найти товар с арттикулем \"{articul}\"");
                productsCount = productsList.Cards.Count;
            }

            card.RequiredSize = card.Sizes.Find(cardSize => cardSize.TechSize.ToLower().Equals(size));
            card.TagsCount = count;

            if (card.RequiredSize == null) throw new OnRunException("Ошибка размера", $"Не удалось найти размер {size} у товара с артикулем \"{articul}\"");
            if (!countIsNumber) throw new OnRunException("Ошибка количества", $"Неверно указано количество товара");

            if (!cartBoxNumber.Equals("") && int.TryParse(cartBoxNumber, out _)) card.BoxId = int.Parse(row.ItemArray[3].ToString());

            return card;
        }
    }
}
