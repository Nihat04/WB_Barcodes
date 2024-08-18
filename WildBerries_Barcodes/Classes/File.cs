using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBBarcodes.Classes
{
    public abstract class File<T>
    {
        public T Item { get; set; }
        public string FilePath { get; set; }

        public string GenerateFileName()
        {
            return DateTime.Now.ToString("dd.MM.yyyy_HH-mm");
        }
        public abstract void Save(string folderPath);
    }
}
