using System;

namespace GBEmmy
{
    [Flags]
    public enum Flags : byte
    {
        Zero = 0x80, //b7
        Operation = 0x40, //b6
        HalfCarry = 0x20, //b5
        Carry = 0x10, //b4
    }
}
