namespace GBEmmy.Emulation.Processor
{
    public static class Interrupts
    {
        public const byte VBlank = 0x01;
        public const byte LCDC = 0x02;
        public const byte Timer = 0x04;
        public const byte Serial = 0x08;
        public const byte Joypad = 0x10;
    }
}
