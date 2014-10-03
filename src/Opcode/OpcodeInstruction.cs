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
using System.Linq;
using GBEmmy.Opcode.Operation;

namespace GBEmmy.Opcode
{
    public struct OpcodeInstruction
    {
        public OpcodeInstruction(string description, string flagModifiers, ushort length, ushort duration,
            ushort conditionalDuration = 0)
            : this()
        {
            //FlagModifiers = flagModifiers;
            //Length = length;
            Duration = duration;
            ConditionalDuration = conditionalDuration;


            //operation
            string[] oParts = description.Split(' ');
            string @operator = oParts[0];
            string[] operands = oParts.Length == 1 ? new string[0] : oParts[1].Split(',');

            var operators = new IOperation[]
            {
                new NopOperation(),
                new EmptyOperation(),
                new LdOperation(),
                new RlcOperation(),
                new RrcOperation(),
                new RlOperation(),
                new RrOperation(),
                new SlaOperation(),
                new SraOperation()
            };

            IOperation operation = operators.FirstOrDefault(
                o => o.GetType().Name.Substring(0, o.GetType().Name.Length - "Operation".Length).ToUpper() == @operator);

            if (operation == null) throw new Exception(string.Format("Opcode {0} has no operation", description));

            //operands
            if (operands.Length >= 1) Operand1 = GetOperandByName(operands[0]);
            if (operands.Length >= 2) Operand2 = GetOperandByName(operands[1]);
        }

        public ushort Duration { get; set; }
        public ushort ConditionalDuration { get; set; }

        public Operand Operand1 { get; set; }
        public Operand Operand2 { get; set; }

        private static Operand GetOperandByName(string name)
        {
            Operand value;
            if (Operand.TryParse(name, true, out value))
            {
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
                case "a16":
                    return Operand.Memory;
                case "d16":
                    return Operand.Word;
                case "r8":
                    return Operand.SignedByte;

                default:
                    throw new Exception(string.Format("Unknown operand {0}", name));
            }
        }
    }
}