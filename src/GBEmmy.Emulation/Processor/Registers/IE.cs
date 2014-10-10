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
    public class IE : Register
    {
        public bool JoypadEnabled
        {
            get { return (Value & Interrupts.Joypad) != 0; }
        }

        public bool LCDCEnabled
        {
            get { return (Value & Interrupts.LCDC) != 0; }
        }

        public bool SerialEnabled
        {
            get { return (Value & Interrupts.Serial) != 0; }
        }

        public bool TimerEnabled
        {
            get { return (Value & Interrupts.Timer) != 0; }
        }

        public bool VBlankEnabled
        {
            get { return (Value & Interrupts.VBlank) != 0; }
        }
    }
}