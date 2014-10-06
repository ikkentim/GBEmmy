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

using System.Windows.Forms;
using GBEmmy.Memory;

namespace GBEmmy
{
    public class VideoProcessor
    {
        private const byte Height = 144;
        private const byte Width = 160;
        private MBC _memory;
        private byte _activeMap;
        private byte _scrollX;
        private byte _scrollY;

        private enum FrameState
        {
            ScanlineOAM = 2,
            ScanlineVRAM = 3,
            HBlank = 0,
            VBlank = 1
        }

        private FrameState _state;
        private byte _line;

        public VideoProcessor(MBC memory)
        {
            _memory = memory;
        }

        private static readonly double[] FrameStateDuration =
        {
            0.00004802, 
            0.000114, 
            0.00001931, 
            0.00004137
        };

        private double _timeBuffer;

        private void RenderToScreenBuffer()
        {
            
        }

        public double Run(double duration)
        {
            _timeBuffer += duration;

            if (!(_timeBuffer >= FrameStateDuration[(int) _state])) return duration;

            duration = FrameStateDuration[(int) _state];
            switch (_state)
            {
                case FrameState.HBlank:
                    _state = FrameState.ScanlineOAM;
                    break;
                case FrameState.ScanlineOAM:
                    _state = FrameState.ScanlineVRAM;
                    break;
                case FrameState.ScanlineVRAM:

                    //Fill line from VRAM

                    _state = FrameState.ScanlineVRAM;
                    if (_line >= Height)
                    {
                        _line = 0;
                        _state = FrameState.VBlank;
                    }
                    else
                    {
                        _line++;
                        _state = FrameState.HBlank;
                    }
                    break;
                case FrameState.VBlank:
                    _state = FrameState.ScanlineOAM;
                    break;

            }
            return duration;
        }
    }
}