using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WBBarcodes.Classes.JsonClasses;
using WBBarcodes.Properties;
using WildBerries_Barcodes.Scripts;

namespace WBBarcodes.Classes
{
    public static class FormsManager
    {
        public static Panel ClonePanel(Panel panel)
        {
            var clonedPanel = new Panel();

            clonedPanel.Size = panel.Size;
            clonedPanel.BackColor = System.Drawing.Color.White;
            clonedPanel.BorderStyle = panel.BorderStyle;

            var panelControls = panel.Controls;

            foreach (Control control in panelControls)
            {
                if (control.GetType().Name == "Label")
                {
                    var controlAsLabel = control as Label;
                    var clonedControl = new Label();

                    clonedControl.Text = controlAsLabel.Text;
                    clonedControl.Font = controlAsLabel.Font;
                    clonedControl.Name = controlAsLabel.Name;
                    clonedControl.BackColor = controlAsLabel.BackColor;
                    clonedControl.TextAlign = controlAsLabel.TextAlign;
                    clonedControl.AutoSize = controlAsLabel.AutoSize;
                    clonedControl.Location = controlAsLabel.Location;
                    clonedControl.ForeColor = controlAsLabel.ForeColor;

                    clonedPanel.Controls.Add(clonedControl);
                }
                else if (control.GetType().Name == "PictureBox")
                {
                    var controlAsPictureBox = control as PictureBox;
                    var clonedControl = new PictureBox();

                    clonedControl.Size = controlAsPictureBox.Size;
                    clonedControl.MaximumSize = controlAsPictureBox.MaximumSize;
                    clonedControl.Name = controlAsPictureBox.Name;
                    clonedControl.BackColor = controlAsPictureBox.BackColor;
                    clonedControl.Location = controlAsPictureBox.Location;

                    if (clonedControl.Name == "EAC")
                    {
                        clonedControl.SizeMode = PictureBoxSizeMode.StretchImage;
                        clonedControl.Image = Resources.EAC;
                    }

                    clonedPanel.Controls.Add(clonedControl);
                }
            }

            //TagSize.Change(clonedPanel);
            return clonedPanel;
        }

        public static void RenderPanel(Panel panel, Card card)
        {
            Product product = new Product(card);
            RenderPanel(panel, product);
        }

        public static void RenderPanel(Panel panel, OzonProduct product)
        {
            Product prod = new Product(product);
            RenderPanel(panel, prod);
        }

        public static void RenderPanel(Panel panel, Product product)
        {
            Func<string, string, string> formatFunc = (constText, replaceText) =>
                constText.Contains(':') ? $"{constText.Split(':')[0]}: {replaceText}" : replaceText;

            var controls = panel.Controls;
            foreach (Control control in controls)
            {
                switch (control.Name)
                {
                    case "BarcodeDigits":
                        if(product.Barcode != null) control.Text = formatFunc(control.Text, product.Barcode);
                        break;

                    case "BarcodeIMG":
                        if (product.Barcode != null) Barcode.SetImage(product.Barcode, control as PictureBox);
                        break;

                    case "Size":
                        if (product.Size != null) control.Text = formatFunc(control.Text, product.Size);
                        break;

                    case "Articul":
                        if (product.Articul != null) control.Text = formatFunc(control.Text, product.Articul.ToString());
                        break;

                    case "Country":
                        if (product.Country != null) control.Text = formatFunc(control.Text, product.Country);
                        break;

                    case "Type":
                        if (product.Barcode != null) control.Text = formatFunc(control.Text, product.Type);
                        break;
                }
            }
        }
    }
}
