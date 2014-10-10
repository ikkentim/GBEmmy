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
using System.Diagnostics;
using GBEmmy.Emulation.Cartridges;
using GBEmmy.Emulation.Memory;
using GBEmmy.Emulation.Processor.Registers;

namespace GBEmmy.Emulation.Processor
{
    public partial class Z80
    {
        private IE _ie;
        private double _cycles;

        public ushort IFF { get; set; }

        public byte InterruptQueue { get; set; }
        public Z80(Cartridge cartridge)
        {
            LoadRegisters();

            Memory = cartridge.GetController(this);

            _ie = Memory.Registers.Get<IE>();

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
            //TODO: Implement double speed
            _cycles += (4194304.0 * 1 * duration);

            while (_cycles > 0.0)
            {
                byte instrid = Memory[PC++];//read instrid
                Debug.WriteLine("Z80: @${0:X2}: instr ${1:X2}", PC-1, instrid);

                if ((IFF & 0x100) != 0)
                {
                    IFF &= 0xFF;
                    PC--;
                }
                
                _cycles -= (instrid == 0xCB
                    ? OpcodeTable.Cb[Memory[PC++]]
                    : OpcodeTable.Base[instrid]).Call(this);//run

                if (InterruptQueue != 0)
                {
                    if (_ie.JoypadEnabled && (InterruptQueue & Interrupts.Joypad) != 0)
                    {
                        throw new NotImplementedException();
                    }
                    if (_ie.LCDCEnabled && (InterruptQueue & Interrupts.LCDC) != 0)
                    {
                        throw new NotImplementedException();
                    }
                    if (_ie.SerialEnabled && (InterruptQueue & Interrupts.Serial) != 0)
                    {
                        throw new NotImplementedException();
                    }
                    if (_ie.TimerEnabled && (InterruptQueue & Interrupts.Timer) != 0)
                    {
                        throw new NotImplementedException();
                    }
                    if (_ie.VBlankEnabled && (InterruptQueue & Interrupts.VBlank) != 0)
                    {
                        throw new NotImplementedException();
                    }
                }
            }
        }
    }
}