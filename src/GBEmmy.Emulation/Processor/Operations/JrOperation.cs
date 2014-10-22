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
    ///     JR operand1, address: If operand1 is a true assertion, jump the passed bytes relative to the PC.
    /// </summary>
    public class JrOperation : IOperation
    {
        public bool Call(Z80 cpu, Operand operand1, Operand operand2, byte embedded)
        {
            var isassert = cpu.IsAssertion(operand1);
            if (isassert)
            {
                var step = (sbyte) cpu.Memory[cpu.PC++];

                var result = !cpu.Flags[(operand1)];

                if (!result) return false;
                cpu.PC = (ushort)(cpu.PC + step);

            return true;
            }
            else
            {
                cpu.PC = (ushort)(cpu.PC + 1 + (sbyte)cpu.Memory[cpu.PC]);
                return true;
            }

        }
    }
}