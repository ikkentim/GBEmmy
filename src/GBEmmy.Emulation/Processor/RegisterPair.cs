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

using System.Runtime.InteropServices;

namespace GBEmmy.Emulation.Processor
{
    [StructLayout(LayoutKind.Explicit)]
    internal struct RegisterPair
    {
        [FieldOffset(0)] private byte _lower;

        [FieldOffset(1)] private byte _upper;

        [FieldOffset(0)] private ushort _value;

        public ushort Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public byte Lower
        {
            get { return _lower; }
            set { _lower = value; }
        }

        public byte Upper
        {
            get { return _upper; }
            set { _upper = value; }
        }

        public RegisterPair(ushort value)
        {
            _lower = 0;
            _upper = 0;
            _value = value;
        }

        public static implicit operator ushort(RegisterPair value)
        {
            return value.Value;
        }

        public static implicit operator RegisterPair(ushort value)
        {
            return new RegisterPair(value);
        }
    }
}