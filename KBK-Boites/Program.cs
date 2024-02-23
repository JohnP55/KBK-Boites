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
        var p = new Boite(new FabriqueBoites().Créer(
           "ch\ncv\nmc\nmono J'aime mon \"prof\"\nmono moi itou\nmono eh ben"));
        p.Accepter(new Couleureur());
        sw.Stop();
        Console.WriteLine($"Executed in {sw.ElapsedMilliseconds}ms");
    }
}