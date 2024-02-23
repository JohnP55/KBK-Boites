using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBK_Boites
{
    public class ComboHorizontal : ABCBoite
    {
        public ABCBoite LeftBox => Children[0];
        public ABCBoite RightBox => Children[1];

        public ComboHorizontal(ABCBoite leftBox, ABCBoite rightBox)
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
        public override ABCBoite Clone()
        {
            return new ComboHorizontal(LeftBox, RightBox);
        }

        public override IEnumerator<string> GetEnumerator() => new ComboHorizontalEnumerator(this);
        class ComboHorizontalEnumerator(ComboHorizontal ch) : IEnumerator<string>
        {
            public string Current { get; private set; } = "";
            private int position = Utils.DEFAULT_POSITION;
            private readonly IEnumerator<string> leftEnumerator = ch.LeftBox.GetEnumerator();
            private readonly IEnumerator<string> rightEnumerator = ch.RightBox.GetEnumerator();
            object IEnumerator.Current => throw new NotImplementedException();

            public void Dispose() { }

            public bool MoveNext()
            {
                position++;
                if (position == ch.Height) return false;

                StringBuilder sb = new();
                leftEnumerator.MoveNext();
                sb.Append(leftEnumerator.Current);
                sb.Append(VERTICAL_EDGE);

                rightEnumerator.MoveNext();
                sb.Append(rightEnumerator.Current);

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
