using System.Text.Json.Serialization;

namespace WildBerries_Barcodes
{
    public class Characteristic
    {
        [JsonPropertyName("Ширина упаковки")]
        public int PackageWidth { get; set; }

        [JsonPropertyName("Высота упаковки")]
        public int PackageHeight { get; set; }

        [JsonPropertyName("Длина упаковки")]
        public int PackageLength { get; set; }

        [JsonPropertyName("Рост модели на фото")]
        public int ModelHeight { get; set; }

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
        public int imtID { get; set; }
        public string @object { get; set; }
        public int objectID { get; set; }
        public int nmID { get; set; }
        public string vendorCode { get; set; }
        public List<string> mediaFiles { get; set; }
        public List<Size> sizes { get; set; }
        public List<Characteristic> characteristics { get; set; }
        public bool isProhibited { get; set; }
    }

    public class DataWB
    {
        public List<Data> data { get; set; }
        public bool error { get; set; }
        public string errorText { get; set; }
        public object additionalErrors { get; set; }
    }

    public class Size
    {
        public string techSize { get; set; }
        public List<string> skus { get; set; }
        public int chrtID { get; set; }
        public string wbSize { get; set; }
        public int price { get; set; }
    }


}
