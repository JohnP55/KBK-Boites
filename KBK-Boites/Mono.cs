namespace KBK_Boites
{
    public class Mono : ABCBoite
    {
        public string Text { get; set; }
        public Mono(string text)
        {
            Text = text;
            (Width, Height) = MinSize;
        }
        private (int, int) MinSize
        {
            get
            {
                string[] lines = Text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                int width = lines.Max(x => x.Length);
                int height = lines.Length;
                return (width, height);
            }
        }

        public override IEnumerator<Boite> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        //Not sure how to enumerate through this
        class MonoEnumerator(Mono mono) : IEnumerator<string>
        {
            enum State { Uninitialized, First, Second }
            private State CurrentState { get; set; } = State.Uninitialized;
            public Mono mono { get; init; } = mono;
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
