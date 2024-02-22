using System.Collections;
using System.Text;

namespace KBK_Boites
{
    /// <summary>
    /// Box that only ever has one child
    /// </summary>
    public class Boite : ABCBoite
    {
        public ABCBoite Child => Children[0];

        // <wall_of_text>
        //     ikik this shit is unreadable, but this optimizes a *lot* by
        //     not having to clone boxes created by the constructor and by
        //     not repeating code in the constructors
        //
        //     basically the ((real)) constructor is the private one that
        //     has a bool to clone or not, the public constructor forces true,
        //     and the constructors that make their own mono don't have to also clone them
        // </wall_of_text>
        public Boite() : this(new Mono(""), false) { }
        public Boite(string text) : this(new Mono(text), false) { }
        public Boite(ABCBoite boite) : this(boite, true) { }
        private Boite(ABCBoite boite, bool clone)
        {
            var child = clone ? boite.Clone() : boite;
            Adopt(child);
            (Width, Height) = MinSize();
            ResizeChildren();
        }
        protected override void ResizeChildren()
        {
            Child.Resize(Width, Height);
        }
        protected override (int, int) MinSize()
        {
            int width = Child.Width;
            int height = Child.Height;
            return (width, height);
        }
        public override Boite Clone()
        {
            return new Boite(Child.Clone());
        }
        public override IEnumerator<string> GetEnumerator()
        {
            return new BoiteEnumerator(this);
        }

        class BoiteEnumerator(Boite boite) : IEnumerator<string>
        {
            public string Current { get; private set; } = "";
            private int position = Utils.DEFAULT_POSITION;
            private IEnumerator<string> childEnumerator = boite.Child.GetEnumerator();
            private int heightPadded = boite.Height + (boite.IsRoot ? 2 : 0);
            object IEnumerator.Current => throw new NotImplementedException();

            public void Dispose()
            {
                
            }

            public bool MoveNext()
            {
                position++;
                if (position == heightPadded) return false;

                StringBuilder sb = new StringBuilder();
                if (boite.IsRoot && (position == 0 || position == heightPadded - 1))
                {
                    sb.Append(CORNER);
                    sb.Append(HORIZONTAL_EDGE, boite.Width);
                    sb.Append(CORNER);
                }
                else
                {
                    childEnumerator.MoveNext();
                    sb.Append(childEnumerator.Current);
                
                    if (boite.IsRoot)
                    {
                        sb.Insert(0, VERTICAL_EDGE);
                        sb.Append(VERTICAL_EDGE);
                    }
                }

                if (boite.IsRoot && position < heightPadded - 1)
                    sb.Append(Environment.NewLine);

                Current = sb.ToString();
                return true;
            }

            public void Reset()
            {
                Current = "";
                position = Utils.DEFAULT_POSITION;
            }
        }
    }
}
