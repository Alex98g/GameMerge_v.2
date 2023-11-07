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
    public partial class Win : Form
    {
        public Win(int winTotalScore)
        {
            InitializeComponent();
            label_TotalScore.Text = winTotalScore.ToString();

        }

        private void click_button(object sender, EventArgs e)
        {
            GamePlay gameplay = new GamePlay();
            this.Close();
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();


        }
    }
}
