using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBK_Boites
{
    public class Couleureur : IVisiteur<ABCBoite>
    {
        static readonly ConsoleColor[] Colors = [ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Blue, ConsoleColor.Magenta, ConsoleColor.Cyan];
        private int indentDepth = 0;
        const int NUM_INDENTS = 2;
        const char INDENT = ' ';
        // TODO
        public void Entrer()
        {
            
        }

        public void Sortir()
        {
            
        }

        public void Visiter(ABCBoite elem, Action opt)
        {

        }
    }
}
