
namespace GBEmmy.Opcode.Operation
{
    class AddOperation : IOperation
    {
        public bool Call(Z80 cpu, Operand operand1, Operand operand2, byte embedded)
        {
            if (cpu[operand1] is byte)
            {
                int temp = cpu.GetByte(operand1) + cpu.GetByte(operand2);

                cpu.ToggleFlag(Flags.Carry, temp > 0xFF);
                cpu.ToggleFlag(Flags.HalfCarry, (cpu.GetByte(operand1) & 0xF) + (cpu.GetByte(operand2) & 0xF) > 0xF);
                cpu.SetByte(operand1, (byte) temp);
                cpu.ToggleFlag(Flags.Zero, cpu.GetByte(operand1) == 0);
            }
            else
            {
                int temp = cpu.GetWord(operand1) + cpu.GetWord(operand2);
                cpu.ToggleFlag(Flags.Carry, temp > 0xFFFF);
                cpu.ToggleFlag(Flags.HalfCarry, (cpu.GetWord(operand1) & 0xFFF) + (cpu.GetWord(operand2) & 0xFFF) > 0xFFF);
                cpu.SetWord(operand1, (ushort)temp);
            }
            return true;
        }
    }
}
