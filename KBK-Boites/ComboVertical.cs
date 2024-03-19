using System.Collections;
using System.Text;

namespace Boites
{
    public class ComboVertical : IBoite
    {
        public IBoite TopBox => Children[0];
        public IBoite BottomBox => Children[1];

        public ComboVertical(IBoite topBox, IBoite bottomBox)
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
        public override IBoite Clone()
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

        class ComboVerticalEnumerator(ComboVertical cv) : AbstractBoiteEnumerator(cv)
        {
            private IEnumerator<string> TopEnumerator => ChildEnumerators[0];
            private IEnumerator<string> BottomEnumerator => ChildEnumerators[1];
            private IEnumerator<string>? CurrentChildEnumerator
            {
                get
                {
                    if (Position < cv.TopBox.Height)
                        return TopEnumerator;
                    else if (Position > cv.TopBox.Height)
                        return BottomEnumerator;
                    else // wall
                        return null;
                }
            }
            private bool ShouldDisplayWall => Position == cv.TopBox.Height;
            protected override string GetCurrent_Impl()
            {
                StringBuilder sb = new();
                if (ShouldDisplayWall)
                {
                    sb.Append(HORIZONTAL_EDGE, cv.Width);
                }
                else
                {
                    sb.Append(CurrentChildEnumerator!.Current);
                }
                return sb.ToString();
            }
            protected override bool MoveChildren_Impl()
            {
                if (!ShouldDisplayWall)
                    return CurrentChildEnumerator!.MoveNext();
                return true;
            }
        }
    }
}
