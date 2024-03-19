using System.Collections;
using System.Text;

namespace Boites
{
    /// <summary>
    /// Box that only ever has one child
    /// </summary>
    public class Boite : IBoite
    {
        public IBoite Child => Children[0];

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
        public Boite(IBoite boite) : this(boite, true) { }
        private Boite(IBoite boite, bool clone)
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

        class BoiteEnumerator : AbstractBoiteEnumerator
        {
            private IEnumerator<string> ChildEnumerator => ChildEnumerators[0];
            private bool ShouldDisplayBorder => Box.IsRoot && (Position == 0 || Position == ScaledHeight - 1);
            public BoiteEnumerator(Boite boite) : base(boite)
            {
                ScaledHeight = boite.Height + (boite.IsRoot ? 2 : 0); // Add top and bottom borders
            }
            protected override string GetCurrent_Impl()
            {
                StringBuilder sb = new();
                if (ShouldDisplayBorder)
                {
                    sb.Append(CORNER);
                    sb.Append(HORIZONTAL_EDGE, Box.Width);
                    sb.Append(CORNER);
                }
                else
                {
                    sb.Append(ChildEnumerator.Current);

                    // Add walls if necessary
                    if (Box.IsRoot)
                    {
                        sb.Insert(0, VERTICAL_EDGE);
                        sb.Append(VERTICAL_EDGE);
                    }
                }

                if (Box.IsRoot && Position < ScaledHeight - 1)
                    sb.Append(Environment.NewLine);

                return sb.ToString();
            }
            protected override bool MoveChildren_Impl()
            {
                if (ShouldDisplayBorder)
                    return true;
                return ChildEnumerator.MoveNext();
            }
        }
    }
}
