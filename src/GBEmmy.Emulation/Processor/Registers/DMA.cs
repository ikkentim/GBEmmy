using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GBEmmy.Emulation.Memory;

namespace GBEmmy.Emulation.Processor.Registers
{
    public class DMA : Register
    {
        private readonly Z80 _cpu;

        public DMA(Z80 cpu)
        {
            _cpu = cpu;
        }

        public override byte Value
        {
            set
            {
                base.Value = value;
                var address = (ushort)(value << 8);
                for (int i = 0; i < 0xA0; i++, address++)
                {
                    _cpu.Memory[0xFE00 | i] = _cpu.Memory[address];
                }

            }
        }
    }
}
