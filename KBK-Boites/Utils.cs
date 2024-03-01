
namespace Boites
{
    public static class Utils
    {
        public const int DEFAULT_POSITION = -1;
        public static string[] SplitLines(this string text)
        {
            return text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        }
        public static int FindIf(string s, Func<char, bool> pred)
        {
            int pos = 0;
            for (; pos < s.Length && !pred(s[pos]); pos++)
                ;
            return pos;
        }
    }
}
