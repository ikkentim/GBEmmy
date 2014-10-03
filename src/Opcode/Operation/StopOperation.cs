using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBEmmy.Opcode.Operation
{
    public class StopOperation : IOperation
    {
        public bool Call(Z80 cpu, Operand operand1, Operand operand2, byte embedded)
        {
            throw new NotImplementedException();
        }
    }
}
