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

namespace GBEmmy
{
    public class MMU
    {
        public byte this[ushort addr]
        {
            get { return ReadByte(addr); }
            set { WriteByte(addr, value); }
        }

        public byte this[byte high, byte low]
        {
            get { return ReadByte((high << 8) | low); }
            set { WriteByte((high << 8) | low, value); }
        }
        public byte this[int addr]
        {
            get { return ReadByte(addr); }
            set { WriteByte(addr, value); }
        }

        public byte ReadByte(int addr)
        {
            return ReadByte((ushort) addr);
        }

        public byte ReadByte(ushort addr)
        {
            throw new NotImplementedException();
        }

        public void WriteByte(int addr, byte val)
        {
            WriteByte((ushort) addr, val);
        }

        public void WriteByte(ushort addr, byte val)
        {
            throw new NotImplementedException();
        }
    }
}