﻿using System;
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
                ozonProducts[i - 4] = new OzonProduct { Articul = row.ItemArray[0].ToString(), Name = row.ItemArray[2].ToString(), Barcode = row.ItemArray[4].ToString() };
            }

            return ozonProducts;
        }

        internal static OzonProduct GetProducts(DataRow row, OzonProduct[] products)
        {
            return Array.Find(products, (product) => product.Articul.Equals(row.ItemArray[0].ToString()));
        }

        public static List<string> GetProductsArticuls(DataRow[] data)
        {
            List<string> articuls = new List<string>();

            foreach (DataRow row in data)
            {
                if (row != null)
                {
                    var articul = row.ItemArray[0];
                    if (articul != null)
                    {
                        articuls.Add(row.ItemArray[0].ToString());
                    }
                }
            }

            return articuls;
        }

        public override void Save(string folderPath)
        {
            throw new NotImplementedException();
        }
    }
}
