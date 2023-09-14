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
                    newChineseStr += s.Substring(0, 1);
                }
                catch (Exception)
                {
                    newChineseStr += item;
                }
            }
            return newChineseStr;
        }
        /// <summary>
        /// 检测字符是不是英文（unicode编码中字符的第一个字节是0那他就是英文字符。不是0他就可能是除了英文字符之外的很多种语言的文字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsEnglishWord(string str)
        {
            bool flag = true;
            UnicodeEncoding u = new UnicodeEncoding();
            byte[] byteArr = u.GetBytes(str);
            for (int i = 0; i < byteArr.Length; i++)
            {
                i++;
                if (byteArr[i] != 0)
                {
                    flag = false;
                    return false;
                }
            }
            return flag;
        }
    }
}
