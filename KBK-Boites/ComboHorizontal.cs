using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBK_Boites
{
    internal class ComboHorizontal : ABCBoite
    {
        Boite LeftBox { get; init; }
        Boite RightBox { get; init; }
        public ComboHorizontal(Boite leftBox, Boite rightBox)
        {
            LeftBox = leftBox;
            RightBox = rightBox;
        }
        public override IEnumerator<Boite> GetEnumerator() => new ComboHorizontalEnumerator(this);

        // I'm high lol, didnt realize at first we had to iterate over strings
        class ComboHorizontalEnumerator(ComboHorizontal comboHorizontal) : IEnumerator<string>
        {
            enum State { Uninitialized, First, Second }
            private State CurrentState { get; set; } = State.Uninitialized;
            public ComboHorizontal ComboHorizontal { get; init; } = comboHorizontal;
            public Boite Current => CurrentState switch
            {
                State.First => ComboHorizontal.LeftBox,
                State.Second => ComboHorizontal.RightBox,
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
}
