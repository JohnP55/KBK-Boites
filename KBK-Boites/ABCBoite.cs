using System;
using System.Collections;
using System.Text;


namespace KBK_Boites
{
    public class NonOrphanBoxException : Exception
    {
        public NonOrphanBoxException() : base("Tried to adopt a box that already has a parent.") { }
    }
    public class InvalidResizeException : Exception
    {
        public InvalidResizeException() : base("Tried to resize a box below its minimum bounds.") { }
    }
    public abstract class ABCBoite : IEnumerable<string>
    {
        protected const char CORNER = '+';
        protected const char VERTICAL_EDGE = '|';
        protected const char HORIZONTAL_EDGE = '-';

        protected ABCBoite? Parent { get; private set; } = null;
        protected List<ABCBoite> Children { get; private set; } = [];
        public bool IsRoot => Parent == null;
        protected void Adopt(ABCBoite child)
        {
            if (child.Parent is not null)
                throw new NonOrphanBoxException();

            Children.Add(child);
            child.Parent = this;
        }
        protected abstract void ResizeChildren();
        public virtual int Width { get; protected set; }
        public virtual int Height { get; protected set; }
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
        public abstract ABCBoite Clone();
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
    }
}
