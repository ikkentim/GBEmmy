namespace GBEmmy.Opcode.Operation
{
    public class IncOperation : IOperation
    {
        public bool Call(Z80 cpu, Operand operand1, Operand operand2, byte embedded)
        {
            var v = cpu[operand1];

            if (v is byte)
            {
                var w = (byte) v;
                w++;
                cpu.SetByte(operand1, w);
                cpu.ToggleFlag(Flags.Zero, w == 0);
                cpu.ToggleFlag(Flags.HalfCarry, (w & 0xF) == 0);
                cpu.ToggleFlag(Flags.Subtract, w == 0);
            }
            else
            {
                cpu.SetWord(operand1, (ushort)(((ushort) v) + 1));
            }
            return true;
        }
    }
}
