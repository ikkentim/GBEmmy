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

using System.IO;
using System.Linq;
using GBEmmy.Memory;

namespace GBEmmy.Cartridges
{
    public class CartridgeStream : FileStream
    {
        public CartridgeStream(string path) : base(path, FileMode.Open)
        {
        }

        public bool IsValid
        {
            get
            {
                long pos = Position;

                Position = 0x104;
                uint read = ReadUInt();
                if ((Length < 0x150) || (read != 0x6666EDCE))
                {
                    Position = pos;
                    return false;
                }

                Position = pos;

                return true;
            }
        }

        public bool IsGameBoyColor
        {
            get
            {
                long pos = Position;
                Position = 0x0143;

                bool res = ((ReadByte() & 0x80) != 0);

                Position = pos;
                return res;
            }
        }

        public CartridgeType Type
        {
            get
            {
                long pos = Position;
                Position = 0x0147;

                var res = (CartridgeType) ReadByte();

                Position = pos;
                return res;
            }
        }

        public Bank[] ROM
        {
            get
            {
                long pos = Position;
                var rom = new Bank[Length >> 14];
                for (int i = 0; i < rom.Length; i++)
                    rom[i] = new Bank(ReadBytes(0x4000), true);

                Position = pos;
                return rom;
            }
        }

        public Bank[] RAM
        {
            get
            {
                switch (ROM[0][0x0149])
                {
                    case 1:
                        return new[] {new Bank(0x800)};
                    case 2:
                        return Enumerable.Repeat(new Bank(0x2000, 0xFF), 1).ToArray();
                    case 3:
                        return Enumerable.Repeat(new Bank(0x2000, 0xFF), 4).ToArray();
                    case 5:
                        return Enumerable.Repeat(new Bank(0x2000, 0xFF), 8).ToArray();
                    default:
                        return Enumerable.Repeat(new Bank(0x2000, 0xFF), 16).ToArray();
                }
            }
        }

        public new byte ReadByte()
        {
            return (byte) base.ReadByte();
        }

        public byte[] ReadBytes(int length)
        {
            var result = new byte[length];
            for (int i = 0; i < length; i++)
                result[i] = ReadByte();

            return result;
        }

        public ushort ReadUShort()
        {
            return (ushort) (ReadByte() | (ReadByte() << 8));
        }

        public uint ReadUInt()
        {
            return (uint) (ReadUShort() | (ReadUShort() << 16));
        }

        public Cartridge ToCartridge()
        {
            return new Cartridge(this);
        }
    }
}