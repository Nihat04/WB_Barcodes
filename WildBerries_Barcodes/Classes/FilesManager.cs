using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using WBBarcodes.Api;
using WBBarcodes.Classes.JsonClasses;
using WBBarcodes.Exceptions;
using WildBerries_Barcodes.Scripts;

namespace WBBarcodes.Classes
{
    public enum FileType
    {
        Pdf,
        Excel
    }

    public class FilesManager
    {
        private List<Classes.File<object>> Files {  get; set; }
        public static void GenerateOzonFiles(DataRow[] excelRows, IProgress<int> progress, Panel panel, OzonProducts productsList)
        {
            var pdf = new PDF();

            foreach (var row in excelRows)
            {
                if (row.ItemArray[0].ToString().Equals("Артикул MP")) continue;
                try
                {
                    progress.Report(1);
                    var articul = row.ItemArray[0].ToString();
                    if (row.ItemArray[0].ToString().Equals("")) continue;
                    var card = productsList.result.Find(product => product.offer_id.Equals(articul));
                    if (card == null) throw new OnRunException("There is no such product", $"cannot find product with articul {row.ItemArray[0].ToString()}");
                    var count = int.Parse(row.ItemArray[2].ToString());
                    FormsManager.RenderPanel(panel, card);
                    pdf.AddPage(panel, count);
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

        public static void CreateWbFiles(ExcelRow[] rows, Panel panel, Action? iterationFunc = null)
        {
            var pdf = new PDF();
            var template = new Excel();
            var shkTemplate = new Excel(true);
            var productsList = WildBerriesAPI.getAllProducts();

            foreach (var row in rows)
            {
                try
                {
                    if (iterationFunc != null) iterationFunc();

                    if (row == null) continue;

                    var card = Excel.GetCardFromExcelRow(row, productsList);

                    if (card == null) continue;

                    FormsManager.RenderPanel(panel, card);
                    pdf.AddPage(panel, card.TagsCount);
                    template.AddColumn(card.RequiredSize!.Skus[0], card.TagsCount);
                    shkTemplate.AddColumn(card.RequiredSize.Skus[0], card.TagsCount, card.BoxId);
                }
                catch (OnRunException exception)
                {
                    MessageBox.Show(exception.Message, exception.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (TimeoutException)
                {
                    MessageBox.Show("Время запроса вышло", "Ошибка запроса сервера", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }

            Save(template, shkTemplate, pdf);
        }

        private static void Save(Excel template1, Excel template2, PDF pdf)
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

        /// <summary>
        /// open excel file choose dialog
        /// </summary>
        /// <returns>created temporary file path</returns>
        public static string ChooseExcelFile()
        {
            const string fileFilter = "Excel WorkBook|*.xlsx";
            
            return ChooseFile(fileFilter);
        }

        public static string[] ChooselPdfFiles()
        {
            const string folderName = "Combined_files";

            if (!Directory.Exists(folderName))
                Directory.CreateDirectory(folderName);

            using (OpenFileDialog dialog = new() { Filter = "PDF File (*.pdf)|*.pdf", ValidateNames = true, Multiselect = true })
            {
                if (!(dialog.ShowDialog() == DialogResult.OK))
                    return null;

                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                return dialog.FileNames;
            }
        }

        private static string ChooseFile(string filter)
        {
            if (!Directory.Exists("temp"))
                Directory.CreateDirectory("temp");

            const string temporaryFolderPath = @"temp\";

            using (OpenFileDialog dialog = new() { Filter = filter, ValidateNames = true })
            {
                if (!(dialog.ShowDialog() == DialogResult.OK))
                    return "Error";

                var fileFullPath = Path.Combine(temporaryFolderPath, dialog.SafeFileName);

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                File.Copy(dialog.FileName, fileFullPath, true);

                return fileFullPath;
            }
        }
    }
}
