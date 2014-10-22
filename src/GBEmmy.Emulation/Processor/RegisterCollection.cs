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
            _data[RegisterAddress.TIMA & 0xFF] = new TIMA(cpu, this);
            _data[RegisterAddress.IE & 0xFF] = new IE();
            _data[RegisterAddress.LCDC & 0xFF] = new LCDC();
            _data[RegisterAddress.LY & 0xFF] = new LY();
            _data[RegisterAddress.STAT & 0xFF] = new STAT(cpu, this);

            _data[RegisterAddress.BGP & 0xFF] = new BGP();
            _data[RegisterAddress.DMA & 0xFF] = new DMA(cpu);
            _data[RegisterAddress.IF & 0xFF] = new IF(cpu);
            _data[RegisterAddress.OBP0 & 0xFF] = new OBP0();
            _data[RegisterAddress.OBP1 & 0xFF] = new OBP1();
            _data[RegisterAddress.P1 & 0xFF] = new P1();


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

        public IEnumerator<Register> GetEnumerator()
        {
            return (IEnumerator<Register>) _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public T Get<T>() where T : Register
        {
            return _data.OfType<T>().FirstOrDefault();
        }

        public Register Get(ushort address)
        {
            return _data[address & 0xFF];
        }
    }
}