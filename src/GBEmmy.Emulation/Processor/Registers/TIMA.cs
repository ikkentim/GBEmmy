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

namespace GBEmmy.Emulation.Processor.Registers
{
    internal class TIMA : Register
    {
        private readonly Z80 _cpu;
        private readonly TAC _tac;

        private double _timeSinceIncrement;

        public TIMA(TAC tac, Z80 cpu)
        {
            _tac = tac;
            _cpu = cpu;
        }

        public void Update(double timePassed)
        {
            if (!_tac.TimerEnabled) return;

            _timeSinceIncrement += timePassed;

            double rate = 1.0/_tac.TimerRate;


            while (_timeSinceIncrement >= rate)
            {
                if (Value++ == 0xFF)
                {
                    _cpu.InterruptQueue |= Interrupts.Timer;
                }
                _timeSinceIncrement -= rate;
            }
        }
    }
}