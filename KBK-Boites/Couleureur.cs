using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBK_Boites
{
    public class Couleureur : IVisiteur<ABCBoite>
    {
        private int indentDepth = 0;
        const int NUM_INDENTS = 2;
        const char INDENT = ' ';
        public void Entrer()
        {
            indentDepth += NUM_INDENTS;
        }

        public void Sortir()
        {
            indentDepth -= NUM_INDENTS;
        }

        public void Visiter(ABCBoite elem, Action? opt)
        {
            ConsoleColor prevColor = Console.ForegroundColor;
            // Man I love pattern matching fuck classic switch case
            Console.ForegroundColor = elem switch
            {
                ComboHorizontal => ConsoleColor.Red,
                ComboVertical => ConsoleColor.Green,
                Mono => ConsoleColor.Magenta,
                MonoCombo => ConsoleColor.Yellow,
                Boite => ConsoleColor.Blue,
                _ => ConsoleColor.Cyan,
            };

            Console.Write(new string(INDENT, indentDepth));
            Console.Write(elem.GetType().Name);
            opt?.Invoke();
            Console.WriteLine();
            Console.ForegroundColor = prevColor;

            foreach (var child in elem.Children)
                child.Accepter(this);
        }
    }
}
