using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBEmmy.Emulation.Processor.Registers
{
    public class BGP :Register
    {
        public uint[] ColorArray
        {
            get
            {
                return ColorArrayProperty;
            }
        }

        private readonly uint[] ColorArrayProperty = new uint[4];

        public int Entries
        {
            get
            {
                return ColorArray.Length;
            }
        }

        private readonly uint[] GreyscaleTable = new uint[] { 0x00FFFFFF, 0x00AAAAAA, 0x00666666, 0x00000000 };

        public BGP()
        {
            Value = 0xFC;
        }

        public override byte Value
        {
            set
            {
                base.Value = value;
                for (int i = 0; i < ColorArray.Length; i++)
                {
                    ColorArray[i] = GreyscaleTable[((Value >> (i << 1)) & 0x03)];
                }
            }
        }
    }
}
