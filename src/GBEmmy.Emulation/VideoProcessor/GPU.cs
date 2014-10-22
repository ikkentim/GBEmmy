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
using System.Diagnostics;
using System.Drawing;
using GBEmmy.Emulation.Memory;
using GBEmmy.Emulation.Processor;
using GBEmmy.Emulation.Processor.Registers;

namespace GBEmmy.Emulation.VideoProcessor
{
    public class GPU
    {
        private readonly MBC _memory;
        private double _timeBuffer;
        private readonly STAT _stat;
        private readonly LY _ly;
        private readonly LCDC _lcdc;
        private readonly Register _scx;
        private readonly Register _scy;

        public const byte Height = 144;
        public const byte Width = 160;
        public BgTileMap[] Maps { get; private set; }
        private static readonly double[] FrameStateDuration =
        {
            0.00004802, //HBlank
            0.000114, //VBlank
            0.00001931, //ScanlineOAM
            0.00004137 //ScanlineVRAM
        };

        public GPU(MBC memory)
        {
            _memory = memory;

            _stat = memory.Registers.Get<STAT>();
            _ly = memory.Registers.Get<LY>();
            _lcdc = memory.Registers.Get<LCDC>();
            _scx = memory.Registers.Get(RegisterAddress.SCX);
            _scy = memory.Registers.Get(RegisterAddress.SCY);

            Maps = new[] {new BgTileMap(memory, 0x9800), new BgTileMap(memory, 0x9C00)};
            ScreenBuffer = new uint[Width*Height];
        }

        public uint[] ScreenBuffer { get; private set; }

        public double Run(double duration)
        {
            _timeBuffer += duration;

            if (!(_timeBuffer >= FrameStateDuration[(int)_stat.State])) return duration;

            duration = FrameStateDuration[(int)_stat.State];

            //Debug.WriteLine("Render {0} {1}", _ly.Line, _stat.State);

            switch (_stat.State)
            {
                case FrameState.HBlank:
                    _stat.State = FrameState.ScanlineOAM;
                    break;
                case FrameState.ScanlineOAM:
                    _stat.State = FrameState.ScanlineVRAM;
                    break;
                case FrameState.ScanlineVRAM:

                    if (_ly.Line >= Height)
                    {
                        _stat.State = FrameState.VBlank;
                    }
                    else
                    {
                        //Fill line from VRAM
                        var map = Maps[_lcdc.BackgroundTileMap];
                        //render bg
                        var y = _ly.Line;
                        for (var x = 0; x < Width; x++)
                        {
                            var mapx = _scx.Value + x;
                            var mapy = _scy.Value + y;

                            var tilex = mapx/Tile.Width;
                            var tiley = mapy/Tile.Height;

                            var tile = map[(byte)(tilex % 32), (byte)(tiley % 32)];
                            var dot = tile[(byte) (mapx%Tile.Width), (byte) (mapy%Tile.Height)];

                            ScreenBuffer[y*Width + x] = dot;
                        }
                        //


                        _ly.Line++;
                        _stat.State = FrameState.HBlank;
                    }
                    break;
                case FrameState.VBlank:
                    if (_ly.Line++ == 0) _stat.State = FrameState.HBlank;
                    break;
            }
            return duration;
        }
    }

    public class BgTileMap
    {
        private readonly MBC _memory;
        private readonly ushort _address;
        private readonly LCDC _lcdc;
        private readonly TileDataTable[] _tables;
        public BgTileMap(MBC memory, ushort address)
        {
            _memory = memory;
            _address = address;
            _lcdc = memory.Registers.Get<LCDC>();
            _tables = new[] {new TileDataTable(memory, 0x8000, false), new TileDataTable(memory, 0x8800, true)};

        }

        public TileDataTable Table
        {
            get { return _tables[_lcdc.TileDataTable]; }
        }

        public Tile this[byte x, byte y]
        {
            get
            {
                if (x >= 32) throw new ArgumentOutOfRangeException("x");
                if (y >= 32) throw new ArgumentOutOfRangeException("y");
                var addr = (ushort) (_address + x + 32*y);
                var t = _memory[addr];
                return Table[t];
            } 
        }
        public Tile this[byte table, byte x, byte y]
        {
            get
            {
                if (x >= 32) throw new ArgumentOutOfRangeException("x");
                if (y >= 32) throw new ArgumentOutOfRangeException("y");
                var t = _memory[_address + x + 32*y];

                return _tables[table & 0x01][t];
            }
        }
    }

    public class TileDataTable
    {
        private readonly Tile[] _tiles;

        public TileDataTable(MBC memory, ushort address, bool signed)
        {
            _tiles = new Tile[256];
            if (signed)
            {
                for (var i = -128; i <= 127; i++)
                {
                    _tiles[i + 128] = new Tile(memory, (ushort)(address + (128 * 16) + (i * 16)),i);
                }
            }
            else
            {
                for (var i = 0; i <= 255; i++)
                {
                    ushort addr = (ushort) (address + (i*16));
                    _tiles[i] = new Tile(memory, addr,i);
                }
            }
        }

        public Tile this[byte i]
        {
            get
            {
                return _tiles[i];
            }
        }
    }

    public class Tile
    {
        public int ID { get; set; }
        public const byte Width = 8;
        public const byte Height = 8;

        private readonly MBC _memory;
        private readonly BGP _bgp;
        private readonly ushort _address;

        public Tile(MBC memory, ushort address, int id)
        {
            ID = id;
            _memory = memory;
            _address = address;
            _bgp = memory.Registers.Get<BGP>();
        }

        public uint this[byte x, byte y]
        {
            get
            {
                if (x >= 8) throw new ArgumentOutOfRangeException("x");
                if (y >= 8) throw new ArgumentOutOfRangeException("y");

                var l = _memory[_address + y*2];
                var r = _memory[_address + y*2 + 1];


                var dotmask = 1 << (7 - x);

                var col = new uint[] { 0xFFFFFFFF, 0xFFFF0000, 0xFF00FF00, 0xFF0000FF };
                return col[((l & dotmask) > 0 ? 1 : 0) | ((r & dotmask) > 0 ? 2 : 0)];
                // _bgp.Colors[((l & dotmask) > 0 ? 1 : 0) | ((r & dotmask) > 0 ? 2 : 0)];
            }
        }

        public Bitmap ToBitmap()
        {
            var bmp = new Bitmap(Width, Height);
            for(byte x= 0;x<Width;x++)
                for (byte y = 0; y < Height; y++)
                {
                    var c = this[x, y];
                    bmp.SetPixel(x, y,
                        Color.FromArgb((byte) ((c >> 4) & 0xFF), (byte) ((c >> 2) & 0xFF), (byte) ((c >> 0) & 0xFF)));

                }

            return bmp;
        }
    }
}