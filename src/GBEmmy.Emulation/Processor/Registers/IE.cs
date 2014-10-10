namespace GBEmmy.Emulation.Processor.Registers
{
    public class IE : Register
    {
        public bool JoypadEnabled
        {
            get { return (Value & Interrupts.Joypad) != 0; }
        }

        public bool LCDCEnabled
        {
            get { return (Value & Interrupts.LCDC) != 0; }
        }

        public bool SerialEnabled
        {
            get { return (Value & Interrupts.Serial) != 0; }
        }

        public bool TimerEnabled
        {
            get { return (Value & Interrupts.Timer) != 0; }
        }

        public bool VBlankEnabled
        {
            get { return (Value & Interrupts.VBlank) != 0; }
        }
    }
}
