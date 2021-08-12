using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogicalLayer
{
    public static class StringExtensions
    {
        public static int ToInt(this string valor)
        {
            int temp;
            int.TryParse(valor, out temp);
            return temp;

        }

        public static double ToDouble(this string valor)
        {
            double temp;
            double.TryParse(valor, out temp);
            return temp;
        }

    }
}
