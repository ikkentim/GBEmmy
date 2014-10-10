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

namespace GBEmmy.Emulation.Processor
{
    public partial class Z80
    {
        private RegisterPair _af;
        private RegisterPair _bc;
        private RegisterPair _de;
        private RegisterPair _hl;

        public ushort AF
        {
            get { return _af; }
            set { _af = value; }
        }

        public ushort BC
        {
            get { return _bc; }
            set { _bc = value; }
        }

        public ushort DE
        {
            get { return _de; }
            set { _de = value; }
        }

        public ushort HL
        {
            get { return _hl; }
            set { _hl = value; }
        }

        public ushort PC { get; set; }
        public ushort SP { get; set; }

        public byte A
        {
            get { return _af.Lower; }
            set { _af.Lower = value; }
        }

        public byte F
        {
            get { return _af.Upper; }
            set { _af.Upper = value; }
        }

        public byte B
        {
            get { return _bc.Lower; }
            set { _bc.Lower = value; }
        }

        public byte C
        {
            get { return _bc.Upper; }
            set { _bc.Upper = value; }
        }

        public byte D
        {
            get { return _de.Lower; }
            set { _de.Lower = value; }
        }

        public byte E
        {
            get { return _de.Upper; }
            set { _de.Upper = value; }
        }

        public byte H
        {
            get { return _hl.Lower; }
            set { _hl.Lower = value; }
        }

        public byte L
        {
            get { return _hl.Upper; }
            set { _hl.Upper = value; }
        }

        public FlagsIndexer Flags { get; private set; }

        public ByteOperandIndexer Bytes { get; private set; }

        public WordOperandIndexer Words { get; private set; }

        public bool IsByte(Operand operand)
        {
            switch (operand)
            {
                case Operand.A:
                case Operand.B:
                case Operand.C:
                case Operand.D:
                case Operand.E:
                case Operand.H:
                case Operand.L:
                case Operand.Memory:
                case Operand.MemoryBC:
                case Operand.MemoryByte:
                case Operand.MemoryC:
                case Operand.MemoryDE:
                case Operand.MemoryHL:
                case Operand.Byte:
                    return true;
                default:
                    return false;
            }
        }

        public bool IsAssertion(Operand operand)
        {
            switch (operand)
            {
                case Operand.Carry:
                case Operand.Zero:
                case Operand.NotCarry:
                case Operand.NotZero:
                    return true;
                default:
                    return false;
            }
        }

        private void LoadRegisters()
        {
            Flags = new FlagsIndexer(this);
            Bytes = new ByteOperandIndexer(this);
            Words = new WordOperandIndexer(this);

            AF = 0x01B0;
            BC = 0x0013;
            DE = 0x00D8;
            DE = 0x014D;
            SP = 0xFFFE;
        }
    }
}