using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBEmmy.Emulation.Processor.Registers
{
    public class TAC : Register
    {
        private readonly double[] _timerRates =
        {
            4096.0,
            262144.0,
            65536.0,
            16384.0,
        };

        public double TimerRate { get; set; }

        public bool TimerEnabled { get; set; }


        public TAC()
        {
            TimerRate = _timerRates.First();
        }

        public override byte Value 
        { 
            get { return base.Value; }
            set
            {
                TimerEnabled = ((value >> 2) & 0x01) != 0;
                TimerRate = _timerRates[(value & 0x03)];
            }
        }
    }
}
