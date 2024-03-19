using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boites
{
    public class InvalidBoxException : Exception
    {
        public InvalidBoxException(string prefix) : base($"Tried to instantiate a box type that does not exist ({prefix}).") { }
    }
    public class EmptyConfigException : Exception
    {
        public EmptyConfigException() : base("Tried to parse an empty string as data.") { }
    }
    public class FabriqueBoites
    {
        int Position { get; set; } = 0;
        List<IBoite> ParseChildren(string[] boxes, int boxCount)
        {
            List<IBoite> output = [];
            for (int i = 0; i < boxCount; i++)
            {
                IBoite box = ParseNext(boxes);
                output.Add(box);
            }
            return output;
        }
        IBoite ParseNext(string[] boxes)
        {
            while (Position < boxes.Length)
            {
                string box = boxes[Position];
                int postPrefix = Utils.FindIf(box, c => c == ' ');
                string prefix = box.Substring(0, postPrefix);
                switch (prefix.ToLower())
                {
                    case "mono":
                        {
                            string text = box.Substring(postPrefix + 1, box.Length - postPrefix - 1);
                            Position++;
                            return new Mono(text);
                        }
                    case "ch":
                        {
                            Position++;
                            var parsedBoxes = ParseChildren(boxes, 2);
                            return new ComboHorizontal(parsedBoxes[0], parsedBoxes[1]);
                        }
                    case "cv":
                        {
                            Position++;
                            var parsedBoxes = ParseChildren(boxes, 2);
                            return new ComboVertical(parsedBoxes[0], parsedBoxes[1]);
                        }
                    case "mc":
                        {
                            Position++;
                            var parsedBoxes = ParseChildren(boxes, 1);
                            return new MonoCombo(parsedBoxes[0]);
                        }
                    default:
                        throw new InvalidBoxException(prefix);
                }
            }
            throw new EmptyConfigException();
        }
        public IBoite Créer(string s)
        {
            string[] boxes = s.SplitLines();
            return ParseNext(boxes);
        }
    }
}
