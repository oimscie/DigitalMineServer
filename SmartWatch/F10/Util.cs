using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWatch.F10
{
    public static class Util
    {
        /// <summary>
        ///去除字符
        /// </summary>
        /// <param name="val">原字符串</param>
        /// <param name="start">开头要去除的字符</param>
        /// <param name="end">末尾要去除的字符</param>
        /// <returns></returns>
        public static string TrimString(string val, char start, char end)
        {
            return val.TrimStart(start).TrimEnd(end);
        }
    }
}