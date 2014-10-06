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
    public class IncOperation : IOperation
    {
        public bool Call(Z80 cpu, Operand operand1, Operand operand2, byte embedded)
        {
            object v = cpu[operand1];

            if (v is byte)
            {
                var w = (byte) v;
                w++;
                cpu.SetByte(operand1, w);
                cpu.ToggleFlag(Flags.Zero, w == 0);
                cpu.ToggleFlag(Flags.HalfCarry, (w & 0xF) == 0);
                cpu.ToggleFlag(Flags.Subtract, w == 0);
            }
            else
            {
                cpu.SetWord(operand1, (ushort) (((ushort) v) + 1));
            }
            return true;
        }
    }
}