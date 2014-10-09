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
    public class WordOperandIndexer
    {
        private readonly Z80 _cpu;

        public WordOperandIndexer(Z80 cpu)
        {
            _cpu = cpu;
        }

        public ushort this[Operand operand]
        {
            get
            {
                switch (operand)
                {
                    case Operand.AF:
                        return _cpu.AF;
                    case Operand.BC:
                        return _cpu.BC;
                    case Operand.DE:
                        return _cpu.DE;
                    case Operand.HL:
                        return _cpu.HL;
                    case Operand.SP:
                        return _cpu.SP;
                    case Operand.Word:
                        return _cpu.ReadWord();
                    default:
                        throw new Exception("Invalid operand");
                }
            }
            set
            {
                switch (operand)
                {
                    case Operand.AF:
                        _cpu.AF = value;
                        break;
                    case Operand.BC:
                        _cpu.BC = value;
                        break;
                    case Operand.DE:
                        _cpu.DE = value;
                        break;
                    case Operand.HL:
                        _cpu.HL = value;
                        break;
                    case Operand.SP:
                        _cpu.SP = value;
                        break;
                    default:
                        throw new Exception("Invalid operand");
                }
            }
        }
    }
}