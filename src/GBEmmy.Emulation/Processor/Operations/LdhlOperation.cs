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
    ///     LDHL SP,r8: increase the SP-register by the sbyte.
    /// </summary>
    public class LdhlOperation : IOperation
    {
        public bool Call(Z80 cpu, Operand operand1, Operand operand2, byte embedded)
        {
            var offset = (sbyte) cpu.Read();
            int temp = cpu.SP + offset;

            cpu.Flags.Reset();

            cpu.Flags[Flags.Carry] = offset >= 0 ? cpu.SP > temp : cpu.SP < temp;
            cpu.Flags[Flags.HalfCarry] = (((cpu.SP ^ offset ^ temp) & 0x1000) != 0);

            cpu.HL = (ushort) temp;

            return true;
        }
    }
}