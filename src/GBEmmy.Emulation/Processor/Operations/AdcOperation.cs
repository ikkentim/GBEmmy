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
    ///     ADC A, operand2: Add operand2 to operand1(which is always A), taking the carry flag into account.
    /// </summary>
    public class AdcOperation : IOperation
    {
        public bool Call(Z80 cpu, Operand operand1, Operand operand2, byte embedded)
        {
            byte carry = cpu.Flags[Flags.Carry] ? (byte) 0x01 : (byte) 0x00;
            cpu.Flags[Flags.HalfCarry] = (cpu.Bytes[operand1] & 0xF) + (cpu.Bytes[operand2] & 0xF) > 0xF - carry;

            int temp = cpu.Bytes[operand1] + cpu.Bytes[operand2] + carry;
            cpu.Flags[Flags.Carry] = temp > 0xFF;
            cpu.Flags[Flags.Zero] = (cpu.Bytes[operand1] = (byte) temp) == 0;
            return true;
        }
    }
}