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
    ///     ADD A, operand2: Add operand2 to operand1(which is always A).
    /// </summary>
    public class AddOperation : IOperation
    {
        public bool Call(Z80 cpu, Operand operand1, Operand operand2, byte embedded)
        {
            if (cpu.IsByte(operand1))
            {
                int temp = cpu.Bytes[operand1] + cpu.Bytes[operand2];

                cpu.Flags[Flags.Carry] = temp > 0xFF;
                cpu.Flags[Flags.HalfCarry] = (cpu.Bytes[operand1] & 0xF) + (cpu.Bytes[operand2] & 0xF) > 0xF;
                cpu.Flags[Flags.Zero] = (cpu.Bytes[operand1] = (byte) temp) == 0;
            }
            else
            {
                int temp = cpu.Words[operand1] + cpu.Words[operand2];

                cpu.Flags[Flags.Carry] = temp > 0xFFFF;
                cpu.Flags[Flags.HalfCarry] = (cpu.Words[operand1] & 0xFFF) + (cpu.Words[operand2] & 0xFFF) > 0xFFF;
                cpu.Words[operand1] = (ushort) temp;
            }
            return true;
        }
    }
}