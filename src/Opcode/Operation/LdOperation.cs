namespace GBEmmy.Opcode.Operation
{
    public class LdOperation : IOperation
    {
        public bool Call(Z80 cpu, Operand operand1, Operand operand2)
        {
            cpu[operand1] = cpu[operand2];

            return true;
        }
    }
}
