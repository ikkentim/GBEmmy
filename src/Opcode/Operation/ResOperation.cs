namespace GBEmmy.Opcode.Operation
{
    public class ResOperation : IOperation
    {
        public bool Call(Z80 cpu, Operand operand1, Operand operand2, byte embedded)
        {
            cpu.SetByte(operand2, (byte) (cpu.GetByte(operand2) & (~(1 << embedded))));

            return true;
        }
    }
}
