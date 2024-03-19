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

        class MonoEnumerator : AbstractBoiteEnumerator
        {
            const char PADDING = ' ';
            private string[] Lines { get; }
            public MonoEnumerator(Mono mono) : base(mono)
            {
                Lines = Utils.SplitLines(mono.Text);
            }
            protected override string GetCurrent_Impl()
            {
                string line = Position < Lines.Length ? Lines[Position] : "";

                StringBuilder sb = new();
                sb.Append(line);

                int paddingCount = Box.Width - line.Length;
                if (paddingCount > 0)
                    sb.Append(PADDING, paddingCount);
                
                return sb.ToString();
            }
            protected override bool MoveChildren_Impl()
            {
                return true;
            }
        }
    }
}
