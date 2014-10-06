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
    internal class LdhlOperation : IOperation
    {
        public bool Call(Z80 cpu, Operand operand1, Operand operand2, byte embedded)
        {
            var temp16 = (ushort) (sbyte) cpu.Memory[cpu.Register.PC++];
            int temp = cpu.Register.SP + temp16;

            cpu.ResetFlags();
            cpu.ToggleFlag(Flags.HalfCarry, ((cpu.Register.SP ^ temp16 ^ temp) & 0x10) != 0);
            cpu.ToggleFlag(Flags.Carry, ((cpu.Register.SP ^ temp16 ^ temp) & 0x100) != 0);

            var tempHL = (ushort) temp;

            cpu.Register.H = (byte) (tempHL >> 8);
            cpu.Register.L = (byte) (tempHL);

            return true;
        }
    }
}