using System.Diagnostics;

namespace GBEmmy.Opcode.Operation
{
    public class EmptyOperation : IOperation
    {
        public bool Call(Z80 cpu, Operand operand1, Operand operand2)
        {
            Debug.WriteLine("Call to empty operation");
            return true;
        }
    }
}
