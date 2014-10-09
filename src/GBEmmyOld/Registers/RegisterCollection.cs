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

namespace GBEmmy.Registers
{
    public class RegisterCollection : IEnumerable<IRegister>
    {
        private readonly IRegister[] _registers = new IRegister[0x100]; //$FF00-$FFFF

        public RegisterCollection ()
        {
            for(int i=0;i<_registers.Length;i++)
                _registers[i] = new Register();
        }
        public byte this[ushort address]
        {
            get
            {
                return (address & 0xFF00) == 0
                    ? (byte) 0x00
                    : _registers[address & 0xFF].Value;
            }
            set
            {
                if ((address & 0xFF00) == 0) return;
                _registers[address & 0xFF].Value = value;
            }
        }

        public IEnumerator<IRegister> GetEnumerator()
        {
            return ((IEnumerable<IRegister>) _registers).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public T Get<T>() where T : IRegister
        {
            return _registers.OfType<T>().FirstOrDefault();
        }

        public void Set(ushort address, IRegister register)
        {
            if ((address & 0xFF00) == 0) return;
            _registers[address & 0xFF] = register;
        }
    }
}