namespace GBEmmy.Opcode.Operation
{
    public interface IOperation
    {
        bool Call(Z80 cpu, Operand operand1, Operand operand2);
    }
}
