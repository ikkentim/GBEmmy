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
using GBEmmy.Emulation.Memory;
using GBEmmy.Emulation.Processor;

namespace GBEmmy.Emulation.Cartridges
{
    public class Cartridge
    {
        public Cartridge(CartridgeStream stream)
        {
            if (!stream.IsValid) throw new Exception("Invalid ROM");

            IsCGB = stream.IsCGB;
            Type = stream.Type;
            RAM = stream.RAM;
            ROM = stream.ROM;
        }

        public Bank[] RAM { get; private set; }
        public Bank[] ROM { get; private set; }

        public CartridgeType Type { get; private set; }

        public bool IsCGB { get; private set; }

        public MBC GetController(Z80 cpu)
        {
            switch (Type)
            {
                case CartridgeType.ROM:
                case CartridgeType.ROM_RAM:
                case CartridgeType.ROM_RAM_BATT:
                    return new MBC0(cpu, this);
                case CartridgeType.MBC1:
                case CartridgeType.MBC1_RAM:
                case CartridgeType.MBC1_RAM_BATT:
                    return new MBC1(cpu, this);
                    //                case CartridgeType.MBC2:
                    //                case CartridgeType.MBC2_BATT:
                    //                    return new MBC2(this);
                    //                case CartridgeType.MBC3:
                    //                case CartridgeType.MBC3_RAM:
                    //                case CartridgeType.MBC3_RAM_BATT:
                    //                case CartridgeType.MBC3_TIMER_BATT:
                    //                case CartridgeType.MBC3_TIMER_RAM_BATT:
                    //                    return new MBC3(this);
                    //                case CartridgeType.MBC5:
                    //                case CartridgeType.MBC5_RAM:
                    //                case CartridgeType.MBC5_RAM_BATT:
                    //                case CartridgeType.MBC5_RUMBLE:
                    //                case CartridgeType.MBC5_RUMBLE_SRAM:
                    //                case CartridgeType.MBC5_RUMBLE_SRAM_BATT:
                    //                    return new MBC5(this);

                default:
                    throw new Exception(string.Format("Unsupported memory bank controller {0}", Type));
            }
        }
    }
}