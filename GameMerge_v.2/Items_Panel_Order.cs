using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMerge_v._2
{
    public class Items_Panel_Order : ListViewItem
    {
        public int Score { get; private set; }
        public Items_Panel_Order(int imageIndex, string text, int score)
        {
            ImageIndex = imageIndex;
            Text = text;
            Score = score;
        }
    }
}
