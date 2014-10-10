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

using System.Linq;

namespace GBEmmy.Emulation.Processor.Registers
{
    public class TAC : Register
    {
        private readonly double[] _timerRates =
        {
            4096.0,
            262144.0,
            65536.0,
            16384.0
        };

        public TAC()
        {
            TimerRate = _timerRates.First();
        }

        public double TimerRate { get; set; }

        public bool TimerEnabled { get; set; }


        public override byte Value
        {
            get { return base.Value; }
            set
            {
                TimerEnabled = ((value >> 2) & 0x01) != 0;
                TimerRate = _timerRates[(value & 0x03)];
            }
        }
    }
}