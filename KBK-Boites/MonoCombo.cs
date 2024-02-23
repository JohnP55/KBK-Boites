using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBK_Boites
{
    public class MonoCombo : ABCBoite
    {
        public ABCBoite Child => Children[0];
        public MonoCombo(ABCBoite boite)
        {
            Adopt(boite.Clone());
            (Width, Height) = MinSize();
            ResizeChildren();
        }

        public override ABCBoite Clone()
        {
            return new MonoCombo(Child);
        }

        public override IEnumerator<string> GetEnumerator()
        {
            return new MonoComboEnumerator(this);
        }

        protected override (int, int) MinSize()
        {
            return (Child.Width + 2, Child.Height + 2); // adding borders
        }

        protected override void ResizeChildren()
        {
            Child.Resize(Width - 2, Height - 2);
        }

        class MonoComboEnumerator(MonoCombo mc) : IEnumerator<string>
        {
            public string Current { get; private set; } = "";
            private int position = Utils.DEFAULT_POSITION;
            private readonly IEnumerator<string> childEnumerator = mc.Child.GetEnumerator();

            object IEnumerator.Current => throw new NotImplementedException();

            public void Dispose() { }

            public bool MoveNext()
            {
                position++;
                if (position == mc.Height) return false;

                StringBuilder sb = new();
                if (position == 0 || position == mc.Height - 1)
                {
                    sb.Append(CORNER);
                    sb.Append(HORIZONTAL_EDGE, mc.Width - 2); // minus 2 edges
                    sb.Append(CORNER);
                }
                else
                {
                    childEnumerator.MoveNext();
                    sb.Append(VERTICAL_EDGE);
                    sb.Append(childEnumerator.Current);
                    sb.Append(VERTICAL_EDGE);
                }
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
