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
        private readonly MBC _memory;

        public DMA(MBC memory)
        {
            _memory = memory;
        }

        public override byte Value
        {
            set
            {
                base.Value = value;
                var address = (value << 8);
                for (int i = 0; i < 0xA0; i++, address++)
                {
                    _memory[0xFE00 | i] = _memory[address];
                }

            }
        }
    }
}
