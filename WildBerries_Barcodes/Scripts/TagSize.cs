using System.Configuration;

namespace WildBerries_Barcodes.Scripts
{
    public static class TagSize
    {
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
    }
}
