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
    internal class DaaOperation : IOperation
    {
        public bool Call(Z80 cpu, Operand operand1, Operand operand2, byte embedded)
        {
            if (cpu.HasFlag(Flags.Subtract))
            {
                if (cpu.HasFlag(Flags.HalfCarry)) cpu.Register.A -= 0x06;
                if (cpu.HasFlag(Flags.Carry)) cpu.Register.A -= 0x60;
            }
            else
            {
                if (cpu.HasFlag(Flags.Carry) || cpu.Register.A > 0x99)
                {
                    cpu.Register.A += cpu.HasFlag(Flags.HalfCarry) || (cpu.Register.A & 0x0F) > 0x09
                        ? (byte) 0x66
                        : (byte) 0x60;
                    cpu.ToggleFlag(Flags.Carry, true);
                }
                else if (cpu.HasFlag(Flags.HalfCarry) || (cpu.Register.A & 0x0F) > 0x09) cpu.Register.A += 0x06;
            }

            if (cpu.Register.A == 0)
            {
                cpu.ToggleFlag(Flags.Zero, true);
            }
            else
            {
                cpu.ToggleFlag(Flags.All, true);
                cpu.ToggleFlag(Flags.Zero, false);
            }

            cpu.ToggleFlag(Flags.HalfCarry, false);

            return true;
        }
    }
}