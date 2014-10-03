
namespace GBEmmy.Opcode.Operation
{
    class RrcaOperation : IOperation
    {
        public bool Call(Z80 cpu, Operand operand1, Operand operand2, byte embedded)
        {
            cpu.ToggleFlag(Flags.Carry, (cpu.GetByte(Operand.A) & 0x01) != 0);
      
            if (cpu.HasFlag(Flags.Carry))
                cpu.SetByte(Operand.A, (byte)((cpu.GetByte(Operand.A) >> 1) | 0x80));
            else
                cpu.SetByte(Operand.A, (byte)(cpu.GetByte(Operand.A) >> 1));

            return true;
        }
    }
}
