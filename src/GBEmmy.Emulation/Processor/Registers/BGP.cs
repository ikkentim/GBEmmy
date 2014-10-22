using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBEmmy.Emulation.Processor.Registers
{
    public class BGP :Register
    {
        public uint[] Colors { get; private set; }

        private readonly uint[] _greyscaleTable = { 0x00FFFFFF, 0x00AAAAAA, 0x00666666, 0x00000000 };

        public BGP()
        {
            Colors = new uint[4];
            Value = 0xFC;
        }

        public override byte Value
        {
            set
            {
                base.Value = value;

                for (int i = 0; i < Colors.Length; i++)
                    Colors[i] = _greyscaleTable[((Value >> (i << 1)) & 0x03)];
                
            }
        }
    }
}
