using System.Text.Json;
using System.Text.Json.Serialization;
using WildBerries_Barcodes.Scripts;

namespace WBBarcodes.Classes.JsonClasses
{
    public class TagV2
    {
        [JsonPropertyName("cards")]
        public List<Card> Cards { get; set; }

        [JsonPropertyName("cursor")]
        public Cursor Cursor { get; set; }

        public bool error { get; set; }

        public TagV2 getMore()
        {
            var moreProducts = RestAPI.getAllProducts(this.Cursor.UpdatedAt, this.Cursor.NmID);

            moreProducts.Cards.ForEach(card => this.Cards.Add(card));
            this.Cursor = moreProducts.Cursor;

            return this;
        }
    }

    public class Card
    {
        [JsonPropertyName("nmID")]
        public int? NmID { get; set; }

        [JsonPropertyName("imtID")]
        public int? ImtID { get; set; }

        [JsonPropertyName("nmUUID")]
        public string NmUUID { get; set; }

        [JsonPropertyName("subjectID")]
        public int? SubjectID { get; set; }

        [JsonPropertyName("subjectName")]
        public string SubjectName { get; set; }

        [JsonPropertyName("vendorCode")]
        public string VendorCode { get; set; }

        [JsonPropertyName("brand")]
        public string Brand { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("photos")]
        public List<Photo> Photos { get; set; }

        [JsonPropertyName("dimensions")]
        public Dimensions Dimensions { get; set; }

        [JsonPropertyName("characteristics")]
        public List<Characteristic> Characteristics { get; set; }

        [JsonPropertyName("sizes")]
        public List<Size> Sizes { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime? UpdatedAt { get; set; }

        [JsonPropertyName("tags")]
        public List<Tag> Tags { get; set; }

        public int TagsCount { get; set; }
        public Size? RequiredSize { get; set; }
        public int BoxId { get; set; }
    }

    public class Characteristic
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("value")]
        public Object Value { get; set; }
    }

    public class Cursor
    {
        [JsonPropertyName("updatedAt")]
        public DateTime? UpdatedAt { get; set; }

        [JsonPropertyName("nmID")]
        public int? NmID { get; set; }

        [JsonPropertyName("total")]
        public int? Total { get; set; }
    }

    public class Dimensions
    {
        [JsonPropertyName("length")]
        public int? Length { get; set; }

        [JsonPropertyName("width")]
        public int? Width { get; set; }

        [JsonPropertyName("height")]
        public int? Height { get; set; }
    }

    public class Photo
    {
        [JsonPropertyName("516x288")]
        public string _516x288 { get; set; }

        [JsonPropertyName("big")]
        public string Big { get; set; }

        [JsonPropertyName("small")]
        public string Small { get; set; }
    }

    public class Size
    {
        [JsonPropertyName("chrtID")]
        public int? ChrtID { get; set; }

        [JsonPropertyName("techSize")]
        public string TechSize { get; set; }

        [JsonPropertyName("wbSize")]
        public string WbSize { get; set; }

        [JsonPropertyName("skus")]
        public List<string> Skus { get; set; }
    }

    public class Tag
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("color")]
        public string Color { get; set; }
    }


}
