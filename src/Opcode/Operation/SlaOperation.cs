namespace GBEmmy.Opcode.Operation
{
    class SlaOperation : IOperation
    {
        public bool Call(Z80 cpu, Operand operand1, Operand operand2)
        {
            cpu.ToggleFlag(Flags.Carry, (cpu.GetByte(operand1) & 0x80) != 0);

            cpu.SetByte(operand1, (byte)(cpu.GetByte(operand1) << 1));

            cpu.ToggleFlag(Flags.Zero, cpu.GetByte(operand1) == 0);
            cpu.ToggleFlag(Flags.Subtract, false);
            cpu.ToggleFlag(Flags.HalfCarry, false);

            return true;
        }
    }
}
