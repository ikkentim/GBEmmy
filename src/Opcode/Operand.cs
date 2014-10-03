namespace GBEmmy.Opcode
{
    public enum Operand
    {
        None,
        Byte, //passed in instr
        Word, //passed in instr
        A,
        B,
        C,
        D,
        E,
        H,
        L,
        AF,
        BC,
        DE,
        HL,
        SP,
        Memory, //memory at addr passed in instr
        MemoryBC,
        MemoryDE,
        MemoryHL,
        Zero,
        NotZero,
        NotCarry,
        Carry,
        SignedByte,
        MemoryByte //memory at addr $FF00 + what's passed in instr
    }
}
