using System;
using System.Resources;
using System.Windows.Forms;
using GBEmmy.Opcode;

namespace GBEmmy
{
    public class Z80
    {
        public Clock Clock { get; private set; }
        public MMU Memory { get; private set; }
        public Register Register { get; private set; }

        public Z80()
        {
            Clock = new Clock();
            Memory = new MMU();
            Register = new Register();

            var table = Opcode.OpcodeInstructionsTable.Base;
        }

        public object this[Operand o]
        {
            get
            {
                switch (o)
                {
                    case Operand.A:
                    case Operand.AF:
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
                        return GetByte(o);
                    case Operand.BC:
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
                    case Operand.AF:
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
                        if(!(value is byte)) throw new Exception();
                        SetByte(o, (byte)value);
                        break;
                    case Operand.BC:
                    case Operand.DE:
                    case Operand.HL:
                    case Operand.SP:
                    case Operand.SignedByte:
                    case Operand.Word:
                        if (!(value is ushort)) throw new Exception();
                        SetWord(o, (ushort)value);
                        break;
                    default:
                        throw new NotImplementedException();
                }   
            }
        }
        public byte GetByte(Operand o)
        {
            switch (o)
            {
                case Operand.A:
                    return Register.A;
                case Operand.AF:
                    throw new NotImplementedException(); //?
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
                    var low = Register.PC++;
                    return Memory[Memory[Register.PC++], Memory[low]];
                case Operand.MemoryBC:
                    return Memory[Register.B, Register.C];
                case Operand.MemoryByte:
                    return Memory[0xFF, Memory[Register.PC++]];
                case Operand.MemoryDE:
                    return Memory[Register.D, Register.E];
                case Operand.MemoryHL:
                    return Memory[Register.H, Register.L];
                default:
                    throw new Exception(string.Format("Invalid operand {0}", o));
            }
        }

        public void SetByte(Operand o, byte value)
        {
            switch (o)
            {
                case Operand.A:
                    Register.A = value;
                    break;
                case Operand.AF:
                    throw new NotImplementedException(); //?
                case Operand.B:
                    Register.B = value;
                    break;
                case Operand.Byte:
                    throw new NotImplementedException();
                case Operand.C:
                    Register.C = value;
                    break;
                case Operand.D:
                    Register.D = value;
                    break;
                case Operand.E:
                    Register.E = value;
                    break;
                case Operand.H:
                    Register.H = value;
                    break;
                case Operand.L:
                    Register.L = value;
                    break;
                case Operand.Memory:
                    var low = Register.PC++;
                    Memory[Memory[Register.PC++], Memory[low]] = value;
                    break;
                case Operand.MemoryBC:
                    Memory[Register.B, Register.C] = value;
                    break;
                case Operand.MemoryByte:
                    Memory[0xFF, Memory[Register.PC++]] = value;
                    break;
                case Operand.MemoryDE:
                    Memory[Register.D, Register.E] = value;
                    break;
                case Operand.MemoryHL:
                    Memory[Register.H, Register.L] = value;
                    break;
                default:
                    throw new Exception(string.Format("Invalid operand {0}", o));
            }
        }

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

                default:
                    throw new Exception(string.Format("Invalid operand {0}", o));
            }
        }
        public bool HasFlag(Flags f)
        {
            return Register.Flags.HasFlag(f);
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
                    Register.B = (byte)(value >> 8);
                    Register.C = (byte) value;
                    break;
                case Operand.DE:
                    Register.D = (byte)(value >> 8);
                    Register.E = (byte)value;
                    break;
                case Operand.HL:
                    Register.H = (byte)(value >> 8);
                    Register.L = (byte)value;
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
    }
}
