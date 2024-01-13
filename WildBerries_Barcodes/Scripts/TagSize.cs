using System.Configuration;
using WildBerries_Barcodes.Scripts.JsonClasses;

namespace WildBerries_Barcodes.Scripts
{
    public class TagSize
    {
        public string FontName { get; set; }
        public int PageWidth { get; set; }
        public int PageHeight { get; set; }

        public TagSize(string fontName)
        {
            FontName = fontName;
        }

        public static void Change(Panel panel)
        {
            var controls = GetControls(panel);
        
            controls["About"].Location = new Point(2, panel.Height - controls["About"].Height - 2);
            controls["Country"].Location = new Point(panel.Width - controls["Country"].Width - 2, panel.Height - controls["Country"].Height - 2);
            controls["Brand"].Location = new Point(2, 2);
            controls["Type"].Location = new Point(panel.Width / 2 - controls["Type"].Width / 2, 2);

            controls["BarcodeIMG"].Size = new System.Drawing.Size(panel.Width - 10, panel.Height / 4);
            controls["BarcodeIMG"].Location = new Point(
                panel.Width / 2 - controls["BarcodeIMG"].Width / 2,
                controls["Type"].Location.Y + controls["Type"].Height + 15
                );

            controls["BarcodeDigits"].Location = new Point(
                panel.Width / 2 - controls["BarcodeDigits"].Width / 2,
                controls["BarcodeIMG"].Location.Y + controls["BarcodeIMG"].Height + 3
            );

            controls["EAC"].Location = new Point(
                controls["EAC"].Location.X,
                controls["BarcodeDigits"].Location.Y
                );

            var freeSpace = controls["About"].Location.Y - controls["BarcodeDigits"].Location.Y + controls["BarcodeDigits"].Height;
            var compensation = controls["BarcodeDigits"].Location.Y + controls["BarcodeDigits"].Height;

            controls["Size"].Location = new Point(2, compensation + Convert.ToInt32(freeSpace * (1.0 / 4)) - controls["Size"].Height - 5);
            controls["Articul"].Location = new Point(2, compensation + Convert.ToInt32(freeSpace * (2.0 / 4)) - controls["About"].Height);
            controls["Color"].Location = new Point(2, compensation + Convert.ToInt32(freeSpace * (3.0 / 4)) - Convert.ToInt32(controls["Size"].Height * 1.2));
        }

        private static Dictionary<string, Control> GetControls(Panel panel)
        {
            var dictionary = new Dictionary<string, Control>();
            var controls = panel.Controls;

            foreach (Control control in controls)
            {
                var Name = control.GetType().Name;
                switch (Name)
                {
                    case "Label":
                        var controlAsLabel = control as Label;
                        dictionary.Add(controlAsLabel.Name, controlAsLabel);
                        break;
                    case "PictureBox":
                        var controlAsPictureBox = control as PictureBox;
                        dictionary.Add(controlAsPictureBox.Name, controlAsPictureBox);
                        break;
                }
            }

            return dictionary;
        }

        public static void RenderPanel(Panel panel, Tag row)
        {
            Func<string, string, string> formatFunc = (constText, replaceText) => 
                constText.Contains(':') ? $"{constText.Split(':')[0]}: {replaceText}": replaceText;

            var controls = panel.Controls;
            foreach (Control control in controls)
            {
                switch(control.Name)
                {
                    case "BarcodeDigits":
                        control.Text = formatFunc(control.Text, row.Data[0].Sizes[0].Barcode[0]);
                        break;

                    case "BarcodeIMG":
                        Barcode.GetImage(row.Data[0].Sizes[0].Barcode[0], control as PictureBox);
                        break;

                    case "Size":
                        control.Text = formatFunc(control.Text, row.Data[0].Sizes[0].SellerSize);
                        break;

                    case "Articul":
                        control.Text = formatFunc(control.Text, row.Data[0].WbArticul.ToString());
                        break;

                    case "Color":
                        control.Text = formatFunc(control.Text, row.Data[0].Color);
                        break;

                    case "Country":
                        control.Text = formatFunc(control.Text, row.Data[0].Characteristics[0].Country[0].ToString());
                        break;

                    case "Type":
                        control.Text = formatFunc(control.Text, row.Data[0].Object);
                        break;
                }
            }

            PDF.AddPage(panel, row.Data[0].Count);
        }
    }
}
