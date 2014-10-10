using System.Windows.Forms;
using GBEmmy.Emulation;
using GBEmmy.Emulation.Cartridges;

namespace GBEmmy.Emulator.BasicGDI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            var gb = new GameBoy(new CartridgeStream("cpu_instrs.gb").ToCartridge());
            gb.Run();
        }
    }
}
