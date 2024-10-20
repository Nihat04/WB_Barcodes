using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBBarcodes.Classes.JsonClasses
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Attribute
    {
        public int? attribute_id { get; set; }
        public int? complex_id { get; set; }
        public List<Value> values { get; set; }
    }

    public class Image
    {
        public string file_name { get; set; }
        public bool? @default { get; set; }
        public int? index { get; set; }
    }

    public class Result
    {
        public int? id { get; set; }
        public string barcode { get; set; }
        public int? category_id { get; set; }
        public string name { get; set; }
        public string offer_id { get; set; }
        public int? height { get; set; }
        public int? depth { get; set; }
        public int? width { get; set; }
        public string dimension_unit { get; set; }
        public int? weight { get; set; }
        public string weight_unit { get; set; }
        public List<Image> images { get; set; }
        public string image_group_id { get; set; }
        public List<object> images360 { get; set; }
        public List<object> pdf_list { get; set; }
        public List<Attribute> attributes { get; set; }
        public List<object> complex_attributes { get; set; }
        public string color_image { get; set; }
        public string last_id { get; set; }
    }

    public class OzonProducts
    {
        public List<Result> result { get; set; }
        public int? total { get; set; }
        public string last_id { get; set; }
    }

    public class Value
    {
        public int? dictionary_value_id { get; set; }
        public string value { get; set; }
    }


}
