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
using System.Windows.Forms;
using GBEmmy.Cartridges;

namespace GBEmmy
{
    public partial class GameForm : Form
    {
        public GameForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            var loader = new CartridgeStream("cpu_instrs.gb");

            var gb = new GameBoy(loader.ToCartridge());

            gameBoyDisplay1.SetGPU(gb.VideoProcessor);
            gb.Run();

            base.OnLoad(e);
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}