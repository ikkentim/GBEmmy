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

namespace GBEmmy.Registers
{
    /// <summary>
    ///     LCDC Y-Coordinate (R).
    ///     The LY indicates the vertical line to which
    ///     the present data is transferred to the LCD
    ///     Driver. The LY can take on any value
    ///     between 0 through 153. The values between
    ///     144 and 153 indicate the V-Blank period.
    ///     Writing will reset the counter.
    /// </summary>
    public class LYRegister : IRegister
    {
        private byte _line;

        public byte Line
        {
            get { return _line; }
            set { _line = (byte) (value%154); }
        }

        public byte Value
        {
            get { return Line; }
            set { Line = 0; }
        }
    }
}