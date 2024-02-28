using System.ComponentModel;
using System.Diagnostics;

namespace KBK_Boites;

class Program
{
    static void TestFabriques()
    {
        var p = new FabriqueBoites().Créer("mono J'aime mon \"prof\"");
        Console.WriteLine(new Boite(p));
        p = new FabriqueBoites().Créer("cv\nmono J'aime mon \"prof\"\nmono moi itou");
        Console.WriteLine(new Boite(p));
        p = new FabriqueBoites().Créer("ch\nmono J'aime mon \"prof\"\nmono moi itou");
        Console.WriteLine(new Boite(p));
        p = new FabriqueBoites().Créer(
           "ch\ncv\nmono J'aime mon \"prof\"\nmono moi itou\nmono eh ben");
        Console.WriteLine(new Boite(p));
        p = new FabriqueBoites().Créer(
           "ch\ncv\nmc\nmono J'aime mon \"prof\"\nmono moi itou\nmono eh ben");
        Console.WriteLine(new Boite(p));
    }
    static void Main(string[] args)
    {
        Stopwatch sw = Stopwatch.StartNew();
        TestFabriques();
        TesterVisiteurs();
        sw.Stop();
        Console.WriteLine($"Executed in {sw.ElapsedMilliseconds}ms");
    }
    static void TesterVisiteurs()
    {
        static void Tester(Boite b, params IVisiteur<ABCBoite>[] viz)
        {
            Console.WriteLine(b);
            foreach (var v in viz)
                b.Accepter(v);
        }
        var coul = new Couleureur();
        var mes = new Mesureur();
        Tester(new Boite(), coul, mes);
        Tester(new Boite("yo"), coul, mes);
        string texte = @"Man! Hey!!!
ceci est un test
multiligne";
        string autTexte = "Ceci\nitou, genre";
        Boite b0 = new Boite(texte);
        Boite b1 = new Boite(autTexte);
        Tester(b0, coul, mes);
        Tester(b1, coul, mes);
        ComboVertical cv = new ComboVertical(b0, b1);
        Tester(new Boite(cv), coul, mes);
        ComboHorizontal ch = new ComboHorizontal(b0, b1);
        Tester(new Boite(ch), coul, mes);
        ComboVertical cvplus = new ComboVertical(new Boite(cv), new Boite(ch));
        Tester(new Boite(cvplus), coul, mes);
        ComboHorizontal chplus = new ComboHorizontal(new Boite(cv), new Boite(ch));
        Tester(new Boite(chplus), coul, mes);
        ComboVertical cvv = new ComboVertical(new Boite(chplus), new Boite("coucou"));
        Tester(new Boite(cvv), coul, mes);
        Tester(new Boite(
           new ComboHorizontal(
              new Boite("a\nb\nc\nd\ne"),
                 new Boite(
                    new ComboVertical(
                       new Boite("allo"), new Boite("yo")
                    )
                 )
              )
           ), coul, mes
        );
        Tester(
           new Boite(new ComboHorizontal(new Boite("Yo"), new Boite())),
           coul, mes
        );
        Tester(
           new Boite(new ComboHorizontal(new Boite(), new Boite("Ya"))),
           coul, mes
        );
        Tester(
           new Boite(new ComboHorizontal(new Boite(), new Boite())),
           coul, mes
        );
        Tester(
           new Boite(new ComboVertical(new Boite("Yip"), new Boite())),
           coul, mes
        );
        Tester(
           new Boite(new ComboVertical(new Boite(), new Boite("Yap"))),
           coul, mes
        );
        Tester(
           new Boite(new ComboVertical(new Boite(), new Boite())),
           coul, mes
        );
        Tester(new Boite(new MonoCombo(new Boite("allo"))), coul, mes);
        Tester(new Boite(
           new MonoCombo(new Boite(new MonoCombo(new Boite("allo"))))
        ), coul, mes);
        Tester(new Boite(
           new ComboVertical(
              new Boite(new MonoCombo(new Boite(new MonoCombo(new Boite("allo"))))),
              new Boite("Eh ben")
           )
        ), coul, mes);
        Tester(new Boite(
           new ComboHorizontal(new Boite("a\nb\nc\nd"),
                               new Boite(new MonoCombo(new Boite())))
        ), coul, mes);
        Tester(new Boite(
           new ComboHorizontal(new Boite(),
                               new Boite(new MonoCombo(new Boite())))
        ), coul, mes);
        Tester(new Boite(
           new ComboHorizontal(
              new Boite(new MonoCombo(new Boite(new MonoCombo(new Boite("allo"))))),
              new Boite(new ComboVertical(
                 new Boite("Eh ben"),
                 new Boite(new MonoCombo(new Boite(
                    new ComboHorizontal(new Boite("yo"), new Boite("hey"))
                 )))
              ))
           )
        ), coul, mes);
        Console.WriteLine($"\n\nLa plus petite boite est :\n{mes.PlusPetite}");
        Console.WriteLine($"\n\nLa plus grande boite est :\n{mes.PlusGrande}");
    }
}