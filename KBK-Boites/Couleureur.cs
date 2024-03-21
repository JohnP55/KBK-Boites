using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boites
{
    public class Couleureur : IVisiteur<IBoite>
    {
        private int indentDepth = -2;
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

        public void Visiter(IBoite elem, Action? opt)
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
