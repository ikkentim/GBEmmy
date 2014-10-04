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

namespace GBEmmy.Opcode.Operation
{
    internal class SbcOperation : IOperation
    {
        public bool Call(Z80 cpu, Operand operand1, Operand operand2, byte embedded)
        {
            cpu.ToggleFlag(Flags.HalfCarry, (cpu.GetByte(operand1) & 0xF) - (cpu.GetByte(operand2) & 0xF) < 1);
            cpu.ToggleFlag(Flags.Carry, cpu.GetByte(operand1) - cpu.GetByte(operand2) < 1);

            if (cpu.HasFlag(Flags.Carry))
            {
                cpu.ToggleFlag(Flags.Zero,
                    (cpu.Register.A = (byte) (cpu.GetByte(operand1) - cpu.GetByte(operand2) - 1)) == 0);
            }
            else
            {
                cpu.ToggleFlag(Flags.Zero,
                    (cpu.Register.A = (byte) (cpu.GetByte(operand1) - cpu.GetByte(operand2))) == 0);
            }

            cpu.ToggleFlag(Flags.Subtract, true);

            return true;
        }
    }
}