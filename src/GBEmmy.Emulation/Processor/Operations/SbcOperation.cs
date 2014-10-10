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
    ///     SBC operand1,operand2: subtract operand2 from operand1(always A) taking the carry flag into account.
    /// </summary>
    public class SbcOperation : IOperation
    {
        public bool Call(Z80 cpu, Operand operand1, Operand operand2, byte embedded)
        {
            cpu.Flags[Flags.HalfCarry] = (cpu.Bytes[operand1] & 0xF) - (cpu.Bytes[operand2] & 0xF) < 1;
            cpu.Flags[Flags.Carry] = cpu.Bytes[operand1] - cpu.Bytes[operand2] < 1;

            cpu.Flags[Flags.Zero] =
                (cpu.A = (byte) (cpu.Bytes[operand1] - cpu.Bytes[operand2] - (cpu.Flags[Flags.Carry] ? 1 : 0))) == 0;

            cpu.Flags[Flags.Subtract] = true;

            return true;
        }
    }
}