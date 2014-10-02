// GBEmmy
// Copyright (C) 2014 Tim Potze
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <http://unlicense.org>

using System;

namespace GBEmmy
{
    public class Z80
    {
        public delegate void OpCode(Register r, MMU m);

        private readonly Clock _clock = new Clock();

        private readonly MMU _memory = new MMU();
        private readonly Register _register = new Register();

        private OpCode[] _opCodes =
        {
            // 0x0*
            (r, m) =>
            {
                r.M = 1;
                r.T = 4;
            }, /* NOP */
            (r, m) => OpCodes.LDddnn2(r, m, ref r.B, ref r.C, m[r.PC++], m[r.PC++]), /* LD BC,d16 */
            (r, m) => OpCodes.LDBCA(r, m), /* LD (BC),A */
            (r, m) => OpCodes.NOIMP(), /* INC BC */
            (r, m) => OpCodes.NOIMP(), /* INC B */
            (r, m) => OpCodes.NOIMP(), /* DEC B */
            (r, m) => OpCodes.LDrn(r, ref r.B, m[r.PC++]), /* LD B,d8 */
            (r, m) => OpCodes.NOIMP(), /* RLCA */
            (r, m) => OpCodes.NOIMP(), /* LD (a16),SP */
            (r, m) => OpCodes.NOIMP(), /* ADD HL,BC */
            (r, m) => OpCodes.LDABC(r, m), /* LD A,(BC) */
            (r, m) => OpCodes.NOIMP(), /* DEC BC */
            (r, m) => OpCodes.NOIMP(), /* INC C */
            (r, m) => OpCodes.NOIMP(), /* DEC C */
            (r, m) => OpCodes.LDrn(r, ref r.C, m[r.PC++]), /* LD C,d8 */
            (r, m) => OpCodes.NOIMP(), /* RRCA */
            // 0x1*
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            //0x2*
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            //0x3*
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            //0x4*
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            //0x5*
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            //0x6*
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            //0x7*
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            //0x8*
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            //0x9*
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            //0xA*
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            //0xB*
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            //0xC*
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            //0xD*
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            //0xE*
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            //0xF*
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP(),
            (r, m) => OpCodes.NOIMP()
        };

        public void Reset()
        {
            _register.Reset();
            _clock.Reset();
        }
    }


    // ReSharper disable InconsistentNaming
    public static class OpCodes
    {
        public static void NOIMP()
        {
            throw new NotImplementedException();
        }

        #region 8-Bit Load Group

        /// <summary>
        ///     LD r, r'
        ///     r, ← r′
        /// </summary>
        public static void LDrr(Register reg, ref byte r1, ref byte r2)
        {
            r1 = r2;
            reg.M = 1;
            reg.T = 4;
        }

        /// <summary>
        ///     LD r,n
        ///     r ← n
        /// </summary>
        public static void LDrn(Register reg, ref byte r, byte n)
        {
            r = n;
            reg.M = 2;
            reg.T = 7;
        }

        /// <summary>
        ///     LD r, (HL)
        ///     r ← (HL)
        /// </summary>
        public static void LDrHL(Register reg, MMU mem, ref byte r)
        {
            r = mem[(reg.H << 8) + reg.L];
            reg.M = 2;
            reg.T = 7;
        }

        //skip: LD r, (IX+d)
        //skip: LD r, (IY+d)

        /// <summary>
        ///     LD (HL), r
        ///     (HL) ← r
        /// </summary>
        public static void LDHLr(Register reg, MMU mem, ref byte r)
        {
            mem[(reg.H << 8) + reg.L] = r;
            reg.M = 2;
            reg.T = 7;
        }

        //skip: LD (IX+d), r
        //skip: LD (IY+d), r

        /// <summary>
        ///     LD (HL), n
        ///     (HL) ← n
        /// </summary>
        public static void LDHLn(Register reg, MMU mem, byte n)
        {
            mem[(reg.H << 8) + reg.L] = n;
            reg.M = 3;
            reg.T = 10;
        }

        //skip: LD (IX+d), n
        //skip: LD (IY+d), n

        /// <summary>
        ///     LD A, (BC)
        ///     A ← (BC)
        /// </summary>
        public static void LDABC(Register reg, MMU mem)
        {
            reg.A = mem[(reg.B << 8) + reg.C];
            reg.M = 2;
            reg.T = 7;
        }

        /// <summary>
        ///     LD A, (DE)
        ///     A ← (DE)
        /// </summary>
        public static void LDADE(Register reg, MMU mem)
        {
            reg.A = mem[(reg.D << 8) + reg.E];
            reg.M = 2;
            reg.T = 7;
        }

        /// <summary>
        ///     LD A, (nn)
        ///     A ← (nn)
        /// </summary>
        public static void LDAnn(Register reg, MMU mem, byte n1, byte n2)
        {
            reg.A = mem[(n2 << 8) + n1];
            reg.M = 4;
            reg.T = 13;
        }

        /// <summary>
        ///     LD (BC), A
        ///     (BC) ← A
        /// </summary>
        public static void LDBCA(Register reg, MMU mem)
        {
            mem[(reg.B << 8) + reg.C] = reg.A;
            reg.M = 2;
            reg.T = 7;
        }

        /// <summary>
        ///     LD (DE), A
        ///     (DE) ← A
        /// </summary>
        public static void LDDEA(Register reg, MMU mem)
        {
            mem[(reg.D << 8) + reg.E] = reg.A;
            reg.M = 2;
            reg.T = 7;
        }

        /// <summary>
        ///     LD (nn), A
        ///     (nn) ← A
        /// </summary>
        public static void LDnnA(Register reg, MMU mem, byte n1, byte n2)
        {
            mem[(n2 << 8) + n1] = reg.A;
            reg.M = 4;
            reg.T = 13;
        }

