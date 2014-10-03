namespace GBEmmy.Opcode.Operation
{
    public class BitOperation : IOperation
    {
        public bool Call(Z80 cpu, Operand operand1, Operand operand2, byte embedded)
        {
            cpu.ToggleFlag(Flags.Zero, (cpu.GetByte(operand2) & (1 << embedded)) == 0);
            cpu.ToggleFlag(Flags.HalfCarry, true);
            cpu.ToggleFlag(Flags.Subtract, false);

            return true;
        }
    }
}
