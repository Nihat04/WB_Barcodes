using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBBarcodes.Exceptions
{
    public class OnRunException : Exception
    {
        public OnRunException() : base("Ошибка")
        {
            Title = "Ошибка продукта";
        }

        public OnRunException(string title, string message) : base(message)
        {
            Title = title;
        }

        public readonly string Title;
    }
}
