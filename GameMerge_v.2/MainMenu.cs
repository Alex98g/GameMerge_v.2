using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameMerge_v._2
{
    public partial class MainMenu : Form
    {
        public bool isShowWindow = false;
        public MainMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GamePlay gamePlay = new GamePlay();
            gamePlay.Show();
            this.Hide();
        }

        private void formClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
