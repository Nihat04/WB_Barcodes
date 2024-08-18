using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBBarcodes.Classes
{
    internal class Ozon : File<OzonProduct>
    {
        public static OzonProduct[] GetProducts()
        {
            var path = "ozon_products.xlsx";
            var excelRows = Excel.ReadFile(path);
            var ozonProducts = new OzonProduct[excelRows.Length - 4];

            for (int i = 4; i < excelRows.Length; i++)
            {
                var row = excelRows[i];
                ozonProducts[i - 4] = new OzonProduct { Articul = row.ItemArray[0].ToString(), Name = row.ItemArray[2].ToString(), Barcode = row.ItemArray[3].ToString() };

            }

            return ozonProducts;
        }

        internal static OzonProduct GetProducts(DataRow row, OzonProduct[] products)
        {
            return Array.Find(products, (product) => product.Articul.Equals(row.ItemArray[0].ToString()));
        }

        public override void Save(string folderPath)
        {
            throw new NotImplementedException();
        }
    }
}
