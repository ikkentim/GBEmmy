// GBEmmy
// Copyright (C) 2014 Tim Potze
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <http://unlicense.org>

namespace GBEmmy.Processor.Opcode.Operation
{
    internal class CallOperation : IOperation
    {
        public bool Call(Z80 cpu, Operand operand1, Operand operand2, byte embedded)
        {
            var dest = (ushort) (cpu.Memory[cpu.Register.PC++] | (cpu.Memory[cpu.Register.PC++] << 8));
            switch (operand1)
            {
                case Operand.Carry:
                case Operand.Zero:
                case Operand.NotCarry:
                case Operand.NotZero:
                    if (!cpu.HasFlag(operand1)) return false;
                    break;
            }

            cpu.Memory[--cpu.Register.SP] = (byte) (cpu.Register.PC >> 8);
            cpu.Memory[--cpu.Register.SP] = (byte) cpu.Register.PC;
            cpu.Register.PC = dest;

            return true;
        }
    }
}