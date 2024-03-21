using System.ComponentModel;
using System.Diagnostics;

namespace Boites;

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
    static Couleureur couleureur = new();
    static Mesureur mesureur = new();
    static void PrintBoitesFromNotation(string notation)
    {
        var boites = new FabriqueBoites().Créer(notation);
        Tester(new Boite(boites), couleureur, mesureur);
    }
    static void Part3Test()
    {

        PrintBoitesFromNotation(@"cv
mono We're no strangers to love
cv
ch
mono You know the rules and so do I
mono A full commitment's what I'm thinking of
mono You wouldn't get this from any other guy");
        PrintBoitesFromNotation(@"cv
mono I just wanna tell you how I'm feeling
cv
mono 
mono Gotta make you understand
");
        PrintBoitesFromNotation(@"ch
ch
mono 
cv
mono Never gonna give you up
ch
mono Never gonna let you down
cv
mono Never gonna run around and desert you
ch
mono Never gonna make you cry
ch
mono 
ch
mono Never gonna say goodbye
mono Never gonna tell a lie and hurt you
mono 
mono ");

        PrintBoitesFromNotation(@"ch
mono We've known each other for so long
cv
cv
cv
mono Your heart's been aching, but you're too shy to say it
mono Inside, we both know what's been going on
mono We know the game and we're gonna play it.
mono ");
        PrintBoitesFromNotation(@"ch
ch
mono And if you ask me how I'm feeling
ch
mono 
mono 
mono Don't tell me you're too blind to see");

        PrintBoitesFromNotation(@"cv
cv
mono Never gonna give you up
ch
mono 
ch
mono Never gonna let you down
ch
mono Never gonna run around and desert you
ch
mono 
ch
mono Never gonna make you cry
cv
mono Never gonna say goodbye
mono 
mono Never gonna tell a lie and hurt you
mono ");

        PrintBoitesFromNotation(@"mc
mc
mc
mc
mc
ch
mc
mc
mono Never gonna give you up
ch
ch
mono Never gonna let you down
mc
mc
mono Never gonna run around and desert you
ch
ch
mono 
mono Never gonna make you cry
cv
mono Never gonna say goodbye
mc
mono Never gonna tell a lie and hurt you");
    }
    static void Main(string[] args)
    {
        // Stopwatch sw = Stopwatch.StartNew();
        // TestFabriques();
        // TesterVisiteurs();
        // sw.Stop();
        // Console.WriteLine($"Executed in {sw.ElapsedMilliseconds}ms");
        Part3Test();
        
    }
    static void Tester(Boite b, params IVisiteur<IBoite>[] viz)
    {
        Console.WriteLine(b);
        foreach (var v in viz)
            b.Accepter(v);
    }
    static void TesterVisiteurs()
    {
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