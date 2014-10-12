using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBEmmy.Emulation.VideoProcessor
{
    public enum FrameState : byte
    {
        ScanlineOAM = 2,
        ScanlineVRAM = 3,
        HBlank = 0,
        VBlank = 1
    }
}
