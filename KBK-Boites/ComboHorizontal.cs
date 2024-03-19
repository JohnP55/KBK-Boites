using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boites
{
    public class ComboHorizontal : IBoite
    {
        public IBoite LeftBox => Children[0];
        public IBoite RightBox => Children[1];

        public ComboHorizontal(IBoite leftBox, IBoite rightBox)
        {
            Adopt(leftBox.Clone());
            Adopt(rightBox.Clone());
            (Width, Height) = MinSize();
            ResizeChildren();
        }
        protected override void ResizeChildren()
        {
            foreach (var child in Children)
            {
                child.Resize(child.Width, Height);
            }
        }
        protected override (int, int) MinSize()
        {
            int width = LeftBox.Width + RightBox.Width + 1; // plus separator
            int height = Math.Max(LeftBox.Height, RightBox.Height);
            height = Math.Max(height, 1);
            return (width, height);
        }
        public override IBoite Clone()
        {
            return new ComboHorizontal(LeftBox, RightBox);
        }
        public override IEnumerator<string> GetEnumerator() => new ComboHorizontalEnumerator(this);

        class ComboHorizontalEnumerator(ComboHorizontal ch) : AbstractBoiteEnumerator(ch)
        {
            private IEnumerator<string> LeftEnumerator => ChildEnumerators[0];
            private IEnumerator<string> RightEnumerator => ChildEnumerators[1];
            protected override string GetCurrent_Impl()
            {
                StringBuilder sb = new();
                sb.Append(LeftEnumerator.Current);
                sb.Append(VERTICAL_EDGE);
                sb.Append(RightEnumerator.Current);
                return sb.ToString();
            }
            protected override bool MoveChildren_Impl()
            {
                LeftEnumerator.MoveNext();
                RightEnumerator.MoveNext();
                return true;
            }
        }
    }
}
