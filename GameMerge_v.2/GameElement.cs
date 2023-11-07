using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMerge_v._2
{
    public class GameElement : Panel
    {

        public int Level { get; set; }
        public int ImageIndex { get; set; }

        public GameElement(int level, Color color, int imageindex)
        {
            Level = level;
            BackColor = color;
            Size = new Size(25, 25); // size of element
            AllowDrop = true; // allow drop element
            ImageIndex = imageindex;

            if (level == 1)
            {
                if ((color == Color.Blue) && (imageindex == 0))
                {
                    BackgroundImage = Image.FromFile("C:/Users/Alex/source/repos/GameMerge/GameMerge/Properties/Blue_Level1.png");

                }
                if ((color == Color.Green) && (imageindex == 4))
                {
                    BackgroundImage = Image.FromFile("C:/Users/Alex/source/repos/GameMerge/GameMerge/Properties/Green_Level1.png");
                }
                if ((color == Color.Red) && (imageindex == 8))
                {
                    BackgroundImage = Image.FromFile("C:/Users/Alex/source/repos/GameMerge/GameMerge/Properties/Red_Level1.png");
                }


            }
            if (level == 2)
            {
                if ((color == Color.Blue) && (imageindex == 1))
                {
                    BackgroundImage = Image.FromFile("C:/Users/Alex/source/repos/GameMerge/GameMerge/Properties/Blue_Level2.png");
                }
                if ((color == Color.Green) && (imageindex == 5))
                {
                    BackgroundImage = Image.FromFile("C:/Users/Alex/source/repos/GameMerge/GameMerge/Properties/Green_Level2.png");
                }
                if ((color == Color.Red) && (imageindex == 9))
                {
                    BackgroundImage = Image.FromFile("C:/Users/Alex/source/repos/GameMerge/GameMerge/Properties/Red_Level2.png");
                }
            }
            if (level == 3)
            {
                if ((color == Color.Blue) && (imageindex == 2))
                {
                    BackgroundImage = Image.FromFile("C:/Users/Alex/source/repos/GameMerge/GameMerge/Properties/Blue_Level3.png");
                }
                if ((color == Color.Green) && (imageindex == 6))
                {
                    BackgroundImage = Image.FromFile("C:/Users/Alex/source/repos/GameMerge/GameMerge/Properties/Green_Level3.png");
                }
                if ((color == Color.Red) && (imageindex == 10))
                {
                    BackgroundImage = Image.FromFile("C:/Users/Alex/source/repos/GameMerge/GameMerge/Properties/Red_Level3.png");
                }
            }
            if (level == 4)
            {
                if ((color == Color.Blue) && (imageindex == 3))
                {
                    BackgroundImage = Image.FromFile("C:/Users/Alex/source/repos/GameMerge/GameMerge/Properties/Blue_Level4.png");
                }
                if ((color == Color.Green) && (imageindex == 7))
                {
                    BackgroundImage = Image.FromFile("C:/Users/Alex/source/repos/GameMerge/GameMerge/Properties/Green_Level4.png");
                }
                if ((color == Color.Red) && (imageindex == 11))
                {
                    BackgroundImage = Image.FromFile("C:/Users/Alex/source/repos/GameMerge/GameMerge/Properties/Red_Level4.png");
                }
            }
            BackgroundImageLayout = ImageLayout.Zoom;

        }
    }
}
