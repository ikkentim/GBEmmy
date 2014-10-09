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
    internal class SubOperation : IOperation
    {
        public bool Call(Z80 cpu, Operand operand1, Operand operand2, byte embedded)
        {
            //a -= operand1

            cpu.ToggleFlag(Flags.HalfCarry, (cpu.Register.A & 0xF) < (cpu.GetByte(operand1) & 0xF));
            cpu.ToggleFlag(Flags.Carry, cpu.Register.A < cpu.GetByte(operand1));
            cpu.ToggleFlag(Flags.Subtract, true);
            cpu.ToggleFlag(Flags.Zero, (cpu.Register.A -= cpu.GetByte(operand1)) == 0);

            return true;
        }
    }
}