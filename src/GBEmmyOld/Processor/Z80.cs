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
using GBEmmy.Cartridges;
using GBEmmy.Memory;
using GBEmmy.Processor.Opcode;
using GBEmmy.Registers;

namespace GBEmmy.Processor
{
    public class Z80
    {
        private double _cycles;

        public Z80(Cartridge cartridge)
        {
            Clock = new Clock();
            Memory = cartridge.GetController();
            Register = new CPURegister();

        }

        #region Properties

        public Clock Clock { get; private set; }
        public MBC Memory { get; private set; }
        public CPURegister Register { get; private set; }

        public object this[Operand o]
        {
            get
            {
                switch (o)
                {
                    case Operand.A:
                    case Operand.B:
                    case Operand.Byte:
                    case Operand.C:
                    case Operand.D:
                    case Operand.E:
                    case Operand.H:
                    case Operand.L:
                    case Operand.Memory:
                    case Operand.MemoryBC:
                    case Operand.MemoryByte:
                    case Operand.MemoryDE:
                    case Operand.MemoryHL:
                    case Operand.MemoryC:
                        return GetByte(o);
                    case Operand.BC:
                    case Operand.AF:
                    case Operand.DE:
                    case Operand.HL:
                    case Operand.SP:
                    case Operand.SignedByte:
                    case Operand.Word:
                        return GetWord(o);
                    default:
                        throw new NotImplementedException();
                }
            }
            set
            {
                switch (o)
                {
                    case Operand.A:
                    case Operand.B:
                    case Operand.Byte:
                    case Operand.C:
                    case Operand.D:
                    case Operand.E:
                    case Operand.H:
                    case Operand.L:
                    case Operand.Memory:
                    case Operand.MemoryBC:
                    case Operand.MemoryByte:
                    case Operand.MemoryDE:
                    case Operand.MemoryHL:
                    case Operand.MemoryC:
                        SetByte(o, (byte) value);
                        break;
                    case Operand.BC:
                    case Operand.AF:
                    case Operand.DE:
                    case Operand.HL:
                    case Operand.SP:
                    case Operand.SignedByte:
                    case Operand.Word:
                        SetWord(o, (ushort) value);
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        #endregion

        #region Bytes

        public byte GetByte(Operand o)
        {
            switch (o)
            {
                case Operand.A:
                    return Register.A;
                case Operand.B:
                    return Register.B;
                case Operand.Byte:
                    return Memory[Register.PC++];
                case Operand.C:
                    return Register.C;
                case Operand.D:
                    return Register.D;
                case Operand.E:
                    return Register.E;
                case Operand.H:
                    return Register.H;
                case Operand.L:
                    return Register.L;
                case Operand.Memory:
                    ushort low = Register.PC++;
                    return Memory[Memory[Register.PC++], Memory[low]];
                case Operand.MemoryBC:
                    return Memory[Register.B, Register.C];
                case Operand.MemoryByte:
                    return Memory[0xFF, Memory[Register.PC++]];
                case Operand.MemoryDE:
                    return Memory[Register.D, Register.E];
                case Operand.MemoryHL:
                    return Memory[Register.H, Register.L];
                case Operand.MemoryC:
                    return Memory[0xFF, Register.C];
                default:
                    throw new Exception(string.Format("Invalid operand {0}", o));
            }
        }

        public byte SetByte(Operand o, byte value)
        {
            switch (o)
            {
                case Operand.A:
                    return Register.A = value;
                case Operand.B:
                    return Register.B = value;
                case Operand.Byte:
                    throw new NotImplementedException();
                case Operand.C:
                    return Register.C = value;
                case Operand.D:
                    return Register.D = value;
                case Operand.E:
                    return Register.E = value;
                case Operand.H:
                    return Register.H = value;
                case Operand.L:
                    return Register.L = value;
                case Operand.Memory:
                    ushort low = Register.PC++;
                    return Memory[Memory[Register.PC++], Memory[low]] = value;
                case Operand.MemoryBC:
                    return Memory[Register.B, Register.C] = value;
                case Operand.MemoryByte:
                    return Memory[0xFF, Memory[Register.PC++]] = value;
                case Operand.MemoryDE:
                    return Memory[Register.D, Register.E] = value;
                case Operand.MemoryHL:
                    return Memory[Register.H, Register.L] = value;
                case Operand.MemoryC:
                    return Memory[0xFF, Register.C] = value;
                default:
                    throw new Exception(string.Format("Invalid operand {0}", o));
            }
        }

        #endregion

        #region Flags

        public bool HasFlag(Operand o)
        {
            switch (o)
            {
                case Operand.Carry:
                    return Register.Flags.HasFlag(Flags.Carry);
                case Operand.NotCarry:
                    return !Register.Flags.HasFlag(Flags.Carry);
                case Operand.NotZero:
                    return !Register.Flags.HasFlag(Flags.Zero);
                case Operand.Zero:
                    return Register.Flags.HasFlag(Flags.Zero);
                case Operand.None:
                    return true;
                default:
                    throw new Exception(string.Format("Invalid operand {0}", o));
            }
        }

        public bool HasFlag(Flags f)
        {
            return Register.Flags.HasFlag(f);
        }

        public void ResetFlags()
        {
            Register.Flags = Flags.None;
        }

        public void ToggleFlag(Flags f, bool toggle)
        {
            if (toggle)
            {
                Register.Flags |= f;
            }
            else
            {
                Register.Flags &= (Flags.All ^ f);
            }
        }

        #endregion

        #region Word

        public ushort GetWord(Operand o)
        {
            switch (o)
            {
                case Operand.BC:
                    return (ushort) ((Register.B << 8) | Register.C);
                case Operand.DE:
                    return (ushort) ((Register.D << 8) | Register.E);
                case Operand.HL:
                    return (ushort) ((Register.H << 8) | Register.L);
                case Operand.AF:
                    return (ushort)((Register.A << 8) | (byte)Register.Flags);
                case Operand.SP:
                    return Register.SP;
                case Operand.SignedByte:
                    return (ushort) (sbyte) Memory[Register.SP++];
                case Operand.Word:
                    //correct order of pc? h/l?
                    return (ushort) ((Memory[Register.PC++] << 8) | Memory[Register.PC++]);
                default:
                    throw new Exception(string.Format("Invalid operand {0}", o));
            }
        }

        public void SetWord(Operand o, ushort value)
        {
            switch (o)
            {
                case Operand.BC:
                    Register.B = (byte) (value >> 8);
                    Register.C = (byte) value;
                    break;
                case Operand.DE:
                    Register.D = (byte) (value >> 8);
                    Register.E = (byte) value;
                    break;
                case Operand.HL:
                    Register.H = (byte) (value >> 8);
                    Register.L = (byte) value;
                    break;
                case Operand.AF:
                    Register.A = (byte)(value >> 8);
                    Register.Flags = (Flags)(byte)value;
                    break;
                case Operand.SP:
                    Register.SP = value;
                    return;
                case Operand.SignedByte:
                    //???
                    throw new NotImplementedException();
                case Operand.Word:
                    throw new NotImplementedException();
                default:
                    throw new Exception(string.Format("Invalid operand {0}", o));
            }
        }

        #endregion

        public void Run(double duration)
        {
            _cycles += (4194304.0*1*duration);

            while (_cycles > 0.0)
            {
                ushort addr = Register.PC++;

                int instrid = Memory[addr];



                Debug.WriteLine("C: {0} > {1}", addr, instrid);
                _cycles -= (instrid == 0xCB
                    ? OpcodeInstructionsTable.Cb[Memory[Register.PC++]]
                    : OpcodeInstructionsTable.Base[instrid]).Call(this);

                Clock.M += Register.M;
                Clock.T += Register.T;
            }
        }
    }
}