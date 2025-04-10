﻿using WildBerries_Barcodes.Scripts.JsonClasses;

namespace WildBerries_Barcodes.Scripts
{
    public static class Barcode
    {
        public static void SetImage(string barcodeText, PictureBox barcodeBox)
        {
            var barcodeNumbers = "888888888888";

            if (!(barcodeText.Length >= barcodeNumbers.Length && barcodeText.Length <= barcodeNumbers.Length + 1))
                return;

            var barcode = new BarcodeLib.Barcode();
            barcodeNumbers = barcodeText;

            barcodeBox.Image = barcode.Encode(BarcodeLib.TYPE.CODE128, barcodeNumbers, Color.Black, Color.Transparent, barcodeBox.Width, barcodeBox.Height);
        }

        public static Image GetImage2(string barcode, PictureBox barcodeBox)
        {
            var barcodeImg = GetImage(barcode);
            MemoryStream strm = new MemoryStream();
            barcodeImg.Save(strm, System.Drawing.Imaging.ImageFormat.Png);

            Image xfoto = Image.FromStream(strm);
            barcodeBox.Image = xfoto;
            return xfoto;
        }

        public static Image GetImage(string barcodeText)
        {
            var barcode = new BarcodeLib.Barcode();
            return barcode.Encode(BarcodeLib.TYPE.CODE128, barcodeText, Color.Black, Color.Transparent, 415, 49);
        }
    }
}