using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBEmmy.Emulation.Processor.Registers
{
    public class DIV : Register
    {
        public override byte Value
        {
            get
            {
                return base.Value;
            }
            set
            {
                base.Value = 0;
            }
        }

        private double _timeSinceIncrement;

        private const double IncrementTime = 1.0/16384;
        public void Update(double timePassed)
        {
            _timeSinceIncrement += timePassed;

            while (_timeSinceIncrement >= IncrementTime)
            {
                base.Value++;
                _timeSinceIncrement -= IncrementTime;
            }
        }
    }
}
