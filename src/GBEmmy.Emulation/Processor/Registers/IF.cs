using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBEmmy.Emulation.Processor.Registers
{
    public class IF : Register
    {
        private readonly Z80 _cpu;

        public IF(Z80 cpu)
        {
            _cpu = cpu;
            base.Value = 0x01;
        }

        public override byte Value
        {
            get { return (byte) (base.Value | 0xE0); }
            set
            {
                base.Value = value;
                _cpu.InterruptQueue = value;
            }
        }
    }
}
