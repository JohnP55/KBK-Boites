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

            Console.Write(new string(INDENT, indentDepth));
            Console.ForegroundColor = elem.Name switch
            {
                ComboHorizontal.NAME => ConsoleColor.Red,
                ComboVertical.NAME => ConsoleColor.Green,
                Mono.NAME => ConsoleColor.Magenta,
                MonoCombo.NAME => ConsoleColor.Yellow,
                Boite.NAME => ConsoleColor.Blue,
                _ => ConsoleColor.Cyan,
            };
            Console.Write(elem.Name);
            opt?.Invoke();

            Console.WriteLine();
            Console.ForegroundColor = prevColor;

            foreach (var child in elem.Children)
                child.Accepter(this);
        }
    }
}
