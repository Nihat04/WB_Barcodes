using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBBarcodes.Scripts
{
    public class ExcelTemplate
    {
        private DataTable Dt { get; set; }
        private bool CartonInclude;

        public ExcelTemplate(bool cartonInclude = false)
        {
            Dt = new DataTable();
            this.CartonInclude = cartonInclude;

            if(cartonInclude)
            {
                Dt.Columns.Add("баркод товара", typeof(long));
                Dt.Columns.Add("кол-во товаров", typeof(long));
                Dt.Columns.Add("шк короба", typeof(long));
                Dt.Columns.Add("срок годности");
            } else
            {
                Dt.Columns.Add("баркод", typeof(long));
                Dt.Columns.Add("количество", typeof(long));
            }
        }

        public void AddColumn(string barcode, int count, int CartonID = 0)
        {
            if(CartonInclude)
            {
                Dt.Rows.Add(new object[] { barcode, count, CartonID });
            } else
            {
                Dt.Rows.Add(new object[] { barcode, count });
            }
        }

        public void Clear()
        {
            Dt.Clear();
        }

        public void Save(string folderPath)
        {
            var book = new Workbook();

            for(int i = 0; i < book.Worksheets.Count; i++)
                book.Worksheets.Remove(i);

            //book.Worksheets.Add("лист1");
            var sheet = book.Worksheets[0];
            sheet.InsertDataTable(Dt, true, 1, 1);

            var fileName = "template.xlsx";
            if (CartonInclude)
                fileName = "shk-excel.xlsx";

            book.SaveToFile(Path.Combine(folderPath, fileName));
        }
    }
}
