using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBK_Boites
{
    internal class ComboVertical : ABCBoite
    {
        Boite TopBox {  get; set; }
        Boite BottomBox {  get; set; }

        public ComboVertical(Boite topBox, Boite bottomBox)
        {
            TopBox = topBox;
            BottomBox = bottomBox;
        }

        public override IEnumerator<string> GetEnumerator() => new ComboVerticalEnumerator(this);

        class ComboVerticalEnumerator(ComboVertical comboVertical) : IEnumerator<string>
        {
            enum State { Uninitialized, First, Second }
            private State CurrentState { get; set; } = State.Uninitialized;
            public ComboVertical ComboVertical { get; init; } = comboVertical;
            public Boite Current => CurrentState switch
            {
                State.First => ComboVertical.TopBox,
                State.Second => ComboVertical.BottomBox,
                _ => throw new UninitializedEnumeratorException()
            };

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (CurrentState == State.Second)
                    return false;

                // Wtf thats legal lol
                CurrentState++;
                return true;
            }
            public void Dispose() { }

            public void Reset() { }
        }
    }

    class UninitializedEnumeratorException : Exception { }
}
