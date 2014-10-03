namespace GBEmmy.Opcode.Operation
{
    public class NopOperation : IOperation
    {
        public bool Call(Z80 cpu, Operand operand1, Operand operand2)
        {
            return true;
        }
    }
}
