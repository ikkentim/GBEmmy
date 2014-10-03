using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBEmmy.Opcode.Operation
{
    public class RlcaOperation : IOperation
    {
        public bool Call(Z80 cpu, Operand operand1, Operand operand2, byte embedded)
        {
            cpu.ToggleFlag(Flags.Carry, (cpu.GetByte(Operand.A) & 0x80) != 0);

            if (cpu.HasFlag(Flags.Carry))
                cpu.SetByte(Operand.A, (byte) ((cpu.GetByte(Operand.A) << 1) | 0x01));
            else
                cpu.SetByte(Operand.A, (byte) (cpu.GetByte(Operand.A) << 1));

            cpu.ToggleFlag(Flags.Zero, false);
            cpu.ToggleFlag(Flags.Subtract, false);
            cpu.ToggleFlag(Flags.HalfCarry, false);

            return true;
        }
    }
}
