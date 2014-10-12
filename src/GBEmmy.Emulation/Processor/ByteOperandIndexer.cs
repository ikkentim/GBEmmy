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
using System.Diagnostics;

namespace GBEmmy.Emulation.Processor
{
    public class ByteOperandIndexer
    {
        private readonly Z80 _cpu;

        public ByteOperandIndexer(Z80 cpu)
        {
            _cpu = cpu;
        }

        public byte this[Operand operand]
        {
            get
            {
                switch (operand)
                {
                    case Operand.A:
                        return _cpu.A;
                    case Operand.B:
                        return _cpu.B;
                    case Operand.C:
                        return _cpu.C;
                    case Operand.D:
                        return _cpu.D;
                    case Operand.E:
                        return _cpu.E;
                    case Operand.H:
                        return _cpu.H;
                    case Operand.L:
                        return _cpu.L;
                    case Operand.Memory:
                        return _cpu.Memory[_cpu.Read(), _cpu.Read()];
                    case Operand.MemoryBC:
                        return _cpu.Memory[_cpu.BC];
                    case Operand.MemoryByte:
                        return _cpu.Memory[_cpu.Read(), 0xFF];
                    case Operand.MemoryC:
                        return _cpu.Memory[_cpu.C, 0xFF];
                    case Operand.MemoryDE:
                        return _cpu.Memory[_cpu.DE];
                    case Operand.MemoryHL:
                        return _cpu.Memory[_cpu.HL];
                    case Operand.Byte:
                        return _cpu.Read();
                    default:
                        throw new Exception("Invalid operand");
                }
            }
            set
            {
                switch (operand)
                {
                    case Operand.A:
                        _cpu.A = value;
                        break;
                    case Operand.B:
                        _cpu.B = value;
                        break;
                    case Operand.C:
                        _cpu.C = value;
                        break;
                    case Operand.D:
                        _cpu.D = value;
                        break;
                    case Operand.E:
                        _cpu.E = value;
                        break;
                    case Operand.H:
                        _cpu.H = value;
                        break;
                    case Operand.L:
                        _cpu.L = value;
                        break;
                    case Operand.Memory:
                        _cpu.Memory[_cpu.Read(), _cpu.Read()] = value;
                        break;
                    case Operand.MemoryBC:
                        _cpu.Memory[_cpu.BC] = value;
                        break;
                    case Operand.MemoryByte:
                        _cpu.Memory[_cpu.Read(), 0xFF] = value;
                        break;
                    case Operand.MemoryC:
                        _cpu.Memory[_cpu.C, 0xFF] = value;
                        break;
                    case Operand.MemoryDE:
                        _cpu.Memory[_cpu.DE] = value;
                        break;
                    case Operand.MemoryHL:
                        _cpu.Memory[_cpu.HL] = value;
                        break;
                    default:
                        throw new Exception("Invalid operand");
                }
            }
        }
    }
}