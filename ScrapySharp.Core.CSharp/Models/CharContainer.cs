namespace ScrapySharp.Core.CSharp
{
    public class CharContainer
    {
        internal int offset;
        internal char c;
        public CharContainer(char c, int offset)
        {
            this.c = c;
            this.offset = offset;
        }
        public int Offset
        {
            get
            {
                return this.offset;
            }
        }
        public char Char
        {
            get
            {
                return this.c;
            }
        }
    }
}
