using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBEmmy.Emulation.Processor.Registers
{
    public class LY : Register
    {
        private byte _line;

        public override byte Value
        {
            get { return Line; }
            set { Line = 0; }
        }

        public byte Line
        {
            get { return _line; }
            set { _line = (byte) (value%153); }
        }
    }
}
