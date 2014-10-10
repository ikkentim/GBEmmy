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

using System;

namespace GBEmmy.Emulation.Processor.Operations
{
    /// <summary>
    ///     HALT: Waits for an interupt or reset.
    /// </summary>
    public class HaltOperation : IOperation
    {
        public bool Call(Z80 cpu, Operand operand1, Operand operand2, byte embedded)
        {
            if ((cpu.IFF & 0x01) != 0)
            {
                cpu.PC--;
                cpu.IFF |= 0x80;
            }
            else
            {
                if ((cpu.Memory[0xFF0F] & cpu.Memory[0xFFFF]) != 0)
                {
                    cpu.IFF |= 0x100;
                }
                else
                {
                    cpu.PC--;
                    cpu.IFF |= 0x81;
                }
            }
            return true;
        }
    }
}