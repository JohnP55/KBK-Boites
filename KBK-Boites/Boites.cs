using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBK_Boites
{
    public abstract class ABCBoite
    {
        protected ABCBoite? InnerBox { get; set; }
        public virtual int Width { get; protected set; }
        public virtual int Height { get; protected set; }
    }
    public class Mono : ABCBoite
    {
        public string Text { get; set; }
        public Mono(string text)
        {
            InnerBox = null;
            Text = text;
            (Width, Height) = GetMinSize();
        }
        private (int, int) GetMinSize()
        {
            string[] lines = Text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            int width = lines.Max(x => x.Length);
            int height = lines.Length;
            return (width, height);
        }
    }

    public class Boite : ABCBoite
    {
        public Boite()
        {
            InnerBox = new Mono("");
        }
        public Boite(string text)
        {
            InnerBox = new Mono(text);
        }
        public Boite(ABCBoite boite)
        {
            // TODO: faire une copie d'une boite récursivement
            InnerBox = boite;
        }
        public override string ToString()
        {
            // TODO
            return "";
        }
    }
}
