namespace GBEmmy
{
    public class Register
    {
        public byte A { get; set; }
        public byte B { get; set; }
        public byte C { get; set; }
        public byte D { get; set; }
        public byte E { get; set; }
        public byte H { get; set; }
        public byte L { get; set; }
        public Flags Flags { get; set; }

        public short PC { get; set; }
        public short SP { get; set; }

        //instruction time
        public short M { get; set; }
        public short T { get; set; }

        public void Reset()
        {
            A = 0;
            B = 0;
            C = 0;
            D = 0;
            E = 0;
            H = 0;
            L = 0;
            Flags = 0;
            PC = 0;
            SP = 0;
        }
    }
}
