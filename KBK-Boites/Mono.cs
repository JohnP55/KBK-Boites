using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;

namespace Boites
{
    /// <summary>
    /// Box that only has a text element
    /// </summary>
    public class Mono : IBoite, IEnumerable<string>
    {
        public string Text { get; set; }
        public Mono(string text)
        {
            Text = text;
            (Width, Height) = MinSize();
        }
        protected override void ResizeChildren() { }
        protected override (int, int) MinSize()
        {
            if (Text.Length == 0) return (0, 0);

            string[] lines = Text.SplitLines();
            int width = lines.Max(x => x.Length);
            int height = lines.Length;
            return (width, height);
        }

        public override Mono Clone()
        {
            return new Mono(Text);
        }

        public override IEnumerator<string> GetEnumerator()
        {
            return new MonoEnumerator(this);
        }

        public override void Accepter(IVisiteur<IBoite> viz)
        {
            viz.Entrer();
            viz.Visiter(this, () => Console.Write($" {Height} x {Width}"));
            viz.Sortir();
        }

        class MonoEnumerator(Mono mono) : IEnumerator<string>
        {
            const char PADDING = ' ';
            private string[] Lines { get; } = mono.Text.SplitLines();
            public string Current { get; private set; } = "";

            private int position = Utils.DEFAULT_POSITION;
            object IEnumerator.Current => throw new NotImplementedException();

            public void Dispose() { }

            public bool MoveNext()
            {
                position++;
                if (position >= mono.Height) return false;

                string line = position < Lines.Length ? Lines[position] : "";
                int paddingCount = mono.Width - line.Length;
                
                StringBuilder sb = new();
                sb.Append(line);
                if (paddingCount > 0)
                    sb.Append(PADDING, paddingCount);

                Current = sb.ToString();
                return true;
            }

            public void Reset()
            {
                position = Utils.DEFAULT_POSITION;
                Current = "";
            }
        }
    }
}
