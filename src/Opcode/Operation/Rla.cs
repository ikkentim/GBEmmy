namespace GBEmmy.Opcode.Operation
{
    public class RlaOperation : IOperation
    {
        public bool Call(Z80 cpu, Operand operand1, Operand operand2, byte embedded)
        {
            if (cpu.HasFlag(Flags.Carry))
            {
                cpu.ToggleFlag(Flags.Carry, cpu.HasFlag(Flags.Zero));
                cpu.SetByte(Operand.A, (byte)((cpu.GetByte(Operand.A) << 1) | 0x01));
            }
            else
            {
                cpu.ToggleFlag(Flags.Carry, cpu.HasFlag(Flags.Zero));
                cpu.SetByte(Operand.A, (byte)(cpu.GetByte(Operand.A) << 1));
            }

            cpu.ToggleFlag(Flags.Carry, true);

            return true;
        }
    }
}
