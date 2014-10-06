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

using GBEmmy.Memory;
using GBEmmy.Registers;
using GBEmmy.VideoProcessor;

namespace GBEmmy
{
    public class GPU
    {
        private const byte Height = 144;
        private const byte Width = 160;

        private static readonly double[] FrameStateDuration =
        {
            0.00004802, //HBlank
            0.000114, //VBlank
            0.00001931, //ScanlineOAM
            0.00004137 //ScanlineVRAM
        };

        private readonly LCDCRegister _lcdc;
        private readonly LYRegister _ly;
        private readonly STATRegister _stat;
        private Map[] _maps;
        private byte _scrollX;
        private byte _scrollY;

        private double _timeBuffer;

        public GPU(MBC memory)
        {
            _ly = memory.Registers.Get<LYRegister>();
            _lcdc = memory.Registers.Get<LCDCRegister>();
            _stat = memory.Registers.Get<STATRegister>();
        }

        private void RenderBackgroundToScreenBuffer(byte line)
        {
            if (!_lcdc.DisplayEnabled)
            {
                //TODO: Clear line

                return;
            }

            if (_lcdc.BackgroundEnabled)
            {
                //Render background
                Map map = _maps[_lcdc.ActiveMap];

                for (int x = 0; x < Width; x += Tile.Width)
                {
                    int tileIdx = 0 //TODO
                }
                //...
            }
        }

        private void RenderSpritesToScreenBuffer(byte line)
        {
        }

        public double Run(double duration)
        {
            _timeBuffer += duration;

            if (!(_timeBuffer >= FrameStateDuration[(int) _stat.State])) return duration;

            duration = FrameStateDuration[(int) _stat.State];
            switch (_stat.State)
            {
                case FrameState.HBlank:
                    _stat.State = FrameState.ScanlineOAM;
                    break;
                case FrameState.ScanlineOAM:
                    _stat.State = FrameState.ScanlineVRAM;
                    break;
                case FrameState.ScanlineVRAM:

                    //Fill line from VRAM
                    RenderBackgroundToScreenBuffer(_ly.Line);
                    RenderSpritesToScreenBuffer(_ly.Line);

                    _stat.State = FrameState.ScanlineVRAM;
                    if (_ly.Line >= Height)
                    {
                        _stat.State = FrameState.VBlank;
                    }
                    else
                    {
                        _ly.Line++;
                        _stat.State = FrameState.HBlank;
                    }
                    break;
                case FrameState.VBlank:
                    if (_ly.Line++ == 0) _stat.State = FrameState.ScanlineOAM;
                    break;
            }
            return duration;
        }
    }

    internal class Map
    {
        public Map(ushort attributesAddress)
        {
        }
    }

    internal class Tile
    {
        public const byte Width = 8;
        public const byte Height = 8;
    }
}