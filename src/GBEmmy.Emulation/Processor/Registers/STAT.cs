using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GBEmmy.Emulation.VideoProcessor;

namespace GBEmmy.Emulation.Processor.Registers
{
    public class STAT : Register
    {
        private readonly Z80 _cpu;
        private readonly LCDC _lcdc;
        private readonly IE _ie;
        private readonly LY _ly;
        private readonly Register _lyc;
        public STAT(Z80 cpu, RegisterCollection registers)
        {
            _cpu = cpu;
            _lcdc = registers.Get<LCDC>();
            _ie = registers.Get<IE>();
            _ly = registers.Get<LY>();
            _lyc = registers.Get(RegisterAddress.LYC);
        }

        public bool Coincidence
        {
            get
            {
                return ((Value & 0x40) != 0);
            }
        }

        public bool CoincidenceFlag
        {
            get
            {
                return ((Value & 0x04) != 0);
            }
            set
            {
                Value = (byte) (value ? (Value | 0x04) : (Value & ~0x04));
            }
        }

        public int InterruptSelection
        {
            get
            {
                return ((Value >> 3) & 0x07);
            }
        }

        public byte ModeFlag
        {
            get
            {
                return (byte)(Value & 0x03);
            }
            set
            {
                Value &= 0xFC;
                Value |= (byte) (value & 0x03);
                if (_lcdc.DisplayEnabled)
                {
                    if (ModeFlag == 1)
                    {
                        _cpu.InterruptQueue |= Interrupts.VBlank;
                    }
                    else if (((InterruptSelection & (1 << ModeFlag)) != 0) && (CoincidenceFlag == false))
                    {
                        _cpu.InterruptQueue |= Interrupts.LCDC;
                    }
                    if (ModeFlag == 3)
                    {
                        Debug.WriteLine("CSLI");
                        CheckScanLineInterrupt();
                    }
                }
            }
        }

        public FrameState State
        {
            // bit 0-1
            get { return (FrameState)ModeFlag; }
            set { ModeFlag = (byte) value; }
        }

        private void CheckScanLineInterrupt()
        {
            CoincidenceFlag = _ly == _lyc;
            if ((_lcdc.DisplayEnabled) && (CoincidenceFlag))
            {
                if ((Coincidence) && ((_ie.Value & Interrupts.LCDC) != 0))
                {
                    _cpu.InterruptQueue |= Interrupts.LCDC;
                }
            }
            return;
        }
    }
}

