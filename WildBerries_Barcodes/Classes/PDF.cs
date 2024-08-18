using PdfSharp.Drawing;
using PdfSharp.Pdf;
using WBBarcodes.Classes;

namespace WildBerries_Barcodes.Scripts
{
    internal class PDF: File<PdfDocument>
    {
        public PDF()
        {
            Item = new PdfDocument();
            Item.Info.Title = "created with WbBarcode";
        }

        /// <summary>
        /// Добавляет страницу в PDF файл, без сохранения
        /// </summary>
        /// <param name="panel">изображение</param>
        /// <param name="quantity">Количество повторений страниц, по умолчанию 1</param>
        public void AddPage(Panel panel, int quantity = 1)
        {
            var image = GetPanelAsImage(panel);
            var doubleQuantity = quantity * 2;

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var memStream = new MemoryStream();
            image.Save(memStream, System.Drawing.Imaging.ImageFormat.Png);

            for (int i = 0; i < doubleQuantity; i++)
            {
                var page = Item.AddPage();
                page.Width = panel.Width;
                page.Height = panel.Height;

                XGraphics gfx = XGraphics.FromPdfPage(page);
                gfx.DrawImage(XImage.FromStream(memStream), 0, 0, page.Width, page.Height);
            }
        }

        public void AddPage(OzonProduct product, int quantity)
        {
            for(int i = 0; i < quantity; i++)
            {
                var barcodeImg = Barcode.GetImage(product.Barcode);
                var page = Item.AddPage();
                page.Width = 580;
                page.Height = 300;

                XGraphics gfx = XGraphics.FromPdfPage(page);

                MemoryStream strm = new MemoryStream();
                barcodeImg.Save(strm, System.Drawing.Imaging.ImageFormat.Png);

                XImage xfoto = XImage.FromStream(strm);
                gfx.DrawImage(xfoto, 0, page.Width.Value / 2 - xfoto.Width / 2, 600, 180);
                gfx.DrawString(product.Barcode, new XFont("Arial", 20), XBrushes.Black, new XRect(page.Width.Value / 2 - 80, 230, 20, 0), XStringFormats.BaseLineLeft);
                gfx.DrawString($"Артикуль: {product.Articul}", new XFont("Arial", 25), XBrushes.Black, new XRect(30, 265, 20, 0), XStringFormats.BaseLineLeft);
                gfx.DrawString(product.Name, new XFont("Arial", 20), XBrushes.Black, new XRect(30, 285, 14, 0), XStringFormats.BaseLineLeft);
            }

        }

        public void AddPage(string name, string barcode)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            var barcodeImg = Barcode.GetImage(barcode);
            var page = Item.AddPage();
            page.Width = 580;
            page.Height = 300;

            XGraphics gfx = XGraphics.FromPdfPage(page);

            MemoryStream strm = new MemoryStream();
            barcodeImg.Save(strm, System.Drawing.Imaging.ImageFormat.Png);

            XImage xfoto = XImage.FromStream(strm);
            gfx.DrawImage(xfoto, 20, 15, 500, 180);
            gfx.DrawString(barcode, new XFont("Arial", 20), XBrushes.Black, new XRect(page.Width.Value / 2 - 80, 220, 20, 0), XStringFormats.BaseLineLeft);
            gfx.DrawString(name, new XFont("Arial", 40), XBrushes.Black, new XRect(30, 270, 14, 0), XStringFormats.BaseLineLeft);
        }

        public override void Save(string folderPath)
        {
            if (Item.PageCount == 0)
            {
                MessageBox.Show("Нельзя сохранить файл с отсутствующими страницами", "Ошибка сохранения PDF", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var filePath = Path.Combine(folderPath, "tags.pdf");
            Item.Save(filePath);
        }

        public Bitmap GetPanelAsImage(Panel FinalPanel)
        {
            var btm = new Bitmap(FinalPanel.Width, FinalPanel.Height);
            FinalPanel.DrawToBitmap(btm, new Rectangle(0, 0, btm.Width, btm.Height));

            return btm;
        }
    }
}
