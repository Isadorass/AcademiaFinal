using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace BussinesLogicalLayer
{
     public class Standardization
     {
         public static string StandardizeString(string palavra)
         {
             if (string.IsNullOrWhiteSpace(palavra))
             {
                 return "";
             }

             palavra = palavra.Trim();
             palavra = Regex.Replace(palavra, @"\s+", " ");

             TextInfo textInfo = Thread.CurrentThread.CurrentCulture.TextInfo;
             palavra = textInfo.ToTitleCase(palavra);

             return palavra;
         }
     }
}
