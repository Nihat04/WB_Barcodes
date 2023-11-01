using ExcelDataReader;
using System.Data;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using WildBerries_Barcodes.Scripts.JsonClasses;

namespace WildBerries_Barcodes.Scripts
{
    public static class Logic
    {
        public static void ApplyData(Panel panel, List<Dictionary<string, object>> excelData)
        {
            var controls = panel.Controls;

            foreach (var row in excelData)
            {
                foreach (Control control in controls)
                {
                    var type = control.GetType().Name;

                    switch (type)
                    {
                        case "Label":
                            var controlAsLabel = control as Label;

                            if (!row.ContainsKey(controlAsLabel.Name))
                                continue;

                            if (controlAsLabel.Text.Contains(':'))
                                controlAsLabel.Text = $"{controlAsLabel.Text.Split(':')[0]}: {row[controlAsLabel.Name]}";
                            else
                                controlAsLabel.Text = row[controlAsLabel.Name].ToString();
                            break;

                        case "PictureBox":
                            var controlAsPictureBox = control as PictureBox;

                            if (controlAsPictureBox.Name.ToLower() != "barcodeimg")
                                break;

                            BarcodeImage(row["BarcodeDigits"].ToString(), controlAsPictureBox);
                            break;
                    }
                }

                PDF.AddPage(panel, int.Parse(row["count"].ToString()) * 2);
            }
        }

        public static object GetCountry(List<Characteristic> characteristics)
        {
            foreach (Characteristic characteristic in characteristics)
            {
                if (!Equals(characteristic.Country, null))
                    return characteristic.Country[0];
            }
            return "Unknown";
        }

        public static void BarcodeImage(string barcodeText, PictureBox barcodeBox)
        {
            var barcodeNumbers = "888888888888";

            if (!(barcodeText.Length >= barcodeNumbers.Length && barcodeText.Length <= barcodeNumbers.Length + 1))
                return;

            var barcode = new BarcodeLib.Barcode();
            barcodeNumbers = barcodeText;

            barcodeBox.Image = barcode.Encode(BarcodeLib.TYPE.EAN13, barcodeNumbers, Color.Black, Color.Transparent, barcodeBox.Width, barcodeBox.Height);
        }

        public static string GetBarcode(object requiredSize, List<WildBerries_Barcodes.Scripts.JsonClasses.Size> sizes)
        {
            foreach (var size in sizes)
            {
                if (Equals(requiredSize.ToString(), size.techSize.ToString()))
                    return size.skus[0].ToString();
            }
            return "unknown size";
        }

    }
}