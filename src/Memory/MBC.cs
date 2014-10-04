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

using System;
using System.Linq;

namespace GBEmmy.Memory
{
    public abstract class MBC
    {
        protected MBC(Cartridge cartridge)
        {
            RAM = cartridge.RAM;
            ROM = cartridge.ROM;
            WorkRAM = Enumerable.Repeat(new Bank(0x1000), 8).ToArray();
        }

        public Bank[] RAM { get; private set; }
        public Bank[] ROM { get; private set; }

        public bool RAMEnabled { get; protected set; }

        public Bank[] WorkRAM { get; private set; }

        public byte RAMIndex { get; set; }
        public byte ROMIndex { get; set; }

        public byte this[ushort addr]
        {
            get { return ReadByte(addr); }
            set { WriteByte(addr, value); }
        }

        public byte this[byte high, byte low]
        {
            get { return ReadByte((ushort) ((high << 8) | low)); }
            set { WriteByte((ushort) ((high << 8) | low), value); }
        }

        public byte this[int addr]
        {
            get { return ReadByte((ushort) addr); }
            set { WriteByte((ushort) addr, value); }
        }

        public virtual void WriteByte(ushort address, byte value)
        {
            switch (address & 0xF000)
            {
                case 0x8000:
                case 0x9000:
                    throw new NotImplementedException("VRAM not implemented");
                    break;
                case 0xA000:
                case 0xB000:
                    if ((RAM != null) && (RAMEnabled))
                    {
                        RAM[RAMIndex][address] = value;
                    }
                    break;
                case 0xC000:
                case 0xE000:
                    WorkRAM[0][address] = value;
                    break;
                case 0xD000:
                case 0xF000:
                    if (address >= 0xFF00)
                    {
                        throw new NotImplementedException();
                    }
                    if (address >= 0xFE00)
                    {
                        if (address < 0xFEA0)
                        {
                            throw new NotImplementedException("Sprites not implemented");
                        }
                    }
                        //(BNK != null)
                        //{
                        //   thr noimp
                        //}
                    else
                    {
                        WorkRAM[1][address] = value;
                    }
                    break;
            }
        }

        public virtual byte ReadByte(ushort address)
        {
            switch (address & 0xF000)
            {
                case 0x0000:
                case 0x1000:
                case 0x2000:
                case 0x3000:
                    return ROM[0][address];
                case 0x4000:
                case 0x5000:
                case 0x6000:
                case 0x7000:
                    return ROM[ROMIndex][address & 0x0FFF];
                case 0x8000:
                case 0x9000:
                    throw new NotImplementedException("VRAM not implemented");
                case 0xA000:
                case 0xB000:
                    if ((RAM != null) && (RAMEnabled))
                    {
                        return RAM[RAMIndex][address];
                    }
                    break;
                case 0xC000:
                case 0xE000:
                    return WorkRAM[0][address];
                case 0xD000:
                case 0xF000:
                    if (address >= 0xFF00)
                    {
                        throw new NotImplementedException();
                    }
                    if (address >= 0xFE00)
                    {
                        if (address < 0xFEA0)
                        {
                            throw new NotImplementedException("Sprites not implemented");
                        }
                    }
                        //(BNK != null)
                        //{
                        //   thr noimp
                        //}
                    else
                    {
                        return WorkRAM[1][address];
                    }
                    break;
            }

            return 0;
        }
    }
}