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
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using GBEmmy.Emulation.Processor.Operations;

namespace GBEmmy.Emulation.Processor
{
    public struct Opcode
    {
        #region Operators

        private static readonly IOperation[] _operators =
        {
            new NopOperation(),
            new EmptyOperation(),
            new LdOperation(),
            new RlcOperation(),
            new RrcOperation(),
            new RlOperation(),
            new RrOperation(),
            new SlaOperation(),
            new SraOperation(),
            new SwapOperation(),
            new SrlOperation(),
            new BitOperation(),
            new ResOperation(),
            new SetOperation(),
            new IncOperation(),
            new DecOperation(),
            new RlcaOperation(),
            new AddOperation(),
            new RrcaOperation(),
            new AdcOperation(),
            new StopOperation(),
            new RlaOperation(),
            new JrOperation(),
            new RraOperation(),
            new LdiOperation(),
            new DaaOperation(),
            new CplOperation(),
            new LddOperation(),
            new ScfOperation(),
            new CcfOperation(),
            new HaltOperation(),
            new SubOperation(),
            new SbcOperation(),
            new AndOperation(),
            new XorOperation(),
            new OrOperation(),
            new CpOperation(),
            new RetOperation(),
            new PopOperation(),
            new JpOperation(),
            new CallOperation(),
            new PushOperation(),
            new RstOperation(),
            new RetiOperation(),
            new LdhOperation(),
            new DiOperation(),
            new LdhlOperation(),
            new EiOperation()
        };

        #endregion

        private readonly byte _embedded;

        public Opcode(string instruction, byte length, byte duration, byte conditionalDuration = 0) : this()
        {
            Length = length;
            Duration = duration;
            ConditionalDuration = conditionalDuration;

            string[] tmp = instruction.Split(' ', ',');
            string name = Operator = tmp[0];
            IEnumerable<string> operands = tmp.Skip(1);

            Operation = _operators.FirstOrDefault(
                o => o.GetType().Name.Substring(0, o.GetType().Name.Length - "Operation".Length).ToUpper() == name);
            _embedded = 0;

            if (operands.Any()) Operand1 = GetOperand(operands.ElementAt(0), name, ref _embedded);
            if (operands.Count() == 2) Operand2 = GetOperand(operands.ElementAt(1), name, ref _embedded);
        }

        public string Operator { get; private set; }
        public byte Length { get; private set; }
        public byte Duration { get; private set; }
        public byte ConditionalDuration { get; private set; }

        public byte Embedded
        {
            get { return _embedded; }
        }

        public IOperation Operation { get; private set; }
        public Operand Operand1 { get; private set; }
        public Operand Operand2 { get; private set; }

        private static Operand GetOperand(string name, string @operator, ref byte embedded)
        {
            byte embeddedValue;
            if (byte.TryParse(name, out embeddedValue))
            {
                embedded = embeddedValue;
                return Operand.Embedded;
            }

            if (name.Length > 1 && name.Last() == 'H')
            {
                embedded = Convert.ToByte(name.Substring(0, name.Length - 1), 16);
                return Operand.Embedded;
            }

            Operand value;
            if (Operand.TryParse(name, true, out value))
            {
                if (!(value == Operand.C && new[] {"JR", "JP", "CALL", "RET"}.Contains(@operator)))
                    //These C's are C flags
                    return value;
            }

            switch (name)
            {
                case "(BC)":
                    return Operand.MemoryBC;
                case "(DE)":
                    return Operand.MemoryDE;
                case "(HL)":
                    return Operand.MemoryHL;
                case "d8":
                    return Operand.Byte;
                case "(a8)":
                    return Operand.MemoryByte;
                case "(C)":
                    return Operand.MemoryC;
                case "a16":
                case "(a16)":
                    return Operand.Memory;
                case "d16":
                    return Operand.Word;
                case "r8":
                    return Operand.SignedByte;
                case "NZ":
                    return Operand.NotZero;
                case "Z":
                    return Operand.Zero;
                case "NC":
                    return Operand.NotCarry;
                case "C":
                    return Operand.Carry;
                default:
                    throw new Exception(string.Format("Unknown operand {0}", name));
            }
        }

        public int Call(Z80 cpu)
        {
            return Operation.Call(cpu, Operand1, Operand2, Embedded) ? Duration : ConditionalDuration;
        }

        public override string ToString()
        {
            var em = Embedded;
            var ops = new[] {Operand1, Operand2}.Where(o => o != Operand.None)
                .Select(o => o == Operand.Embedded ? em.ToString() : o.GetDescription());
            return string.Format("{0} {1}", Operator, string.Join(",", ops));
        }
    }

    public static class OperandHelper
    {
        public static string GetDescription(this Enum value)
        {
            var a = value.GetType().GetField(value.ToString()).GetCustomAttribute<DescriptionAttribute>();
            return a == null ? null : a.Description;
        }
    }
}