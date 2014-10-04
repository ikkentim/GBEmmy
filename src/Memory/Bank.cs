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

using System;

namespace GBEmmy.Memory
{
    public sealed class Bank
    {
        private readonly byte[] _data;
        private readonly int _mask;

        public Bank()
            : this(0x1000) // 4k
        {
        }

        public Bank(bool readOnly)
            : this(0x1000, readOnly) // 4k
        {
        }

        public Bank(int length)
            : this(length, 0, false)
        {
        }

        public Bank(int length, bool readOnly)
            : this(length, 0, readOnly)
        {
        }

        public Bank(byte[] data) : this(data, false)
        {
        }

        public Bank(byte[] data, bool readOnly)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (!IsValidLength(data.Length)) throw new ArgumentException("Invalid length", "length");
            ReadOnly = readOnly;
            _data = data;
            _mask = Length - 1;
        }

        public Bank(int length, byte defaultValue) : this(length, defaultValue, false)
        {
        }

        public Bank(int length, byte defaultValue, bool readOnly)
        {
            if (!IsValidLength(length)) throw new ArgumentException("Invalid length", "length");

            ReadOnly = readOnly;
            _data = new byte[length];
            _mask = Length - 1;

            if (defaultValue == 0) return;

            for (int i = 0; i < length; i++)
                _data[i] = defaultValue;
        }

        public int Length
        {
            get { return _data.Length; }
        }

        public bool ReadOnly { get; private set; }

        public byte this[int offset]
        {
            get { return _data[offset & _mask]; }
            set
            {
                if (ReadOnly) throw new Exception("Writing to ROM is not allowed");
                _data[offset & _mask] = value;
            }
        }

        private bool IsValidLength(int length)
        {
            int i;
            for (i = 1; i < length; i *= 2) ;
            return i == length;
        }
    }
}