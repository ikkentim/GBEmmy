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
    ///     INC operand1: Increase operand1 by 1.
    /// </summary>
    public class IncOperation : IOperation
    {
        public bool Call(Z80 cpu, Operand operand1, Operand operand2, byte embedded)
        {
            if (cpu.IsByte(operand1))
            {
                cpu.Flags[Flags.Zero] = ++cpu.Bytes[operand1] == 0;
                cpu.Flags[Flags.HalfCarry] = (cpu.Bytes[operand1] & 0xF) == 0;
                cpu.Flags[Flags.Subtract] = cpu.Bytes[operand1] == 0;
            }
            else
            {
                cpu.Words[operand1]++;
            }

            return true;
        }
    }
}