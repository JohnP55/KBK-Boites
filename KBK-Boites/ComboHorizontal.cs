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
        public override ABCBoite Clone()
        {
            throw new NotImplementedException();
        }

        public override IEnumerator<string> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        protected override (int, int) MinSize()
        {
            throw new NotImplementedException();
        }

        protected override void ResizeChildren()
        {
            throw new NotImplementedException();
        }
    }
}
