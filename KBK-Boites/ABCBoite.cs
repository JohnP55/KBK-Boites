using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBK_Boites
{
    public abstract class ABCBoite : IEnumerable<string>
    {
        public virtual int Width { get; protected set; }
        public virtual int Height { get; protected set; }

        public abstract IEnumerator<string> GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
