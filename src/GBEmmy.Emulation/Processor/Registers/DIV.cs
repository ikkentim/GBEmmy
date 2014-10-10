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
    public class DIV : Register
    {
        private const double IncrementTime = 1.0/16384;
        private double _timeSinceIncrement;

        public override byte Value
        {
            get { return base.Value; }
            set { base.Value = 0; }
        }

        public void Update(double timePassed)
        {
            _timeSinceIncrement += timePassed;

            while (_timeSinceIncrement >= IncrementTime)
            {
                base.Value++;
                _timeSinceIncrement -= IncrementTime;
            }
        }
    }
}