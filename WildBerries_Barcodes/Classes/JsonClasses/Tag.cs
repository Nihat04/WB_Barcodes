using System.Text.Json.Serialization;

namespace WildBerries_Barcodes.Scripts.JsonClasses
{
    public class Characteristic
    {
        [JsonPropertyName("Ширина упаковки")]
        public int? PackageWidth { get; set; }

        [JsonPropertyName("Высота упаковки")]
        public int? PackageHeight { get; set; }

        [JsonPropertyName("Длина упаковки")]
        public int? PackageLength { get; set; }

        [JsonPropertyName("Рост модели на фото")]
        public int? ModelHeight { get; set; }

        [JsonPropertyName("Тип посадки")]
        public List<string> SitType { get; set; }

        [JsonPropertyName("Модель джинсов")]
        public List<string> JeansType { get; set; }

        [JsonPropertyName("Тип ростовки")]
        public List<string> HeightType { get; set; }

        [JsonPropertyName("Декоративные элементы")]
        public List<string> DecorativeElements { get; set; }

        [JsonPropertyName("Любимые герои")]
        public List<string> FavoriteCharacters { get; set; }

        [JsonPropertyName("Страна производства")]
        public List<string> Country { get; set; }

        [JsonPropertyName("Параметры модели на фото (ОГ-ОТ-ОБ)")]
        public List<string> ModelParametres { get; set; }

        [JsonPropertyName("Тип карманов")]
        public List<string> PocketType { get; set; }

        [JsonPropertyName("Уход за вещами")]
        public List<string> ItemCate { get; set; }

        [JsonPropertyName("Особенности модели")]
        public List<string> Specifies { get; set; }

        [JsonPropertyName("Размер на модели")]
        public List<string> OnModelSize { get; set; }

        [JsonPropertyName("Вид застежки")]
        public List<string> CloseType { get; set; }
    }

    public class Data
    {
        [JsonPropertyName("imtID")]
        public int ImtID { get; set; }

        [JsonPropertyName("object")]
        public string? Object { get; set; }

        [JsonPropertyName("objectID")]
        public int ObjectID { get; set; }

        [JsonPropertyName("nmID")]
        public int WbArticul { get; set; }

        [JsonPropertyName("vendorCode")]
        public string? SellerArticul { get; set; }

        [JsonPropertyName("mediaFiles")]
        public List<string>? MediaFiles { get; set; }

        [JsonPropertyName("sizes")]
        public List<Size> Sizes { get; set; }

        private Characteristic? characteristics;
        [JsonPropertyName("characteristics")]
        public List<Characteristic>? Characteristics
        {
            get
            {
                return new List<Characteristic>() { characteristics };
            }

            set
            {
                if(value == null) throw new ArgumentNullException("value");

                characteristics = new Characteristic();

                foreach(var characteristic in value)
                {
                    if(characteristic == null) continue;

                    var props = characteristic.GetType().GetProperties();

                    foreach (var propertyInfo in characteristic.GetType().GetProperties())
                    {
                        var propertyValue = propertyInfo.GetValue(characteristic);
                        if (propertyValue == null) continue;

                        propertyInfo.SetValue(this.characteristics, propertyValue);
                        break;
                    }

                }
            }
        }

        [JsonPropertyName("isProhibited")]
        public bool IsProhibited { get; set; }

        public int Count { get; set; }
        public string Color { get; set; }
        public int CartboxNumber { get; set; }
    }

    public class Tag
    {
        [JsonPropertyName("data")]
        public List<Data>? Data { get; set; }

        [JsonPropertyName("error")]
        public bool Error { get; set; }

        [JsonPropertyName("errorText")]
        public string? ErrorText { get; set; }

        [JsonPropertyName("additionalErrors")]
        public object? AdditionalErrors { get; set; }

        public TagSize? Size { get; set; }

        public void FilterData(string sellerArticul)
        {
            foreach(var data in this.Data)
            {
                if (data.SellerArticul != sellerArticul) continue;

                this.Data = new List<Data> { data };
                break;
            }
        }

        public void FilterSize(string size)
        {
            this.Data[0].Sizes = new List<Size> { this.Data[0].Sizes.Find(el => String.Equals(el.SellerSize, size)) };
        }
    }

    public class Size
    {
        [JsonPropertyName("techSize")]
        public string? SellerSize { get; set; }

        [JsonPropertyName("skus")]
        public List<string>? Barcode { get; set; }

        [JsonPropertyName("chrtID")]
        public int ChrtID { get; set; }

        [JsonPropertyName("wbSize")]
        public string? WbSize { get; set; }

        [JsonPropertyName("price")]
        public int Price { get; set; }
    }


}
