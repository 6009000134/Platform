using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.International.Converters.PinYinConverter;

namespace MyPlatform.Common
{
    public class PinYinConverter
    {

        public static string GetPinYin(string Str)
        {
            string newChineseStr = "";
            foreach (char item in Str)
            {
                try
                {
                    ChineseChar c = new ChineseChar(item);
                    string s = c.Pinyins[0].ToString();
                    newChineseStr += s.Substring(0, s.Length - 1);
                }
                catch (Exception)
                {
                    newChineseStr += item;
                }
            }
            return newChineseStr;
        }
    }
}
