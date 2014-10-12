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

using System.Linq;
using System.Windows.Forms;
using GBEmmy.Emulation;
using GBEmmy.Emulation.Cartridges;
using GBEmmy.Emulation.Processor;

namespace GBEmmy.Emulator.BasicGDI
{
    public partial class MainForm : Form
    {
        private GameBoy gb;
        public MainForm()
        {
            InitializeComponent();

            gb = new GameBoy(new CartridgeStream("cpu_instrs.gb").ToCartridge());
            gb.Run();

            timer1.Tick += (sender, args) =>
            {
                listView1.BeginUpdate();
                listView1.ListViewItemSorter = null;
                listView1.Items.Clear();

                //here we add items to listview
                for (ushort addr = 0; addr < 0x100;)
                {
                    var instraddr = addr;

                    var instrid = gb.Processor.Memory[addr++];
                    var instr = instrid == 0xCB
                        ? OpcodeTable.Cb[gb.Processor.Memory[instraddr = addr++]]
                        : OpcodeTable.Base[instrid];

                    var dt = new[]
                    {
                        Operand.Byte,
                        Operand.Memory,
                        Operand.MemoryByte,
                        Operand.SignedByte,
                        Operand.Word
                    };
                    var d = 0;
                    bool l=false;
                    bool r=false;
                    if (dt.Contains(instr.Operand1))
                    {
                        l = true; d++;}
                    if (dt.Contains(instr.Operand2))
                    {
                        r = true;d++;
                    }

                    var dv = new string[d];
                    if (l)
                    {
                        dv[0] = instr.Operand1 == Operand.Word
                            ? (gb.Processor.Memory[addr++] | gb.Processor.Memory[addr++] << 8).ToString(
                                "X4")
                                : instr.Operand1 == Operand.SignedByte ? ((sbyte)gb.Processor.Memory[addr++]).ToString("X2") : gb.Processor.Memory[addr++].ToString("X2");
                    }
                    if (r)
                    {
                        dv[dv.Length -1] = instr.Operand2 == Operand.Word
                            ? (gb.Processor.Memory[addr++] | gb.Processor.Memory[addr++] << 8).ToString(
                                "X4")
                            : instr.Operand2 == Operand.SignedByte ? ((sbyte)gb.Processor.Memory[addr++]).ToString("X2") : gb.Processor.Memory[addr++].ToString("X2");
                    }
                    ListViewItem x;
                    listView1.Items.Add(x=new ListViewItem(new[] {instraddr.ToString("X2"), instr.ToString(),  string.Join("," , dv)}));
                    x.Selected = instraddr == gb.Processor.PC;
                    if(x.Selected)x.EnsureVisible();

                }
                listView1.EndUpdate();
                timer1.Enabled = false;
            };
        }

        private void timer2_Tick(object sender, System.EventArgs e)
        {
            label1.Text = string.Format("AF: {0:X4}\nBC: {1:X4}\nDE: {2:X4}\nHL: {3:X4}\nIFF: {4:X4}\nPC: {5:X4}\nSP: {6:X4}", gb.Processor.AF,
                gb.Processor.BC, gb.Processor.DE, gb.Processor.HL, gb.Processor.IFF, gb.Processor.PC, gb.Processor.SP);
        }
    }
}