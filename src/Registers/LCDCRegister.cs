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
    ///     LCD Control (R/W).
    /// </summary>
    public class LCDCRegister : Register
    {
        public bool DisplayEnabled
        {
            get { return (Value & 0x80) != 0; }
        }

        public bool BackgroundEnabled
        {
            get { return (Value & 0x01) != 0; }
        }

        public byte ActiveMap
        {
            get { return (byte) ((Value >> 3) & 0x01); }
        }
    }
}