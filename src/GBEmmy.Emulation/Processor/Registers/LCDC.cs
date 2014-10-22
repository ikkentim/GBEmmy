using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBEmmy.Emulation.Processor.Registers
{
    public class LCDC : Register
    {
        public LCDC()
        {
            Value = 0x91;
        }

        public bool BackgroundEnabled
        {
            get { return ((Value >> 0) & 0x01) != 0; }
        }

        public bool SpritesEnabled
        {
            get { return ((Value >> 1) & 0x01) != 0; }
        }

        public byte SpriteSize
        {
            get { return (byte) ((Value >> 2) & 0x01); }
        }

        public byte BackgroundTileMap
        {
            get { return (byte) ((Value >> 3) & 0x01); }
        }

        public byte TileDataTable
        {
            get { return (byte) ((Value & 0x10) == 0 ? 1 : 0); }
        }

        public bool WindowEnabled
        {
            get { return ((Value >> 5) & 0x01) != 0; }
        }

        public byte WindowTileMap
        {
            get { return (byte) ((Value >> 6) & 0x01); }
        }

        public bool DisplayEnabled
        {
            get { return ((Value >> 7) & 0x01) != 0; }
        }
    }
}
