using System;
using System.Windows.Forms;

namespace SudokuSolver.UI
{
    public partial class MainGame : Form
    {
        private GameAdapter gameAdapter;

        public MainGame()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            gameAdapter = new GameAdapter();
            gameAdapter.BuildGame(groupBox1);
            base.OnLoad(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gameAdapter.Solve();
        }
    }
}
