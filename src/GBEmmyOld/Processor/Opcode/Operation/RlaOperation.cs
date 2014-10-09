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
    public class RlaOperation : IOperation
    {
        public bool Call(Z80 cpu, Operand operand1, Operand operand2, byte embedded)
        {
            if (cpu.HasFlag(Flags.Carry))
            {
                cpu.ToggleFlag(Flags.Carry, cpu.HasFlag(Flags.Zero));
                cpu.SetByte(Operand.A, (byte) ((cpu.GetByte(Operand.A) << 1) | 0x01));
            }
            else
            {
                cpu.ToggleFlag(Flags.Carry, cpu.HasFlag(Flags.Zero));
                cpu.SetByte(Operand.A, (byte) (cpu.GetByte(Operand.A) << 1));
            }

            cpu.ToggleFlag(Flags.Carry, true);

            return true;
        }
    }
}