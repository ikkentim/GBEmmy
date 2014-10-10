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

using System.Diagnostics;
using GBEmmy.Emulation.Cartridges;
using GBEmmy.Emulation.Memory;
using GBEmmy.Emulation.Processor.Registers;

namespace GBEmmy.Emulation.Processor
{
    public partial class Z80
    {
        private readonly IE _ie;
        private readonly Register _if;
        private double _cycles;

        public Z80(Cartridge cartridge)
        {
            LoadRegisters();

            Memory = cartridge.GetController(this);

            _ie = Memory.Registers.Get<IE>();
            _if = Memory.Registers.Get(RegisterAddress.IF);
        }

        public ushort IFF { get; set; }

        public byte InterruptQueue { get; set; }

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
            _cycles += (4194304.0*1*duration);

            while (_cycles > 0.0)
            {
                ushort debug = PC;
                byte instrid = Memory[PC++]; //read instrid

                if ((IFF & 0x100) != 0)
                {
                    IFF &= 0xFF;
                    PC--;
                }

                Opcode instr = (instrid == 0xCB
                    ? OpcodeTable.Cb[Memory[PC++]]
                    : OpcodeTable.Base[instrid]);

                _cycles -= instr.Call(this); //run

                Debug.WriteLine("Z80: @${0:X2}: instr ${1:X2}({2} {3},{4} / {5})", debug, instrid, instr.Operator,
                    instr.Operand1, instr.Operand2, instr.Embedded);

                _if.Value = InterruptQueue;
                if ((IFF & 0x20) != 0)
                {
                    IFF &= 0xDF;
                    IFF |= 0x01;
                }
                else if (((IFF & 0x01) != 0) && (InterruptQueue != 0) && (_ie.Value != 0))
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (((InterruptQueue & (1 << j)) != 0) && ((_ie.Value & (1 << j)) != 0))
                        {
                            if ((IFF & 0x80) != 0)
                            {
                                PC++;
                                IFF &= 0x7F;
                            }
                            IFF &= 0x7E;
                            InterruptQueue &= (byte) (~(1 << j));

                            _if.Value &= (byte) (~(1 << j));

                            Memory[--SP] = (byte) (PC >> 8);
                            Memory[--SP] = (byte) PC;

                            PC = (ushort) (0x0040 + (j << 3));
                            break;
                        }
                    }
                }
            }
        }
    }
}