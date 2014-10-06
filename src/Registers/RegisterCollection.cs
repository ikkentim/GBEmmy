using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GBEmmy.Registers
{
    class RegisterCollection : IEnumerable<IRegister>
    {
        private readonly IRegister[] _registers = new IRegister[0x100];//$FF00-$FFFF

        public IRegister this[ushort address]
        {
            get { return (address & 0xFF00) == 0 ? null : _registers[address & 0xFF]; }
            set
            {
                if ((address & 0xFF00) == 0) return;
                _registers[address & 0xFF] = value;
            }
        }

        public IRegister Get<T>() where T : IRegister
        {
            return _registers.OfType<T>().FirstOrDefault();
        }

        public IEnumerator<IRegister> GetEnumerator()
        {
            return ((IEnumerable<IRegister>) _registers).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
