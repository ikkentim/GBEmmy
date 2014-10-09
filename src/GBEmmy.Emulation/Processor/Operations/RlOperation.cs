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
    public class RlOperation : IOperation
    {
        public bool Call(Z80 cpu, Operand operand1, Operand operand2, byte embedded)
        {
            byte v;
            if (cpu.Flags[Flags.Carry])
                v = (byte) ((cpu.GetByte(operand1) << 1) | 0x01);
            else
                v = (byte) (cpu.GetByte(operand1) << 1);

            cpu.ToggleFlag(Flags.Carry, (cpu.GetByte(operand1) & 0x80) != 0);

            cpu.SetByte(operand1, v);

            cpu.ToggleFlag(Flags.Zero, cpu.GetByte(operand1) == 0);
            cpu.ToggleFlag(Flags.Subtract, false);
            cpu.ToggleFlag(Flags.HalfCarry, false);

            return true;
        }
    }
}