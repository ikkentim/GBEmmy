using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GBEmmy.Emulation.Cartridges;
using GBEmmy.Emulation.Processor;

namespace GBEmmy.Emulation.Memory
{
    public class MBC0 : MBC
    {
        public MBC0(Z80 cpu, Cartridge cartridge) : base(cpu, cartridge)
        {
            RAMEnabled = true;
            ROMIndex = 0;
        }
    }
}
