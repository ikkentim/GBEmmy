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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GBEmmy.Emulation.Processor.Registers;

namespace GBEmmy.Emulation.Processor
{
    public class RegisterCollection : IEnumerable<Register>
    {
        private readonly Register[] _data = new Register[0x100];

        public RegisterCollection(Z80 cpu, bool isCGB)
        {
            for (int i = 0; i < _data.Length; i++)
                _data[i] = new Register();

            //Register abnormal registers
            _data[RegisterAddress.DIV & 0xFF] = new DIV();
            _data[RegisterAddress.TAC & 0xFF] = new TAC();
            _data[RegisterAddress.TIMA & 0xFF] = new TIMA((TAC)_data[RegisterAddress.TAC & 0xFF], cpu);

            if (isCGB)
            {
                //...
            }
        }

        public byte this[ushort address]
        {
            get { return _data[address & 0xFF].Value; }
            set { _data[address & 0xFF].Value = value; }
        }

        public T Get<T>() where T : Register
        {
            return _data.OfType<T>().FirstOrDefault();
        }

        public Register Get(ushort address)
        {
            return _data[address & 0xFF];
        }

        public IEnumerator<Register> GetEnumerator()
        {
            return (IEnumerator<Register>) _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}