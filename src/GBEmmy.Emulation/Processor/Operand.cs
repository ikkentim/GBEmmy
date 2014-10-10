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
        MemoryC,
        MemoryHL,
        Zero,
        NotZero,
        NotCarry,
        Carry,
        SignedByte,
        MemoryByte, //memory at addr $FF00 + what's passed in instr
        Embedded //embedded in opcode
    }
}