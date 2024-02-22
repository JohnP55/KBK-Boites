namespace KBK_Boites
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Boite b = new();
            Console.WriteLine(b);
            Console.WriteLine(new Boite("yo"));
            string texte = @"Man! Hey!!!
ceci est un test
multiligne";
            string autTexte = "Ceci\nitou, genre";
            Boite b0 = new(texte);
            Boite b1 = new(autTexte);
            Console.WriteLine(b0);
            Console.WriteLine(b1);
            ComboVertical cv = new(b0, b1);
            Console.WriteLine(new Boite(cv));
            return;
            Boite boite = new Boite(
                new ComboVertical(
                    new ComboVertical(
                        new Boite("this is a test string\nballs"),
                        new Boite("this is the second box and it's also longer\nballs t w o\nballs t h r e e")
                    ),
                    new Boite("the two boxes above are in a ComboVertical\nthis is just a mono\nthe cv and this mono are in a ComboVertical together")
                )
            );
            Console.WriteLine(boite);
            Boite b2 = new Boite(
                new Boite(
                    new Boite(
                        new ComboVertical(
                            new Boite("isn't that awesome"),
                            new Boite("\n\n\n\n\nb a l l s")
                        )
                    )
                )
            );
            Console.WriteLine(b2);
        }
    }
}
