using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace KBK_Boites
{
    public static class Utils
    {
        public const int DEFAULT_POSITION = -1;
        public static string[] SplitLines(this string text)
        {
            return text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        }
    }
}
