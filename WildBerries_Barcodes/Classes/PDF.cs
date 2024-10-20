using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.Advanced;
using System.IO;
using WBBarcodes.Classes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

        public void AddPageMark(PdfPage pdfPage, string title)
        {
            //PdfDictionary pageDictionary = pdfPage.Contents.Elements.GetDictionary(0);

            //PdfPage newPage = this.Item.AddPage();

            //newPage.Width = 560;
            //newPage.Height = 280;
            SaveImageFromFile(pdfPage);
        }

        private void SaveImageFromFile(PdfPage page)
        {
            PdfDictionary resources = page.Elements.GetDictionary("/Resources");
            var imageCount = 0;

            if( resources != null )
            {
                PdfDictionary xObjects = resources.Elements.GetDictionary("/XObject");

                if ( xObjects != null )
                {
                    ICollection<PdfItem> items = xObjects.Elements.Values;

                    foreach (PdfItem item in items)
                    {
                        PdfReference reference = item as PdfReference;
                        if ( reference != null )
                        {
                            PdfDictionary xObject = reference.Value as PdfDictionary;
                            var sybtype = xObject.Elements.GetString("/Subtype");

                            if ( xObject != null && xObject.Elements.GetString("/Subtype") == "/Image")
                            {
                                ExportImage(xObject, ref imageCount);
                            }
                        }
                    }
                }
            }
        }

        private static void ExportImage(PdfDictionary image, ref int count)
        {
            string filter = image.Elements.GetName("/Filter");
            switch (filter)
            {
                case "/DCTDecode":
                    ExportJpegImage(image, ref count);
                    break;

                case "/FlateDecode":
                    ExportAsPngImage(image, ref count);
                    break;
            }
        }

        private static void ExportJpegImage(PdfDictionary image, ref int count)
        {
            // Fortunately JPEG has native support in PDF and exporting an image is just writing the stream to a file.
            byte[] stream = image.Stream.Value;
            FileStream fs = new FileStream(String.Format("Image{0}.jpeg", count++), FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(stream);
            bw.Close();
        }

        private static void ExportAsPngImage(PdfDictionary image, ref int count)
        {
            int width = image.Elements.GetInteger(PdfImage.Keys.Width);
            int height = image.Elements.GetInteger(PdfImage.Keys.Height);
            int bitsPerComponent = image.Elements.GetInteger(PdfImage.Keys.BitsPerComponent);

            // TODO: You can put the code here that converts vom PDF internal image format to a Windows bitmap
            // and use GDI+ to save it in PNG format.
            // It is the work of a day or two for the most important formats. Take a look at the file
            // PdfSharp.Pdf.Advanced/PdfImage.cs to see how we create the PDF image formats.
            // We don't need that feature at the moment and therefore will not implement it.
            // If you write the code for exporting images I would be pleased to publish it in a future release
            // of PDFsharp.
        }

        public override void Save(string folderPath)
        {
            if (Item.PageCount == 0)
            {
                MessageBox.Show("Нельзя сохранить файл с отсутствующими страницами", "Ошибка сохранения PDF", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string fileName = GenerateFileName() + ".pdf";
            var filePath = Path.Combine(folderPath, fileName);
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
