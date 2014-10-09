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

namespace GBEmmy.Emulation.Processor.Operations
{
    /// <summary>
    ///     DAA: ???.
    /// </summary>
    public class DaaOperation : IOperation
    {
        public bool Call(Z80 cpu, Operand operand1, Operand operand2, byte embedded)
        {
            //TODO: Requires testing
            if (cpu.Flags[Flags.Subtract])
            {
                if (cpu.Flags[Flags.HalfCarry]) cpu.A -= 0x06;
                if (cpu.Flags[Flags.Carry]) cpu.A -= 0x60;
            }
            else
            {
                if (cpu.Flags[Flags.Carry] || cpu.A > 0x99)
                {
                    cpu.A += cpu.Flags[Flags.HalfCarry] || (cpu.A & 0x0F) > 0x09
                        ? (byte) 0x66
                        : (byte) 0x60;
                    cpu.Flags[Flags.Carry] = true;
                }
                else if (cpu.Flags[Flags.HalfCarry] || (cpu.A & 0x0F) > 0x09) cpu.A += 0x06;
            }

            if (cpu.A == 0)
            {
                cpu.Flags[Flags.Zero] = true;
            }
            else
            {
                cpu.Flags[Flags.All ^ Flags.Zero] = true;
            }

            cpu.Flags[Flags.HalfCarry] = false;

            return true;
        }
    }
}