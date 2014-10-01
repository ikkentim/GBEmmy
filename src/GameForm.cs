using System;
using System.Windows.Forms;

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
            var z = (new Z80());

            base.OnLoad(e);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
