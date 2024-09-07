using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBBarcodes.Classes.JsonClasses;

namespace WBBarcodes.Classes
{
    public class Product
    {
        public string? Articul { get; set; }
        public string? Size { get; set; }
        public string Country { get; set; }
        public string Type { get; set; }
        public string Barcode { get; set; }

        public Product(Card wbProduct)
        {
            Articul = wbProduct.NmID.ToString();

            if(wbProduct != null && wbProduct.RequiredSize != null) Size = wbProduct.RequiredSize.TechSize;

            Country = wbProduct.getCountry();
            Type = wbProduct.SubjectName;
            Barcode = wbProduct.RequiredSize.Skus[0];
        }

        public Product(OzonProduct product)
        {
            Articul = product.Articul;
            Size = product.Name;
            Barcode = product.Barcode;
        }
    }
}
