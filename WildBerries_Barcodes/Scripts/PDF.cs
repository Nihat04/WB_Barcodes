﻿using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Diagnostics;

namespace WildBerries_Barcodes.Scripts
{
    internal static class PDF
    {

        private static PdfDocument PDFile = new PdfDocument();

        public static void CreateNew()
        {
            PDFile = new PdfDocument();
        }

        /// <summary>
        /// Добавляет страницу в PDF файл, без сохранения
        /// </summary>
        /// <param name="panel">изображение</param>
        /// <param name="quantity">Количество повторений страниц, по умолчанию 1</param>
        public static void AddPage(Panel panel, int quantity = 1)
        {
            var image = GetPanelAsImage(panel);

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var memStream = new MemoryStream();
            image.Save(memStream, System.Drawing.Imaging.ImageFormat.Png);

            for (int i = 0; i < quantity; i++)
            {
                var page = PDFile.AddPage();
                page.Width = panel.Width;
                page.Height = panel.Height;

                XGraphics gfx = XGraphics.FromPdfPage(page);
                gfx.DrawImage(XImage.FromStream(memStream), 0, 0, page.Width, page.Height);
            }
        }

        public static void Save()
        {
            if (PDFile.PageCount == 0)
            {
                MessageBox.Show("Нельзя сохранить файл с отсутствующими страницами", "Ошибка сохранения PDF", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var fileName = DateTime.Now.ToString("dd.MM.yyyy_t") + ".pdf";

            if (!Directory.Exists(@"PDF's folder"))
                Directory.CreateDirectory("PDF's folder");

            var filePath = @$"PDF's folder\{fileName}";
            PDFile.Save(filePath);

            Process.Start(new ProcessStartInfo()
            {
                FileName = filePath,
                UseShellExecute = true,
            });
        }

        public static Bitmap GetPanelAsImage(Panel FinalPanel)
        {
            var btm = new Bitmap(FinalPanel.Width, FinalPanel.Height);
            FinalPanel.DrawToBitmap(btm, new Rectangle(0, 0, btm.Width, btm.Height));

            return btm;
        }
    }
}
