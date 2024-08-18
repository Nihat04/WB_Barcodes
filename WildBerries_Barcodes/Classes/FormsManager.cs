using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            TagSize.Change(clonedPanel);
            return clonedPanel;
        }
    }
}
