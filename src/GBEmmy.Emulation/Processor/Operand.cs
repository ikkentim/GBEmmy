// GBEmmy
// Copyright (C) 2014 Tim Potze
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <http://unlicense.org>

using System.ComponentModel;

namespace GBEmmy.Emulation.Processor
{
    public enum Operand
    {
        None,
        [Description("d8")]
        Byte, //passed in instr
        [Description("d16")]
        Word, //passed in instr
        [Description("A")]
        A,
        [Description("B")]
        B,
        [Description("C")]
        C,
        [Description("D")]
        D,
        [Description("E")]
        E,
        [Description("H")]
        H,
        [Description("L")]
        L,
        [Description("AF")]
        AF,
        [Description("BC")]
        BC,
        [Description("DE")]
        DE,
        [Description("HL")]
        HL,
        [Description("SP")]
        SP,
        [Description("(a16)")]
        Memory, //memory at addr passed in instr
        [Description("(BC)")]
        MemoryBC,
        [Description("(DE)")]
        MemoryDE,
        [Description("(C)")]
        MemoryC,
        [Description("(HL)")]
        MemoryHL,
        [Description("Z")]
        Zero,
        [Description("NZ")]
        NotZero,
        [Description("NC")]
        NotCarry,
        [Description("C")]
        Carry,
        [Description("r8")]
        SignedByte,
        [Description("$FF00+a8")]
        MemoryByte, //memory at addr $FF00 + what's passed in instr
        [Description("em")]
        Embedded //embedded in opcode/operator
    }
}