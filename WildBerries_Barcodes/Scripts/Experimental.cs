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
        public static Task<Tag> AsyncPostRequest(DataRow row)
        {
            var task = new Task<Tag>(() =>
            {
                var tag = Excel.GetTagFromRow(row);
                return tag;
            });
            task.Start();
            return task;
        }

        async public static void DoEverything(Panel panel)
        {
            var path = Excel.ChooseFile();
            var excelRows = Excel.ReadFile(path);

            foreach (var row in excelRows)
            {
                var formatedRow = Excel.GetTagFromRow(row);
                if (formatedRow == null) continue;

                if (formatedRow.Data[0] == null)
                {
                    MessageBox.Show("Ошибка с количесвом этикеток", "Неверное значение",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                TagSize.RenderPanel(panel, formatedRow);
                PDF.AddPage(panel, formatedRow.Data[0].Count);
            }

            PDF.Save();
            File.Delete(path);
        }
    }
}
