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
        private readonly IE _ie;
        private readonly Register _if;
        private double _cycles;

        public Z80(Cartridge cartridge)
        {
            LoadRegisters();

            Memory = cartridge.GetController(this);

            _ie = Memory.Registers.Get<IE>();
            _if = Memory.Registers.Get(RegisterAddress.IF);

            IME = true;

            Memory.BootromEnabled = false;

            PC = 0x100;
            BC = 0x0013;
            DE = 0x00D8;
            HL = 0x014D;
            SP = 0xFFFE;
            AF = 0x01B0;
        }

        public bool IME { get; set; }

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

                if (Memory.BootromEnabled && PC == 0x100)
                {
                    Memory.BootromEnabled = false;
                    Debug.WriteLine("QUIT bootrom");
                }

                byte instrid = Memory[PC++]; //read instrid

                Opcode instr = (instrid == 0xCB
                    ? OpcodeTable.Cb[Memory[PC++]]
                    : OpcodeTable.Base[instrid]);
                
                Debug.WriteLine("Z80: @${0:X2}: instr ${1} \t({2}) \t[{3:X2}, {4:X2}][[AF: {5:X4},BC: {6:X4},DE: {7:X4},HL: {8:X4}, SP:{9:X4}]]", debug, instrid, instr, Memory[PC], Memory[PC+1], AF, BC, DE, HL, SP);

                _cycles -= instr.Call(this); //run

                _if.Value = InterruptQueue;

                if (IME && (InterruptQueue != 0) && (_ie.Value != 0))
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (((InterruptQueue & (1 << j)) != 0) && ((_ie.Value & (1 << j)) != 0))
                        {
                            InterruptQueue &= (byte) (~(1 << j));

                            _if.Value &= (byte) (~(1 << j));

                            Memory[--SP] = (byte) (PC >> 8);
                            Memory[--SP] = (byte) PC;

                            Debug.WriteLine("Jump to interrupt {0}", (1 << j));
                            PC = (ushort) (0x0040 + (j << 3));
                            break;
                        }
                    }
                }
            }
        }
    }
}