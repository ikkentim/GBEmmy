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
    internal class AdcOperation : IOperation
    {
        public bool Call(Z80 cpu, Operand operand1, Operand operand2, byte embedded)
        {
            int temp;
            if (cpu.HasFlag(Flags.Carry))
            {
                temp = cpu.GetByte(operand1) + cpu.GetByte(operand2) + 1;
                cpu.ToggleFlag(Flags.HalfCarry, (cpu.GetByte(operand1) & 0xF) + (cpu.GetByte(operand2) & 0xF) > 0xE);
            }
            else
            {
                temp = cpu.GetByte(operand1) + cpu.GetByte(operand2);
                cpu.ToggleFlag(Flags.HalfCarry, (cpu.GetByte(operand1) & 0xF) + (cpu.GetByte(operand2) & 0xF) > 0xF);
            }
            cpu.ToggleFlag(Flags.Carry, temp > 0xFF);
            cpu.SetByte(operand1, (byte) temp);
            cpu.ToggleFlag(Flags.Zero, cpu.GetByte(operand1) == 0);

            return true;
        }
    }
}