        //skipped: LD A, I
        //skipped: LD A, R
        //skipped: LD I, A
        //skipped: LD R, A

        #endregion

        #region 16-Bit Load Group

        /// <summary>
        ///     LD dd, nn
        ///     dd ← nn
        /// </summary>
        public static void LDddnn(Register reg, MMU mem, ref byte d1, ref byte d2, byte n1, byte n2)
        {
            mem[(d1 << 8) + d2] = mem[(n2 << 8) + n1];
            reg.M = 2;
            reg.T = 10;
        }

        //skipped: LD IX, nn
        //skipped: LD IY, nn

        /// <summary>
        ///     LD HL, (nn)
        ///     H ← (nn + 1), L ← (nn)
        /// </summary>
        public static void LDHLnn(Register reg, MMU mem, byte n1, byte n2)
        {
            reg.H = mem[(n2 << 8) + n1 + 1];
            reg.L = mem[(n2 << 8) + n1];
            reg.M = 5;
            reg.T = 16;
        }

        /// <summary>
        ///     LD dd, (nn)
        ///     ddh ← (nn + 1) ddl ← (nn)
        /// </summary>
        public static void LDddnn2(Register reg, MMU mem, ref byte d1, ref byte d2, byte n1, byte n2)
        {
            d2 = mem[(n2 << 8) + n1 + 1];
            d1 = mem[(n2 << 8) + n1];
            reg.M = 6;
            reg.T = 206;
        }

        //skipped: LD IX, (nn)
        //skipped: LD IY, (nn)

        /// <summary>
        ///     LD (nn), HL
        ///     (nn + 1) ← H, (nn) ← L
        /// </summary>
        public static void LDnnHL(Register reg, MMU mem, byte n1, byte n2)
        {
            mem[(n2 << 8) + n1 + 1] = reg.H;
            mem[(n2 << 8) + n1] = reg.L;

            reg.M = 5;
            reg.T = 16;
        }

        /// <summary>
        ///     LD (nn), dd
        ///     (nn + 1) ← ddh, (nn) ← ddl
        /// </summary>
        public static void LDnndd(Register reg, MMU mem, byte n1, byte n2, ref byte d1, ref byte d2)
        {
            mem[(n2 << 8) + n1 + 1] = d2;
            mem[(n2 << 8) + n1] = d1;
            reg.M = 6;
            reg.T = 20;
        }

        //skipped: LD (nn), IX
        //skipped: LD (nn), IY

        /// <summary>
        ///     LD SP, HL
        ///     SP ← HL
        /// </summary>
        public static void LDSPHL(Register reg)
        {
            reg.SP = (short) ((reg.H << 8) + reg.L);
            reg.M = 1;
            reg.T = 6;
        }

        //skipped: LD SP, IX
        //skipped: LD SP, IY

        /// <summary>
        ///     PUSH qq
        ///     (SP – 2) ← qqL, (SP – 1) ← qqH
        /// </summary>
        public static void PUSHqq(Register reg, MMU mem, ref byte q1, ref byte q2)
        {
            mem[--reg.SP] = q2;
            mem[--reg.SP] = q1;

            reg.M = 3;
            reg.T = 11;
        }

        //skipped: PUSH IX
        //skipped: PUSH IY

        /// <summary>
        ///     POP qq
        ///     qqH ← (SP+1), qqL ← (SP)
        /// </summary>
        public static void POPqq(Register reg, MMU mem, ref byte q1, ref byte q2)
        {
            q1 = mem[reg.SP++];
            q2 = mem[reg.SP++];

            reg.M = 3;
            reg.T = 10;
        }

        //skipped: POP IX
        //skipped: POP IY

        #endregion

        #region Exchange, Block Transfer, and Search Group

        /// <summary>
        ///     EX DE, HL
        ///     DE ↔ HL
        /// </summary>
        public static void EXDEHL(Register reg, MMU mem)
        {
            byte tmp = reg.D;
            reg.D = reg.H;
            reg.H = tmp;

            tmp = reg.E;
            reg.E = reg.H;
            reg.H = tmp;

            reg.M = 1;
            reg.T = 4;
        }

        //skipped: EX AF, AF′
        //skipped: EXX

        /// <summary>
        ///     EX (SP), HL
        ///     H ↔ (SP+1), L ↔ (SP)
        /// </summary>
        public static void EXSPHL(Register reg, MMU mem)
        {
            byte tmp = reg.H;
            reg.H = mem[reg.SP + 1];
            mem[reg.SP + 1] = tmp;

            tmp = reg.L;
            reg.L = mem[reg.SP];
            mem[reg.SP] = tmp;

            reg.M = 5;
            reg.T = 19;
        }

        //skipped: EX (SP), IX
        //skipped: EX (SP), IY

        /// <summary>
        ///     LDI
        ///     (DE) ← (HL), DE ← DE + 1, HL ← HL + 1, BC ← BC – 1
        /// </summary>
        public static void LDI(Register reg, MMU mem)
        {
            mem[(reg.D << 8) + reg.E] = mem[(reg.H << 8) + reg.L];

            unchecked
            {
                if (reg.E == 255) reg.D++;
                reg.E++;

                if (reg.L == 255) reg.H++;
                reg.L++;

                if (reg.C == 0) reg.B--;
                reg.C--;

                //TODO: check for overflow
            }
            reg.M = 4;
            reg.T = 16;
        }

        /*
         * Left of at LDIR, page 130 (pdf p. 144) of um0080.pdf
         */

        #endregion

        //TODO: condition bits
    }

    // ReSharper restore InconsistentNaming
}