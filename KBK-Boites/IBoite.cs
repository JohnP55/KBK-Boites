using System;
using System.Collections;
using System.Text;


namespace Boites
{
    public class NonOrphanBoxException : Exception
    {
        public NonOrphanBoxException() : base("Tried to adopt a box that already has a parent.") { }
    }
    public class InvalidResizeException : Exception
    {
        public InvalidResizeException() : base("Tried to resize a box below its minimum bounds.") { }
    }
    public abstract class IBoite : IEnumerable<string>, IVisitable<IBoite>
    {
        protected const char CORNER = '+';
        protected const char VERTICAL_EDGE = '|';
        protected const char HORIZONTAL_EDGE = '-';

        protected IBoite? Parent { get; private set; } = null;
        public List<IBoite> Children { get; private set; } = [];
        public bool IsRoot => Parent == null;
        protected void Adopt(IBoite child)
        {
            if (child.Parent is not null)
                throw new NonOrphanBoxException();

            Children.Add(child);
            child.Parent = this;
        }
        protected abstract void ResizeChildren();
        public virtual int Width { get; protected set; }
        public virtual int Height { get; protected set; }
        public int Area => Width * Height;
        protected abstract (int, int) MinSize();
        public virtual void Resize(int width, int height)
        {
            (int minWidth, int minHeight) = MinSize();
            if (width < minWidth || height < minHeight)
                throw new InvalidResizeException();

            Width = width;
            Height = height;
            ResizeChildren();
        }
        public abstract IBoite Clone();
        public override string ToString()
        {
            StringBuilder sb = new();
            foreach (string line in this)
            {
                sb.Append(line);
            }
            return sb.ToString();
        }
        public abstract IEnumerator<string> GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public virtual void Accepter(IVisiteur<IBoite> viz)
        {
            viz.Entrer();
            viz.Visiter(this, null);
            viz.Sortir();
        }
    }
    public abstract class BoxEnumerator : IEnumerator<string>
    {
        object IEnumerator.Current => throw new NotImplementedException();
        
        protected const int DEFAULT_POSITION = -1;
        protected BoxEnumerator(IBoite box)
        {
            Box = box;
            InitChildEnumerators();
            ScaledHeight = box.Height;
        }

        public string Current { get; protected set; } = "";
        protected int Position { get; set; } = DEFAULT_POSITION;

        protected IBoite Box { get; init; }
        protected virtual int ScaledHeight { get; set; }
        List<IEnumerator<string>> ChildEnumerators { get; } = new List<IEnumerator<string>>();
        protected void InitChildEnumerators()
        {
            foreach (var child in Box.Children)
                ChildEnumerators.Add(child.GetEnumerator());
        }
        protected abstract bool MoveChildren_Impl();
        protected abstract string GetCurrent_Impl();
        public virtual bool MoveNext()
        {
            Position++;
            if (Position == ScaledHeight) return false;
            
            MoveChildren_Impl();
            Current = GetCurrent_Impl();
            return true;
        }
        public void Reset()
        {
            Current = "";
            Position = DEFAULT_POSITION;
        }
        public void Dispose() { }
    }
}
