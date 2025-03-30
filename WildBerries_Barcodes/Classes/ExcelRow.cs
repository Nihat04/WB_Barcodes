using System.Data;
using WBBarcodes.Exceptions;

namespace WBBarcodes.Classes
{
    public class ExcelRow
    {
        public int? Articul { get; set; }
        public string? SellerArticul { get; set; }
        public bool IsSellerArticul { get; set; }
        public string? Size { get; set; }
        public int Count { get; set; }
        public string? BoxId { get; set; }

        public ExcelRow(int? articul, string? sellerArticul, string? size, int count, string? boxId)
        {
            if (sellerArticul == null && articul == null)
                throw new ArgumentNullException("At least one of the articuls must be defined.");

            Articul = articul;
            SellerArticul = sellerArticul;
            Size = size;
            Count = count;
            BoxId = boxId;

            IsSellerArticul = sellerArticul != null;
        }

        public ExcelRow(string? articul, string? sellerArticul, string? size, int count, string? boxId)
            : this(ParseArticul(articul), sellerArticul, size, count, boxId) { }

        public ExcelRow(DataRow row)
            : this(
                row.ItemArray[0]?.ToString(),
                row.ItemArray[1]?.ToString(),
                row.ItemArray[2]?.ToString(),
                ParseCount(row.ItemArray[3]?.ToString()),
                row.ItemArray[4]?.ToString())
        { }

        private static int? ParseArticul(string? articul)
        {
            if (string.IsNullOrEmpty(articul)) return null;

            if (!int.TryParse(articul, out int parsedArticul))
                throw new OnRunException("Ошибка Артикуля", "Артикуль (МП) не числовое значение");

            return parsedArticul;
        }

        private static int ParseCount(string? countStr)
        {
            if (!int.TryParse(countStr, out int count))
                throw new OnRunException("Ошибка Количества", "Количество не является числом");

            return count;
        }
    }
}
