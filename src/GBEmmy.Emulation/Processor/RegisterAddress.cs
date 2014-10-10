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

namespace GBEmmy.Emulation.Processor
{
    public static class RegisterAddress
    {
        public const ushort P1 = 0xFF00;
        public const ushort SB = 0xFF01;
        public const ushort SC = 0xFF02;
        public const ushort DIV = 0xFF04;
        public const ushort TIMA = 0xFF05;
        public const ushort TMA = 0xFF06;
        public const ushort TAC = 0xFF07;
        public const ushort IF = 0xFF0F;
        public const ushort NR10 = 0xFF10;
        public const ushort NR11 = 0xFF11;
        public const ushort NR12 = 0xFF12;
        public const ushort NR13 = 0xFF13;
        public const ushort NR14 = 0xFF14;
        public const ushort NR21 = 0xFF16;
        public const ushort NR22 = 0xFF17;
        public const ushort NR23 = 0xFF18;
        public const ushort NR24 = 0xFF19;
        public const ushort NR30 = 0xFF1A;
        public const ushort NR31 = 0xFF1B;
        public const ushort NR32 = 0xFF1C;
        public const ushort NR33 = 0xFF1D;
        public const ushort NR34 = 0xFF1E;
        public const ushort NR41 = 0xFF20;
        public const ushort NR42 = 0xFF21;
        public const ushort NR43 = 0xFF22;
        public const ushort NR44 = 0xFF23;
        public const ushort NR50 = 0xFF24;
        public const ushort NR51 = 0xFF25;
        public const ushort NR52 = 0xFF26;
        //FF30-FF3F WAVE PTTRN
        public const ushort LCDC = 0xFF40;
        public const ushort STAT = 0xFF41;
        public const ushort SCY = 0xFF42;
        public const ushort SCX = 0xFF43;
        public const ushort LY = 0xFF44;
        public const ushort LYC = 0xFF45;
        public const ushort DMA = 0xFF46;
        public const ushort BGP = 0xFF47;
        public const ushort OBP0 = 0xFF48;
        public const ushort OBP1 = 0xFF49;
        public const ushort WY = 0xFF4A;
        public const ushort WX = 0xFF4B;
        public const ushort IE = 0xFFFF;
    }
}