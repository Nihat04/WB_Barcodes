using PdfSharp.Charting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildBerries_Barcodes.Scripts
{
    public class Excel
    {
        public static void AddColumn(string barcode, int count)
        {
            var path = "template.txt";
            var newText = $"{barcode} {count}\n";
            if (File.Exists(path))
            {
                var file = File.ReadAllText(path);
                newText = file + newText;
            }

            File.WriteAllText(path, newText);
        }
    }
}
