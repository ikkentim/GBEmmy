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

using GBEmmy.VideoProcessor;

namespace GBEmmy.Registers
{
    /// <summary>
    ///     LCDC Status (R/W)
    /// </summary>
    public class STATRegister : Register
    {
        public FrameState State
        {
            // bit 0-1
            get { return (FrameState) (byte) (Value & 0x03); }
            set { Value = (byte) ((Value & ~0x03) | (byte) value); }
        }
    }
}