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
    public partial class Form3 : Form
    {
        /*
        public string String1  
        {  
            set  
            {  
                string1 = value;  
            }  
        }  
        */

        public Form3()
        {
            InitializeComponent();
        }

        public Form3(int p)
        {
            // TODO: Complete member initialization
            this.p = p;
        }
        public int score_Tom;
        public int score_Hack;
        public int score_Peiky;
        public int score_Cider;

        private int p;
        private int[] score = new int[4];
        private bool s = true;
        private bool d = true;
        private bool f = true;
        private bool g = true;
        private int level_1;
        private int level_2;
        private int level_3;
        private int level_4;

        private void Form3_Load(object sender, EventArgs e)
        {
            label9.Text = score_Tom.ToString();
            label17.Text = score_Hack.ToString();
            label15.Text = score_Peiky.ToString();
            label18.Text = score_Cider.ToString();

            score[0] = score_Tom;
            score[1] = score_Hack;
            score[2] = score_Peiky;
            score[3] = score_Cider;

            bubblesort(score);

            level_1 = score[0];
            level_2 = score[1];
            level_3 = score[2];
            level_4 = score[3];

            start_timer();

        }

        private void bubblesort(int[] score)
        {
            //throw new NotImplementedException();
            int i, j, temp;
            for (i = 4 - 1; i > 0; i--)
            {
                for (j = 0; j <= i - 1; j++)
                {
                    if (score[j] < score[j + 1])
                    {
                        temp = score[j];
                        score[j] = score[j + 1];
                        score[j + 1] = temp;
                    }
                }
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (score_Tom == level_1)
            {
                label10.Text = "1";

                if (s == true)
                {
                    panel4.BackColor = Color.Red;
                    Thread.Sleep(300);
                    s = false;
                }
                else
                {
                    panel4.BackColor = Color.Khaki;
                    Thread.Sleep(300);
                    s = true;
                }
            }
            else if (score_Tom == level_2)
            {
                label10.Text = "2";
                timer1.Stop();
            }
            else if (score_Tom == level_3)
            {
                label10.Text = "3";
                timer1.Stop();
            }
            else if (score_Tom == level_4)
            {
                label10.Text = "4";
                timer1.Stop();
            }       
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (score_Hack == level_1)
            {
                label11.Text = "1";

                if (g == true)
                {
                    panel5.BackColor = Color.Red;
                    Thread.Sleep(300);
                    g = false;
                }
                else
                {
                    panel5.BackColor = Color.LightSalmon;
                    Thread.Sleep(300);
                    g = true;
                }

            }
            else if (score_Hack == level_2)
            {
                label11.Text = "2";
                timer2.Stop();
            }
            else if (score_Hack == level_3)
            {
                label11.Text = "3";
                timer2.Stop();
            }
            else if (score_Hack == level_4)
            {
                label11.Text = "4";
                timer2.Stop();
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (score_Peiky == level_1)
            {
                label13.Text = "1";

                if (d == true)
                {
                    panel6.BackColor = Color.Red;
                    Thread.Sleep(300);
                    d = false;
                }
                else
                {
                    panel6.BackColor = Color.Aquamarine;
                    Thread.Sleep(300);
                    d = true;
                }
            }
            else if (score_Peiky == level_2)
            {
                label13.Text = "2";
                timer3.Stop();
            }
            else if (score_Peiky == level_3)
            {
                label13.Text = "3";
                timer3.Stop();
            }
            else if (score_Peiky == level_4)
            {
                label13.Text = "4";
                timer3.Stop();
            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            if (score_Cider == level_1)
            {
                label8.Text = "1";

                if (f == true)
                {
                    panel7.BackColor = Color.Red;
                    Thread.Sleep(300);
                    f = false;
                }
                else
                {
                    panel7.BackColor = Color.Plum;
                    Thread.Sleep(300);
                    f = true;
                }

            }
            else if (score_Cider == level_2)
            {
                label8.Text = "2";
                timer4.Stop();
            }
            else if (score_Cider == level_3)
            {
                label8.Text = "3";
                timer4.Stop();
            }
            else if (score_Cider == level_4)
            {
                label8.Text = "4";
                timer4.Stop();
            }
        }

        private void start_timer()
        {
            
                timer4.Start();
            
                timer3.Start();
            
                timer2.Start();
           
                timer1.Start();
         }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
    }
}
