using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WildBerries_Barcodes.Scripts.JsonClasses;
using WildBerries_Barcodes.Scripts;
using System.Data;

namespace WBBarcodes.Scripts
{
    public class Experimental
    {
        public static Task<Tag> AsyncPostRequest(Panel panel, DataRow row)
        {
            //var task = new Task<Tag>(() =>
            //{
            //    return null;
            //});
            //task.Start();
            //return task;
        }

        async public static void DoEverything(Panel panel, string path)
        {
            //var excelRows = Excel.ReadFile(path);

            //foreach (var row in excelRows)
            //{
            //    var newData = await AsyncPostRequest(panel, row);
            //    if (newData == null) continue;

            //    var controls = panel.Controls;

            //    foreach (Control control in controls)
            //    {
            //        var type = control.GetType().Name;

            //        switch (type)
            //        {
            //            case "Label":
            //                var controlAsLabel = control as Label;

            //                if (!newData.ContainsKey(controlAsLabel.Name))
            //                    continue;

            //                if (controlAsLabel.Text.Contains(':'))
            //                    controlAsLabel.Text = $"{controlAsLabel.Text.Split(':')[0]}: {newData[controlAsLabel.Name]}";
            //                else
            //                    controlAsLabel.Text = newData[controlAsLabel.Name].ToString();
            //                break;

            //            case "PictureBox":
            //                var controlAsPictureBox = control as PictureBox;

            //                if (controlAsPictureBox.Name.ToLower() != "barcodeimg")
            //                    break;

            //                Logic.BarcodeImage(newData["BarcodeDigits"].ToString(), controlAsPictureBox);
            //                break;
            //        }
            //    }

            //    PDF.AddPage(panel, int.Parse(newData["count"].ToString()) * 2);
            //}
        }

        public static void testc(Panel imgPanel)
        {
            //var panel imgPanel

            //var path = Excel.ChooseFile();
            //var excelRows = Excel.ReadFile(path);

            //foreach (var row in excelRows)
            //{
            //    var formatedRow = Excel.FormatRow(row);
            //    if (formatedRow == null) continue;

            //    if (formatedRow.Data[0] == null)
            //    {
            //        MessageBox.Show("Ошибка с количесвом этикеток", "Неверное значение",
            //        MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }

            //    TagSize.ApplyToPanel(ImagePanel, formatedRow);
            //    PDF.AddPage(ImagePanel, formatedRow.Data[0].Count);
            //}

            //PDF.Save();
            //File.Delete(path);
        }
    }
}
