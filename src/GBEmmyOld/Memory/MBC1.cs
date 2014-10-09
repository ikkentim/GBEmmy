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

using GBEmmy.Cartridges;

namespace GBEmmy.Memory
{
    internal class MBC1 : MBC
    {
        private MBC1Mode Mode = MBC1Mode.SixteenEight;

        public MBC1(Cartridge cartridge) : base(cartridge)
        {
            ROMIndex = 1;
        }

        public override void WriteByte(ushort address, byte value)
        {
            switch (address & 0xE000)
            {
                case 0x0000:
                    RAMEnabled = ((value & 0x0A) == 0x0A);
                    break;
                case 0x2000:
                    ROMIndex &= 0x60;
                    ROMIndex |= (byte) (value & 0x1F);
                    ROMIndex %= (byte) ROM.Length;
                    ROMIndex = (byte) ((ROMIndex != 0) ? ROMIndex : 1);
                    break;
                case 0x4000:
                    if (Mode == MBC1Mode.SixteenEight)
                    {
                        ROMIndex &= 0x1F;
                        ROMIndex |= (byte) ((value & 0x03) << 5);
                        ROMIndex %= (byte) ROM.Length;
                        ROMIndex = (byte) ((ROMIndex != 0) ? ROMIndex : 1);
                    }
                    else
                    {
                        RAMIndex = (byte) (value & 0x03);
                    }
                    break;
                case 0x6000:
                    Mode = (MBC1Mode) (value & 0x01);
                    break;
                default:
                    base.WriteByte(address, value);
                    break;
            }
        }

        private enum MBC1Mode
        {
            SixteenEight = 0,
            FourThirtyTwo = 1,
        }
    }
}