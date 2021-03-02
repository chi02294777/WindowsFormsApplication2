using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    class Player
    {
        private int block_index;
        private int money;
        private bool skill_used;
        private Point location;
        private PlayerState state;
        private PictureBox image;
        private int item1;
        private int item2;
        private int item3;

        public Player()
        {

        }

        public Player(string path)
        {
            BlockIndex = 0;
            money = 5000;
            skill_used = false;
            state = PlayerState.Normal;
            image = new PictureBox();
            image.SizeMode = PictureBoxSizeMode.StretchImage;
            image.BackColor = System.Drawing.Color.Transparent;
            image.Size = new Size(50, 50);
            image.Load(path);
            item1 = 1;
            item2 = 1;
            item3 = 1;
        }

        

        public int BlockIndex
        {
            get {return block_index; }
            set {block_index = (value < 0 ? 0 : value); }
        }

        public int Money
        {
            get {return money; }
            set {money = value; }
        }

        public int Item1
        {
            get { return item1; }
            set {item1 = value;  }
        }

        public int Item2
        {
            get { return item2; }
            set { item2 = value; }
        }

        public int Item3
        {
            get { return item3; }
            set { item3 = value; }
        }

        public bool SkillUsed
        {
            get {return skill_used; }
            set { skill_used = value; }
        }

        public Point Location
        {
            get {return location; }
            set
            {
                location = value;
                image.Location = new Point(Location.X + 290 ,Location.Y + 20);
            }
        }

        public PlayerState State
        {
            get {return state; }
            set { state = value; }
        }

        public PictureBox Image
        {
            get { return image; }
        }

        public virtual void Skill(ref Player[] players)
        {

        }

        public virtual void Change_state()
        {

        }
    }
}
