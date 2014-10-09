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

namespace GBEmmy.Emulation.Processor
{
    public class FlagsIndexer
    {
        private readonly Z80 _cpu;

        public FlagsIndexer(Z80 cpu)
        {
            _cpu = cpu;
        }

        public bool this[Flags flag]
        {
            get { return (_cpu.F & (byte) flag) != 0; }
            set
            {
                if (value)
                    _cpu.F |= (byte) flag;
                else
                    _cpu.F &= (byte) ~flag;
            }
        }

        public bool this[Operand operand]
        {
            get
            {
                switch (operand)
                {
                    case Operand.Carry:
                        return this[Flags.Carry];
                    case Operand.NotCarry:
                        return !this[Flags.Carry];
                    case Operand.Zero:
                        return this[Flags.Zero];
                    case Operand.NotZero:
                        return !this[Flags.Zero];
                    case Operand.None:
                        return true;
                    default:
                        throw new Exception("Invalid flags operand");
                }
            }
        }

        public void Reset()
        {
            _cpu.F = 0;
        }
    }
}