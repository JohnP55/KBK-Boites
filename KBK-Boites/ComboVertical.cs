using System.Collections;
using System.Text;

namespace KBK_Boites
{
    public class ComboVertical : ABCBoite
    {
        public ABCBoite TopBox => Children[0];
        public ABCBoite BottomBox => Children[1];

        public ComboVertical(ABCBoite topBox, ABCBoite bottomBox)
        {
            Adopt(topBox.Clone());
            Adopt(bottomBox.Clone());
            (Width, Height) = MinSize();
            ResizeChildren();
        }
        protected override void ResizeChildren()
        {
            TopBox.Resize(Width, TopBox.Height);
            int bottomHeight = Height - (TopBox.Height + 1);
            BottomBox.Resize(Width, bottomHeight);
        }
        public override ABCBoite Clone()
        {
            return new ComboVertical(TopBox, BottomBox);
        }

        protected override (int, int) MinSize()
        {
            int width = Math.Max(TopBox.Width, BottomBox.Width);
            int height = TopBox.Height + BottomBox.Height + 1; // plus separator
            width = Math.Max(width, 1);
            return (width, height);
        }
        public override IEnumerator<string> GetEnumerator()
        {
            return new ComboVerticalEnumerator(this);
        }
        class ComboVerticalEnumerator(ComboVertical cv) : IEnumerator<string>
        {
            public string Current { get; private set; } = "";
            private int position = Utils.DEFAULT_POSITION;
            private readonly IEnumerator<string> topEnumerator = cv.TopBox.GetEnumerator();
            private readonly IEnumerator<string> bottomEnumerator = cv.BottomBox.GetEnumerator();
            object IEnumerator.Current => throw new NotImplementedException();

            public void Dispose() { }

            public bool MoveNext()
            {
                position++;
                if (position >= cv.Height) return false;

                StringBuilder sb = new();
                if (position == cv.TopBox.Height)
                {
                    sb.Append(HORIZONTAL_EDGE, cv.Width);
                }
                else
                {
                    var enumerator = position < cv.TopBox.Height ? topEnumerator : bottomEnumerator;
                    enumerator.MoveNext();
                    sb.Append(enumerator.Current);
                }
                Current = sb.ToString();
                return true;
            }

            public void Reset()
            {
                Current = "";
                position = -1;
            }
        }
    }
}
