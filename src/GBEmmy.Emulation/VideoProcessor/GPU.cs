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

using System.Diagnostics;
using GBEmmy.Emulation.Memory;
using GBEmmy.Emulation.Processor.Registers;

namespace GBEmmy.Emulation.VideoProcessor
{
    public class GPU
    {
        private readonly MBC _memory;
        private double _timeBuffer;
        private readonly STAT _stat;
        private readonly LY _ly;
        public const byte Height = 144;
        public const byte Width = 160;

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

        }

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
}