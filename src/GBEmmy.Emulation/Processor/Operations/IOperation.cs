namespace GBEmmy.Emulation.Processor.Operations
{
    public interface IOperation
    {
        bool Call(Z80 cpu, Operand operand1, Operand operand2, byte embedded);
    }
}
