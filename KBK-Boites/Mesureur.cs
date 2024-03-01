using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boites
{
    public class Mesureur : IVisiteur<IBoite>
    {
        public IBoite? PlusPetite { get; private set; }
        public IBoite? PlusGrande {  get; private set; }
        public void Entrer()
        {
        }

        public void Sortir()
        {
        }

        public void Visiter(IBoite elem, Action? opt)
        {
            PlusPetite ??= elem;
            PlusGrande ??= elem;

            PlusPetite = elem.Area < PlusPetite.Area ? elem : PlusPetite;
            PlusGrande = elem.Area > PlusGrande.Area ? elem : PlusGrande;

            foreach (var child in elem.Children)
                child.Accepter(this);
        }

    }
}
