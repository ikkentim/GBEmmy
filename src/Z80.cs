namespace GBEmmy
{
    public class Z80
    {
        private readonly MMU _memory = new MMU();
        private readonly Register _register = new Register();
        private readonly Clock _clock = new Clock();

        public void Reset()
        {
            _register.Reset();
            _clock.Reset();
        }

        public delegate void OpCode(Register r, MMU m, Clock c);

        private OpCode[] _opCodes =
        {
            // 0x0*
            (r, m, c) => { r.M = 1; r.C = 4; }, /* NOP */
            (r,m,c) => { r.C=m.ReadByte(r.PC++); r.B=m.ReadByte(r.PC++); r.M=3; r.T=12; } /* LD BC,d16 */
        };
    }


    // ReSharper disable InconsistentNaming
    public static class OpCodes
    {
        /// <summary>
        /// LD r, r'
        /// r, ← r′
        /// </summary>
        public static void LDrr(ref byte r1, ref byte r2, ref short m, ref short t)
        {
            r1 = r2;
            m = 1;
            t = 4;
        }

        /// <summary>
        /// LD r,n
        /// r ← n
        /// </summary>
        public static void LDrn(ref byte r, byte n, ref short m, ref short t)
        {
            r = n;
            m = 2;
            t = 7;
        }

        /// <summary>
        /// LD r, (HL)
        /// r ← (HL)
        /// </summary>
        public static void LDrHL(ref byte r, MMU mem, ref byte h, ref byte l, ref short m, ref short t)
        {
            r = mem.ReadByte((short)((h << 8) + l));
            m = 2;
            t = 7;
        }

        //skip: LD r, (IX+d)
        //skip: LD r, (IY+d)

        /// <summary>
        /// LD (HL), r
        /// 
        /// (HL) ← r
        /// </summary>
        public static void LDHLr(MMU mem, ref byte h, ref byte l, ref byte r, ref short m, ref short t)
        {
            mem.WriteByte((short)((h << 8) + l), r);
            m = 2;
            t = 7;
        }

        //skip: LD (IX+d), r
        //skip: LD (IY+d), r

        /// <summary>
        /// LD (HL), n
        /// (HL) ← n
        /// </summary>
        public static void LDHLn(MMU mem, ref byte h, ref byte l, byte n, ref short m, ref short t)
        {
            mem.WriteByte((short)((h<<8) + l), n);
            m = 3;
            t = 10;
        }

        //skip: LD (IX+d), n
        //skip: LD (IY+d), n

        /// <summary>
        /// LD A, (BC)
        /// A ← (BC)
        /// </summary>
        public static void LDABC(ref byte a, MMU mem, ref byte b, ref byte c, ref short m, ref short t)
        {
            a = mem.ReadByte((short) ((b << 8) + c));
            m = 2;
            t = 7;
        }

        //TODO: notes

        /*
         * Left off at LD A, (DE) @ page 87 (p. 101 of pdf) of um0080.pdf
         * 
         * Possible improvements:
         * - Switch m,t from ref to out
         * - Pass register values by register class instead of register byte references.
         * 
         * defaulting opcode params to (Register reg, MMU mmu, [ref byte r], [byte n])
         */
    }
    // ReSharper restore InconsistentNaming
}
