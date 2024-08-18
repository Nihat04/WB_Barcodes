using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBBarcodes.Exceptions;
using WBBarcodes.Scripts;
using WildBerries_Barcodes.Scripts;

namespace WBBarcodes.Classes
{
    public class FileManager
    {

        public static void GenerateOzonFiles(DataRow[] excelRows, IProgress<int> progress)
        {
            var pdf = new PDF();
            var productsList = OzonExcel.GetProducts();

            foreach (var row in excelRows)
            {
                if (row.ItemArray[0].ToString().Equals("Артикуль WB")) continue;
                try
                {
                    progress.Report(1);
                    if (row.ItemArray[0].ToString().Equals("")) continue;
                    var card = OzonExcel.GetProducts(row, productsList);
                    if (card == null) throw new OnRunException("There is no such product", $"cannot find product with articul {row.ItemArray[0].ToString()}");
                    var count = int.Parse(row.ItemArray[2].ToString());
                    pdf.AddPage(card, count * 2);
                }
                catch (OnRunException exception)
                {
                    progress.Report(-1);
                    MessageBox.Show(exception.Message, exception.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (TimeoutException)
                {
                    progress.Report(-1);
                    MessageBox.Show("Время запроса вышло", "Ошибка запроса сервера", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }

            pdf.Save("OzonTags");
        }

        public static void GenerateWbFiles(DataRow[] excelRows, IProgress<int> progress, Panel panel)
        {
            var pdf = new PDF();
            var template = new ExcelTemplate();
            var shkTemplate = new ExcelTemplate(true);
            var productsList = RestAPI.getAllProducts();

            foreach (var row in excelRows)
            {
                try
                {
                    progress.Report(1);
                    var card = ExcelReader.getCardFromProducts(row, productsList);

                    if (card == null) continue;

                    TagSize.RenderPanel(panel, card);
                    pdf.AddPage(panel, card.TagsCount);
                    template.AddColumn(card.RequiredSize.Skus[0], card.TagsCount);
                    shkTemplate.AddColumn(card.RequiredSize.Skus[0], card.TagsCount, card.BoxId);
                }
                catch (OnRunException exception)
                {
                    progress.Report(-1);
                    MessageBox.Show(exception.Message, exception.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (TimeoutException)
                {
                    progress.Report(-1);
                    MessageBox.Show("Время запроса вышло", "Ошибка запроса сервера", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }

            Save(template, shkTemplate, pdf);
        }

        private static void Save(ExcelTemplate template1, ExcelTemplate template2, PDF pdf)
        {
            const string folderPath = @"Tags";

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileFolderName = DateTime.Now.ToString("yyyy.MM.dd_HH-mm");
            Directory.CreateDirectory(Path.Combine(folderPath, fileFolderName));

            string fileFolder = Path.Combine(folderPath, fileFolderName);

            pdf.Save(fileFolder);
            template1.Save(fileFolder);
            template2.Save(fileFolder);

            Process.Start("explorer.exe", fileFolder);
        }
    }
}
