using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace WildBerries_Barcodes.Scripts
{
    internal class PDF
    {

        private PdfDocument PDFile;

        public PDF()
        {
            PDFile = new PdfDocument();
            PDFile.Info.Title = "created with WbBarcode";
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
                var page = PDFile.AddPage();
                page.Width = panel.Width;
                page.Height = panel.Height;

                XGraphics gfx = XGraphics.FromPdfPage(page);
                gfx.DrawImage(XImage.FromStream(memStream), 0, 0, page.Width, page.Height);
            }
        }

        public void Save(string folderPath)
        {
            if (PDFile.PageCount == 0)
            {
                MessageBox.Show("Нельзя сохранить файл с отсутствующими страницами", "Ошибка сохранения PDF", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var filePath = Path.Combine(folderPath, "tags.pdf");
            PDFile.Save(filePath);
        }

        public Bitmap GetPanelAsImage(Panel FinalPanel)
        {
            var btm = new Bitmap(FinalPanel.Width, FinalPanel.Height);
            FinalPanel.DrawToBitmap(btm, new Rectangle(0, 0, btm.Width, btm.Height));

            return btm;
        }
    }
}
