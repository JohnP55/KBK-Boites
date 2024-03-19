using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Boites
{
    public class MonoCombo : IBoite
    {
        public IBoite Child => Children[0];
        public MonoCombo(IBoite boite)
        {
            Adopt(boite.Clone());
            (Width, Height) = MinSize();
            ResizeChildren();
        }
        public override IBoite Clone()
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

        class MonoComboEnumerator(MonoCombo mc) : AbstractBoiteEnumerator(mc)
        {
            private IEnumerator<string> ChildEnumerator => ChildEnumerators[0];
            private bool ShouldDisplayBorder => Position == 0 || Position == mc.Height - 1;
            protected override string GetCurrent_Impl()
            {
                StringBuilder sb = new();
                if (ShouldDisplayBorder)
                {
                    sb.Append(CORNER);
                    sb.Append(HORIZONTAL_EDGE, mc.Width - 2); // minus 2 edges
                    sb.Append(CORNER);
                }
                else
                {
                    sb.Append(VERTICAL_EDGE);
                    sb.Append(ChildEnumerator.Current);
                    sb.Append(VERTICAL_EDGE);
                }
                return sb.ToString();
            }
            protected override bool MoveChildren_Impl()
            {
                if (!ShouldDisplayBorder)
                    return ChildEnumerator.MoveNext();
                return true;
            }
        }
    }
}
