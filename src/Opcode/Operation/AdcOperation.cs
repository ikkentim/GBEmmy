using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBEmmy.Opcode.Operation
{
    class AdcOperation : IOperation
    {
        public bool Call(Z80 cpu, Operand operand1, Operand operand2, byte embedded)
        {
            int temp;
            if (cpu.HasFlag(Flags.Carry))
            {
                temp = cpu.GetByte(operand1) + cpu.GetByte(operand2) + 1;
                cpu.ToggleFlag(Flags.HalfCarry, (cpu.GetByte(operand1) & 0xF) + (cpu.GetByte(operand2) & 0xF) > 0xE);
            }
            else
            {
                temp = cpu.GetByte(operand1) + cpu.GetByte(operand2);
                cpu.ToggleFlag(Flags.HalfCarry, (cpu.GetByte(operand1) & 0xF) + (cpu.GetByte(operand2) & 0xF) > 0xF);
            }
            cpu.ToggleFlag(Flags.Carry,  temp > 0xFF);
            cpu.SetByte(operand1, (byte) temp);
            cpu.ToggleFlag(Flags.Zero, cpu.GetByte(operand1) == 0);

            return true;
        }
    }
}
