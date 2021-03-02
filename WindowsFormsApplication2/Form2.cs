using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApplication2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        bool c = true;

        private void Form2_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();
            //button1.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.BackColor = Color.Transparent;
            button1.Parent = pictureBox1;
            button1.BackColor = Color.Transparent;
            button1.BringToFront();

            //pictureBox1.BackColor = System.Drawing.Color.Transparent;
            
            //Thread.Sleep(3000);

            //pictureBox1.Load("無須.png");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Load("開頭3.png");

            button1.Visible = true;
            button1.Enabled = true;

            timer2.Enabled = true;
            timer2.Start();
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.BackColor = Color.Transparent;
            button1.Parent = pictureBox1;
            button1.BackColor = Color.Transparent;
            button1.BringToFront();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (c == true)
            {
                button1.ForeColor = Color.Red;
                Thread.Sleep(500);
                c = false;
            }
            else
            {
                button1.ForeColor = Color.Black;
                Thread.Sleep(500);
                c = true;
            }
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.BackColor = Color.Transparent;
            button1.Parent = pictureBox1;
            button1.BackColor = Color.Transparent;
            button1.BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show(this);
            timer1.Stop();
            timer1.Enabled = false;
            timer2.Stop();
            timer2.Enabled = false;
            this.Enabled = false;
            this.Visible = false;
            
        }


    }
}
