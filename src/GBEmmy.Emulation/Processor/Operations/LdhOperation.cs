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
    ///     LDH operand1,operand2: one of both is an $FF00|embedded-byte. Load operand2 into operand1.
    /// </summary>
    public class LdhOperation : IOperation
    {
        public bool Call(Z80 cpu, Operand operand1, Operand operand2, byte embedded)
        {
            if (operand1 == Operand.MemoryByte)
                cpu.Memory[(ushort)(0xFF00 | cpu.Bytes[operand1])] = cpu.Bytes[operand2];
            else
                cpu.Bytes[operand1] = cpu.Memory[(ushort)(0xFF00 | cpu.Bytes[operand2])];

            return true;
        }
    }
}