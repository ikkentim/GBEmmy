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

namespace GBEmmy.Emulation.Memory
{
    public class Bank
    {
        private readonly byte[] _data;
        private readonly ushort _mask;

        public Bank(ushort length)
        {
            if (!IsValidLength(length)) throw new Exception("Invalid bank length");
            _data = new byte[length];
            _mask = (ushort) (_data.Length - 1);
        }

        public Bank(ushort length, byte defaultvalue)
        {
            if (!IsValidLength(length)) throw new Exception("Invalid bank length");
            _data = new byte[length];
            _mask = (ushort) (_data.Length - 1);

            for (ushort i = 0; i < length; i++)
                this[i] = defaultvalue;
        }

        public Bank(byte[] data)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (!IsValidLength(data.Length)) throw new Exception("Invalid bank length");

            _data = data;
            _mask = (ushort) (_data.Length - 1);
        }

        public Bank(byte[] data, bool readOnly) : this(data)
        {
            ReadOnly = readOnly;
        }

        public bool ReadOnly { get; private set; }

        public byte this[ushort address]
        {
            get { return _data[address & _mask]; }
            set
            {
                if (ReadOnly) throw new Exception("Cannot write to ROM");
                _data[address & _mask] = value;
            }
        }

        private bool IsValidLength(int length)
        {
            int m = 1;
            for (; m < length; m *= 2) ;
            return length == m;
        }
    }
}