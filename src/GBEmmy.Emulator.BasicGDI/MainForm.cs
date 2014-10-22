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
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GBEmmy.Emulation;
using GBEmmy.Emulation.Cartridges;
using GBEmmy.Emulation.Processor;
using GBEmmy.Emulation.VideoProcessor;

namespace GBEmmy.Emulator.BasicGDI
{
    public partial class MainForm : Form
    {
        private GameBoy gb;
        public MainForm()
        {
            InitializeComponent();

            gb = new GameBoy(new CartridgeStream("tetris.gb").ToCartridge());
            gb.Run();

            panel1.Paint += panel1_Paint;
            panel2.Paint += panel2_Paint;

            tdt = new TileDataTable(gb.Processor.Memory, 0x8000, false);
            bgtm = new BgTileMap(gb.Processor.Memory, 0x9800);
        }

        private TileDataTable tdt;
        private BgTileMap bgtm;
        void panel2_Paint(object sender, PaintEventArgs e)
        {
            for (int id=0;id<256;id++)
            {
                var t = tdt[(byte) (id)];

                    e.Graphics.DrawImage(t.ToBitmap(), (id%16)*8, (id/16)*8);
                
            }

        }

        

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            for(var x = 0;x<GPU.Width;x++)
                for (var y = 0; y < GPU.Height; y++)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb((int)((gb.VideoProcessor.ScreenBuffer[y * GPU.Width + x] >> 4) & 0xFF), 
                        (int)((gb.VideoProcessor.ScreenBuffer[y * GPU.Width + x] >> 2) & 0xFF), 
                    (int)(gb.VideoProcessor.ScreenBuffer[y * GPU.Width + x]) & 0xFF)), x, y, 1, 1);
                }
        }


        private void panelD1_Paint(object sender, PaintEventArgs e)
        {
            for (byte x = 0; x < 32; x++)
                for (byte y = 0; y < 32; y++)
                {
                    //e.Graphics.DrawImage(tdt[(byte)(id)].ToBitmap(), (id % 16) * 8, (id / 16) * 8);

                    var id = gb.Processor.Memory[0x9800 + x + y*32];


                    e.Graphics.DrawImage(tdt[(byte)(id)].ToBitmap(), x*8, y*8);
                    //e.Graphics.DrawImage(
                    //    bgtm[0, x, y].ToBitmap(), x*8, y*8);
                }
            for (byte x = 0; x < 32; x++)
                for (byte y = 0; y < 32; y++)
                {
                    //e.Graphics.DrawImage(tdt[(byte)(id)].ToBitmap(), (id % 16) * 8, (id / 16) * 8);

                    var id = gb.Processor.Memory[0x9800 + x + y * 32];

                    var tl = tdt[(byte) (id)];
                    if(id!=0)
                    e.Graphics.DrawString(id.ToString(), new Font("Arial", 8), Brushes.Blue, x * 8 - 4, y * 8 - 4);
                }
        }


        private void timer2_Tick(object sender, System.EventArgs e)
        {
            label1.Text = string.Format("AF: {0:X4}\nBC: {1:X4}\nDE: {2:X4}\nHL: {3:X4}\nIME: {4}\nPC: {5:X4}\nSP: {6:X4}\n{7}", gb.Processor.AF,
                gb.Processor.BC, gb.Processor.DE, gb.Processor.HL, gb.Processor.IME, gb.Processor.PC, gb.Processor.SP, (Flags)gb.Processor.F);

            panel1.Refresh();
            panel2.Refresh();
            panelD1.Refresh();
            ;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            listView1.BeginUpdate();
            listView1.ListViewItemSorter = null;
            listView1.Items.Clear();

            //here we add items to listview
            ushort st = gb.Processor.PC < 50 ? (ushort)0 : (ushort)(gb.Processor.PC - 50);

            listView2.Items.Clear();
            for (ushort addr = 0x8000; addr < 0x8000 + 0x1000;addr++)
            {
                if (gb.Processor.Memory[addr] != 0)
                listView2.Items.Add(
                    new ListViewItem(new[] {addr.ToString("X"), gb.Processor.Memory[addr].ToString("X2")}));
            }
            for (ushort addr = st; addr != ushort.MaxValue && addr < st + 100; )
            {
                var instraddr = addr;

                var instrid = gb.Processor.Memory[addr++];
                var instr = instrid == 0xCB
                    ? OpcodeTable.Cb[instrid = gb.Processor.Memory[instraddr = addr++]]
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
                bool l = false;
                bool r = false;
                if (dt.Contains(instr.Operand1))
                {
                    l = true; d++;
                }
                if (dt.Contains(instr.Operand2))
                {
                    r = true; d++;
                }

                var dv = new string[d];
                if (l)
                {
                    dv[0] = instr.Operand1 == Operand.Word || instr.Operand1 == Operand.Memory
                        ? (gb.Processor.Memory[addr++] | gb.Processor.Memory[addr++] << 8).ToString(
                            "X4")
                            : instr.Operand1 == Operand.SignedByte ? ((sbyte)gb.Processor.Memory[addr++]).ToString() + "(signed)" : gb.Processor.Memory[addr++].ToString("X2");
                }
                if (r)
                {
                    dv[dv.Length - 1] = instr.Operand2 == Operand.Word || instr.Operand1 == Operand.Memory
                        ? (gb.Processor.Memory[addr++] | gb.Processor.Memory[addr++] << 8).ToString(
                            "X4")
                        : instr.Operand2 == Operand.SignedByte ? ((sbyte)gb.Processor.Memory[addr++]).ToString() + "(signed)" : gb.Processor.Memory[addr++].ToString("X2");
                }
                ListViewItem x;
                listView1.Items.Add(x = new ListViewItem(new[] { instraddr.ToString("X2"), instrid.ToString("X2") + ":" + instr.ToString(), string.Join(",", dv) }));
                x.Selected = instraddr == gb.Processor.PC;
                if (x.Selected) x.EnsureVisible();

            }
            listView1.EndUpdate();
        }

    }
}