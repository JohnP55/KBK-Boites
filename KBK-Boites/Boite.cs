namespace KBK_Boites
{
    public class Boite
    {
        protected ABCBoite? InnerBox { get; set; }
        public Boite()
        {
            InnerBox = new Mono("");
        }
        public Boite(string text)
        {
            InnerBox = new Mono(text);
        }
        public Boite(ABCBoite boite)
        {
            // TODO: faire une copie d'une boite récursivement
            InnerBox = boite;
        }
        public override string ToString()
        {
            // TODO
            return "";
        }
    }
}
