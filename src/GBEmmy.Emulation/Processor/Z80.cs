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
using GBEmmy.Emulation.Cartridges;
using GBEmmy.Emulation.Memory;

namespace GBEmmy.Emulation.Processor
{
    public partial class Z80
    {

        #region Timing

        private double _cycles;

        #endregion

        public Z80(Cartridge cartridge)
        {
            LoadRegisters();

            Memory = cartridge.GetController();
        }

        public MBC Memory { get; set; }

        public byte Read()
        {
            return Memory[PC++];
        }

        public ushort ReadWord()
        {
            return (ushort) (Read() | (Read() << 8));
        }


        public void Run(double duration)
        {
            _cycles += (4194304.0 * 1 * duration);

            while (_cycles > 0.0)
            {
                ushort addr = PC++;

                byte instrid = Memory[addr];

                //Debug.WriteLine("C: {0} > {1}", addr, instrid);
                _cycles -= (instrid == 0xCB
                    ? OpcodeTable.Cb[Memory[PC++]]
                    : OpcodeTable.Base[instrid]).Call(this);
            }
        }
    }
}