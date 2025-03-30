using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WBBarcodes.Classes.JsonClasses;

namespace WBBarcodes.Classes
{
    public class Product
    {
        public string? Articul { get; set; }
        public string SellerArticul { get; set; }
        public string? Size { get; set; }
        public string? SellerSize {  get; set; }
        public string Country { get; set; }
        public string Type { get; set; }
        public string? Barcode { get; set; }
        public string Brand { get; set; }

        public Product(Card wbProduct)
        {
            Articul = wbProduct.NmID.ToString();

            if (wbProduct != null && wbProduct.RequiredSize != null) Size = wbProduct.RequiredSize.TechSize;

            Country = wbProduct.getCountry();
            Type = wbProduct.SubjectName;
            Barcode = wbProduct.RequiredSize.Skus[0];
            Brand = wbProduct.Brand.Trim();
            SellerArticul = wbProduct.VendorCode.ToString();
        }

        public Product(OzonProduct product)
        {
            Articul = product.Articul;
            Size = product.Name;
            Barcode = product.Barcode;
        }

        public Product(Result result)
        {
            Articul = result.offer_id;
            SellerSize = getAttribute(result, 9533);
            Size = getAttribute(result, 4295);
            Country = getAttribute(result, 4389);
            Type = getAttribute(result, 8229);
            Barcode = result.barcode;
        }

        private string getAttribute(Result result, int id)
        {
            var findResult = result.attributes.Find(attribute => attribute.id == id);
            if (findResult == null)
            {
                return "";
            }

            return findResult.values[0].value.ToString();
        }
    }
}
