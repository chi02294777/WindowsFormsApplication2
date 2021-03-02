using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        private int step;
        private int now_player;
        private Player[] players;
        private Block[] map;
        private int round = 0;
        private int max_round = 0;
        private bool first = true;
        private bool systemmode = false;

        public Form1()
        {
            InitializeComponent();
        }

        const int FPS = 50;
        const int DiceNum = 6;
        const double DiceSpeed = 30;
        const double DiceChange = 50;
        const int SlotNum = 10;
        const double SlotSpeed = 30;
        const double SlotChange = 100;

        enum State
        {
            NonUse = 0,
            Rotating = 1,
            Slowing = 2,
            Stop = 3
        }

        double d_speed;

        double d_count;

        int d_place;

        State d_status;

        Bitmap[] d;

        Random rand;

        private void Form1_Load(object sender, EventArgs e)
        {
            //label34.Text = max_round.ToString();
            if (first == true)
            {
                panel1.Enabled = false;
                panel1.Visible = false;
                panel10.Enabled = true;
                panel10.Visible = true;
            }

            pictureBox6.Load("商店關閉1.png");
            label44.Text = "";
            panel8.Enabled = false;

            d_status = State.NonUse;

            d = new Bitmap[DiceNum];
            for (int i = 0; i < DiceNum; ++i)
            {
                d[i] = new Bitmap("d" + (i + 1).ToString() + ".bmp");
            }

            rand = new Random();

            timer2.Interval = 1000 / FPS;
            timer2.Start();

            now_player = 0;
            PictureBox_Map.Load("map6-2.png");

            map = new Block[40];
            map[0] = new StartBlock();
            map[0].Location = new Point(290, 20);
            map[1] = new NoLuckyBlock();
            map[1].Location = new Point(380, 20);
            map[2] = new TomHouseBlock();
            map[2].Location = new Point(470, 20) ;
            map[3] = new StateLuckyBlock();
            map[3].Location = new Point(560, 20);
            map[4] = new NormalBlock();
            map[4].Location = new Point(650, 20);
            map[5] = new LuckyBlock();
            map[5].Location = new Point(650, 110);
            map[6] = new LuckyBlock();
            map[6].Location = new Point(650, 200);
            map[7] = new SchoolBlock();
            map[7].Location = new Point(650, 290);
            map[8] = new StateNoLuckyBlock();
            map[8].Location = new Point(740, 290);
            map[9] = new StateLuckyBlock();
            map[9].Location = new Point(830, 290);
            map[10] = new NormalBlock();
            map[10].Location = new Point(920, 290);
            map[11] = new LuckyBlock();
            map[11].Location = new Point(920, 380);
            map[12] = new HackTreeHouseBlock();
            map[12].Location = new Point(920, 470);
            map[13] = new NoLuckyBlock();
            map[13].Location = new Point(920, 560);
            map[14] = new BuyStoreBlock();
            map[14].Location = new Point(920, 650);
            map[15] = new LuckyBlock();
            map[15].Location = new Point(830, 650);
            map[16] = new StateNoLuckyBlock();
            map[16].Location = new Point(740, 650);
            map[17] = new ChurchBlock();
            map[17].Location = new Point(650, 650);
            map[18] = new StateLuckyBlock();
            map[18].Location = new Point(650, 740);
            map[19] = new StateNoLuckyBlock();
            map[19].Location = new Point(650, 830);
            map[20] = new NormalBlock();
            map[20].Location = new Point(650, 920);
            map[21] = new NoLuckyBlock();
            map[21].Location = new Point(560, 920);
            map[22] = new PeikyHouseBlock();
            map[22].Location = new Point(470, 920);
            map[23] = new StateLuckyBlock();
            map[23].Location = new Point(380, 920);
            map[24] = new NormalBlock();
            map[24].Location = new Point(290, 920);
            map[25] = new LuckyBlock();
            map[25].Location = new Point(290, 830);
            map[26] = new NoLuckyBlock();
            map[26].Location = new Point(290, 740);
            map[27] = new DockBlock();
            map[27].Location = new Point(290, 650);
            map[28] = new StateLuckyBlock();
            map[28].Location = new Point(200, 650);
            map[29] = new StateNoLuckyBlock();
            map[29].Location = new Point(110, 650);
            map[30] = new NormalBlock();
            map[30].Location = new Point(20, 650);
            map[31] = new LuckyBlock();
            map[31].Location = new Point(20, 560);
            map[32] = new ForestBlock();
            map[32].Location = new Point(20, 470);
            map[33] = new StateLuckyBlock();
            map[33].Location = new Point(20, 380);
            map[34] = new SellStoreBlock();
            map[34].Location = new Point(20, 290);
            map[35] = new NoLuckyBlock();
            map[35].Location = new Point(110, 290);
            map[36] = new StateNoLuckyBlock();
            map[36].Location = new Point(200, 290);
            map[37] = new PayMoneyBlock();
            map[37].Location = new Point(290, 290);
            map[38] = new StateLuckyBlock();
            map[38].Location = new Point(290, 200);
            map[39] = new LuckyBlock();
            map[39].Location = new Point(290, 110);
            
            players = new Player[4];

            players[0] = new Player("湯姆.png");
            players[0].Location = new Point(0,0);
            Controls.Add(players[0].Image);
            players[0].Image.Parent = PictureBox_Map;
            players[0].Image.BringToFront();

            players[1] = new Player("哈克.png");
            players[1].Location = new Point(0, 0);
            Controls.Add(players[1].Image);
            players[1].Image.Parent = PictureBox_Map;
            players[1].Image.BringToFront();

            players[2] = new Player("佩琪.png");
            players[2].Location = new Point(0, 0);
            Controls.Add(players[2].Image);
            players[2].Image.Parent = PictureBox_Map;
            players[2].Image.BringToFront();

            players[3] = new Player("席德.png");
            players[3].Location = new Point(0, 0);
            Controls.Add(players[3].Image);
            players[3].Image.Parent = PictureBox_Map;
            players[3].Image.BringToFront();

            information_label.Text = "      遊戲已經開始" + "\n\n" + "  請點選左側骰子" + "\n\n" + "  進行移動" + "\n\n" + "    祝大家遊戲愉快";

            SetUI(TurnPhase.Initial);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            step = 1;
            SetUI(TurnPhase.Walk);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            step = 2;
            SetUI(TurnPhase.Walk);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            step = 3;
            SetUI(TurnPhase.Walk);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            step = 4;
            SetUI(TurnPhase.Walk);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            step = 5;
            SetUI(TurnPhase.Walk);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            step = 6;
            SetUI(TurnPhase.Walk);
        }

        private void Button_End_Click(object sender, EventArgs e)
        {

            leave_store();
            Button_End.BackColor = Color.WhiteSmoke;
            button23.BackColor = Color.WhiteSmoke;
            now_player = (now_player + 1) % 4;
            SetUI(TurnPhase.Initial);
            information_label.Text = "";
            round++;
            int tmp;
            tmp = max_round - round;
            label34.Text = tmp.ToString();

            if(round == max_round)
            {
                Form3 score_table = new Form3();
                score_table.score_Tom = players[0].Money;
                score_table.score_Hack = players[1].Money;
                score_table.score_Peiky = players[2].Money;
                score_table.score_Cider = players[3].Money;
                score_table.Show(this);

                this.Enabled = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (step != 0)
                PlayerMove(players[now_player]);
        }

        private void SetUI(TurnPhase phase)
        {
            switch(phase)
            {
                case TurnPhase.Initial:
                    if(players[now_player].State != PlayerState.DiceAgain)
                    {
                        pictureBox1.Load("小鎮.png");
                    }
                    if (now_player == 0) Label_Player.Text = "湯姆";
                    if (now_player == 1) Label_Player.Text = "哈克";
                    if (now_player == 2) Label_Player.Text = "佩琪";
                    if (now_player == 3) Label_Player.Text = "席德";
                    //Label_Money.Text = players[now_player].Money.ToString();
                    //Label_State.Text = players[now_player].State.ToString();
/*
                    label9.Text = players[0].Money.ToString();
                    label10.Text = players[0].State.ToString();
                    int temp;
                    temp = players[0].BlockIndex + 1;
                    label11.Text = temp.ToString();

                    label17.Text = players[1].Money.ToString();
                    label15.Text = players[1].State.ToString();
                    temp = players[1].BlockIndex + 1;
                    label13.Text = temp.ToString();

                    label24.Text = players[2].Money.ToString();
                    label22.Text = players[2].State.ToString();
                    temp = players[2].BlockIndex + 1;
                    label20.Text = temp.ToString();

                    label31.Text = players[3].Money.ToString();
                    label29.Text = players[3].State.ToString();
                    temp = players[3].BlockIndex + 1;
                    label27.Text = temp.ToString();
 * 
 * 
*/                 
                    use_item_check();
                    if (players[now_player].State == PlayerState.Normal || players[now_player].State == PlayerState.SpeedUp || players[now_player].State == PlayerState.SpeedDown)
                    {
                        button14.Enabled = false;
                    }
                    reloaditem();
                    reload();

                    label33.Text = players[now_player].Item1.ToString();
                    
                    if (players[now_player].State == PlayerState.Stop)
                    {
                        button1.Enabled = false;
                        button2.Enabled = false;
                        button3.Enabled = false;
                        button4.Enabled = false;
                        button5.Enabled = false;
                        button6.Enabled = false;
                        button_start.Enabled = false;
                        button21.Enabled = false;
                        button8.Enabled = false;
                        Button_End.Enabled = true;
                        button23.Enabled = true;
                        players[now_player].State = PlayerState.Normal;
                    }
                 /*   else
                        if (players[now_player].State == PlayerState.SpeedDown)
                    {
                        button1.Enabled = true;
                        button2.Enabled = true;
                        button3.Enabled = true;
                        button4.Enabled = false;
                        button5.Enabled = false;
                        button6.Enabled = false;
                        Button_End.Enabled = false;
                    }*/
                    else
                    {
                        players[now_player].Image.BringToFront();
                        button1.Enabled = true;
                        button2.Enabled = true;
                        button3.Enabled = true;
                        button4.Enabled = true;
                        button5.Enabled = true;
                        button6.Enabled = true;
                        button8.Enabled = true;
                        button_start.Enabled = true;
                        button21.Enabled = true;
                        Button_End.Enabled = false;
                        button23.Enabled = false;
                    }
                       break;
                case TurnPhase.Walk:
                       you_cant_use_item();
                       if (players[now_player].State == PlayerState.SpeedUp)
                       {
                           step = step + 2;
                           Label_Step.Text = step.ToString();
                           button1.Enabled = false;
                           button2.Enabled = false;
                           button3.Enabled = false;
                           button4.Enabled = false;
                           button5.Enabled = false;
                           button6.Enabled = false;
                           Button_End.Enabled = false;
                           button23.Enabled = false;
                       }
                       else
                       if (players[now_player].State == PlayerState.SpeedDown)
                       {
                           step = step - 2;
                           if (step <= 0) step = 1;
                           button1.Enabled = false;
                           button2.Enabled = false;
                           button3.Enabled = false;
                           button4.Enabled = false;
                           button5.Enabled = false;
                           button6.Enabled = false;
                           Button_End.Enabled = false;
                           button23.Enabled = false;
                       }
                       else
                       {
                           Label_Step.Text = step.ToString();
                           button1.Enabled = false;
                           button2.Enabled = false;
                           button3.Enabled = false;
                           button4.Enabled = false;
                           button5.Enabled = false;
                           button6.Enabled = false;
                           Button_End.Enabled = false;
                           button23.Enabled = false;
                       }
                    break;
                case TurnPhase.Dice:
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    button4.Enabled = true;
                    button5.Enabled = true;
                    button6.Enabled = true;
                    Button_End.Enabled = false;
                    button23.Enabled = false;
                    break;
                case TurnPhase.End:
                    if (players[now_player].Item2 > 0)
                    {
                        button14.Enabled = true;
                    }
                    reload();
                    reloaditem();
                    if (players[now_player].State == PlayerState.DiceAgain)
                    {
                        SetUI(TurnPhase.Initial);
                    }
                    else
                    {
                        button1.Enabled = false;
                        button2.Enabled = false;
                        button3.Enabled = false;
                        button4.Enabled = false;
                        button5.Enabled = false;
                        button6.Enabled = false;
                        Button_End.Enabled = true;
                        Button_End.BackColor = Color.Red;
                        button23.Enabled = true;
                        button23.BackColor = Color.Red;
                        button8.Enabled = false;
                        button_start.Enabled = false;
                        button21.Enabled = false;
                    }
                    break;
            }
        }

        private void reload()
        {
            Label_Money.Text = players[now_player].Money.ToString();
            Label_State.Text = players[now_player].State.ToString();

            label9.Text = players[0].Money.ToString();
            label10.Text = players[0].State.ToString();
            int temp;
            temp = players[0].BlockIndex + 1;
            label11.Text = temp.ToString();

            label17.Text = players[1].Money.ToString();
            label15.Text = players[1].State.ToString();
            temp = players[1].BlockIndex + 1;
            label13.Text = temp.ToString();

            label24.Text = players[2].Money.ToString();
            label22.Text = players[2].State.ToString();
            temp = players[2].BlockIndex + 1;
            label20.Text = temp.ToString();

            label31.Text = players[3].Money.ToString();
            label29.Text = players[3].State.ToString();
            temp = players[3].BlockIndex + 1;
            label27.Text = temp.ToString();
        }

        private void PlayerMove(Player player)
        {

            int now_index = player.BlockIndex;
            int next_index = (player.BlockIndex + 1) % 40;
            Point dis = new Point((map[next_index].Location.X - map[now_index].Location.X) / 10,
                                  (map[next_index].Location.Y - map[now_index].Location.Y) / 10);
            player.Location = new Point(player.Location.X + dis.X, player.Location.Y + dis.Y);

            if (player.Location.X + 290 == map[next_index].Location.X &&
                player.Location.Y + 20 == map[next_index].Location.Y)
            {
                --step;
                Label_Step.Text = step.ToString();
                player.BlockIndex = next_index;

                if (step == 0)
                {
                    Random evenrand;
                    int r=0;
                    map[player.BlockIndex].StopAction(ref player);

                    if (map[players[now_player].BlockIndex] is NormalBlock)
                    {
                        information_label.Text = "今天又是一個晴朗的好天氣 希望有好運事發生。" + "\n\n" + "無效果";
                        pictureBox1.Load("無須.png");
                    }
                    else
                    if (map[players[now_player].BlockIndex] is StateLuckyBlock)
                    {
                        if (now_player == 0 || now_player == 1)
                        {
                            evenrand = new Random();
                            r = evenrand.Next(4) + 1;

                            if(r == 1)
                            {
                                players[now_player].State = PlayerState.DiceAgain;
                                players[now_player].Money = players[now_player].Money + 1500;
                                information_label.Text = "與朋友一起搭乘熱氣球 是一個美好的體驗。" + "\n\n" + "再骰 1 次\n快樂值 + 1500";
                                pictureBox1.Load("搭熱氣球.png");
                            }
                            if(r == 2)
                            {
                                players[now_player].State = PlayerState.SpeedUp;
                                players[now_player].Money = players[now_player].Money + 500;
                                information_label.Text = "與朋友一起在河邊烤新鮮的魚 享受美味又享受快樂。" + "\n\n" + "加速 1 回合\n快樂值 + 500";
                                pictureBox1.Load("釣到大魚.png");
                            }
                            if(r == 3)
                            {
                                players[now_player].State = PlayerState.SpeedUp;
                                players[now_player].Money = players[now_player].Money + 300;
                                information_label.Text = "獲得雜貨店老闆的贈禮 美味的糖果 帶來好運。" + "\n\n" + "加速 1 回合\n快樂值 + 300";
                                pictureBox1.Load("偷吃糖果.png");
                            }
                            if(r == 4)
                            {
                                players[now_player].State = PlayerState.SpeedDown;
                                players[now_player].Money = players[now_player].Money + 700;
                                information_label.Text = "參加鎮上小孩的套圈圈比賽 是個疲勞又快樂的一天。" + "\n\n" + "減速 1 回合\n快樂值 + 700";
                                pictureBox1.Load("套圈圈比賽.png");
                            }
                        }
                        else
                        {
                            evenrand = new Random();
                            r = evenrand.Next(3) + 1;

                            if (r == 1)
                            {
                                players[now_player].State = PlayerState.SpeedUp;
                                players[now_player].Money = players[now_player].Money + 500;
                                information_label.Text = "與朋友一起在河邊烤新鮮的魚 享受美味又享受快樂。" + "\n\n" + "加速 1 回合\n快樂值 + 500";
                                pictureBox1.Load("釣到大魚.png");
                            }
                            if (r == 2)
                            {
                                players[now_player].State = PlayerState.SpeedUp;
                                players[now_player].Money = players[now_player].Money + 300;
                                information_label.Text = "獲得雜貨店老闆的贈禮 美味的糖果 帶來好運。" + "\n\n" + "加速 1 回合\n快樂值 + 300";
                                pictureBox1.Load("偷吃糖果.png");
                            }
                            if (r == 3)
                            {
                                players[now_player].State = PlayerState.SpeedDown;
                                players[now_player].Money = players[now_player].Money + 700;
                                information_label.Text = "參加鎮上小孩的套圈圈比賽 是個疲勞又快樂的一天。" + "\n\n" + "減速 1 回合\n快樂值 + 700";
                                pictureBox1.Load("套圈圈比賽.png");
                            }
                        }
                    }
                    else
                    if (map[players[now_player].BlockIndex] is StateNoLuckyBlock)
                    {
                        if (now_player == 1)
                        {
                            evenrand = new Random();
                            r = evenrand.Next(4) + 1;

                            if (r == 1)
                            {
                                players[now_player].State = PlayerState.Stop;
                                players[now_player].Money = players[now_player].Money - 800;
                                information_label.Text = "今日要修理屋子 抽不開身..." + "\n\n" + "囚禁 1 回合\n快樂值 - 800";
                                pictureBox1.Load("蓋房子(哈克).png");
                            }
                            if (r == 2)
                            {
                                players[now_player].State = PlayerState.Stop;
                                players[now_player].Money = players[now_player].Money - 600;
                                information_label.Text = "遇到暴風雨 無法移動..." + "\n\n" + "囚禁 1 回合\n快樂值 - 600";
                                pictureBox1.Load("遇到暴風雨.png");
                            }
                            if (r == 3)
                            {
                                players[now_player].State = PlayerState.SpeedDown;
                                players[now_player].Money = players[now_player].Money - 800;
                                information_label.Text = "遭遇毒蛇 被逼迫逃跑..." + "\n\n" + "減速 1 回合\n快樂值 - 800";
                                pictureBox1.Load("遇到蛇.png");
                            }
                            if (r == 4)
                            {
                                players[now_player].State = PlayerState.SpeedDown;
                                players[now_player].Money = players[now_player].Money - 1200;
                                information_label.Text = "誤觸蝙蝠洞穴 遭到蝙蝠襲擊..." + "\n\n" + "減速 1 回合\n快樂值 - 1200";
                                pictureBox1.Load("被蝙蝠襲擊.png");
                            }
                        }
                        else if (now_player == 3)
                        {
                            evenrand = new Random();
                            r = evenrand.Next(4) + 1;

                            if (r == 1)
                            {
                                players[now_player].State = PlayerState.SpeedDown;
                                players[now_player].Money = players[now_player].Money - 800;
                                information_label.Text = "身體不舒服 導致行動不便..." + "\n\n" + "囚禁 1 回合\n快樂值 - 800";
                                pictureBox1.Load("生病(席德).png");
                            }
                            if (r == 2)
                            {
                                players[now_player].State = PlayerState.Stop;
                                players[now_player].Money = players[now_player].Money - 600;
                                information_label.Text = "遇到暴風雨 無法移動..." + "\n\n" + "囚禁 1 回合\n快樂值 - 600";
                                pictureBox1.Load("遇到暴風雨.png");
                            }
                            if (r == 3)
                            {
                                players[now_player].State = PlayerState.SpeedDown;
                                players[now_player].Money = players[now_player].Money - 800;
                                information_label.Text = "遭遇毒蛇 被逼迫逃跑..." + "\n\n" + "減速 1 回合\n快樂值 - 800";
                                pictureBox1.Load("遇到蛇.png");
                            }
                            if (r == 4)
                            {
                                players[now_player].State = PlayerState.SpeedDown;
                                players[now_player].Money = players[now_player].Money - 1200;
                                information_label.Text = "誤觸蝙蝠洞穴 遭到蝙蝠襲擊..." + "\n\n" + "減速 1 回合\n快樂值 - 1200";
                                pictureBox1.Load("被蝙蝠襲擊.png");
                            }
                        }
                        else
                        {
                            evenrand = new Random();
                            r = evenrand.Next(3) + 1;

                            if (r == 1)
                            {
                                players[now_player].State = PlayerState.Stop;
                                players[now_player].Money = players[now_player].Money - 600;
                                information_label.Text = "遇到暴風雨 無法移動..." + "\n\n" + "囚禁 1 回合\n快樂值 - 600";
                                pictureBox1.Load("遇到暴風雨.png");
                            }
                            if (r == 2)
                            {
                                players[now_player].State = PlayerState.SpeedDown;
                                players[now_player].Money = players[now_player].Money - 800;
                                information_label.Text = "遭遇毒蛇 被逼迫逃跑..." + "\n\n" + "減速 1 回合\n快樂值 - 800";
                                pictureBox1.Load("遇到蛇.png");
                            }
                            if (r == 3)
                            {
                                players[now_player].State = PlayerState.SpeedDown;
                                players[now_player].Money = players[now_player].Money - 1200;
                                information_label.Text = "誤觸蝙蝠洞穴 遭到蝙蝠襲擊..." + "\n\n" + "減速 1 回合\n快樂值 - 1200";
                                pictureBox1.Load("被蝙蝠襲擊.png");
                            }
                        }
                    }
                    else
                    if (map[players[now_player].BlockIndex] is NoLuckyBlock)
                    {
                        if( now_player == 0)
                        {
                            evenrand = new Random();
                            r = evenrand.Next(4) + 1;

                            if(r == 1)
                            {
                                players[now_player].Money = players[now_player].Money - 300;
                                information_label.Text = "想在河中尋寶 但是卻空手而回..." + "\n\n" + "快樂值 - 300";
                                pictureBox1.Load("尋寶失敗.png");
                            }
                            if(r == 2)
                            {
                                players[now_player].Money = players[now_player].Money - 800;
                                information_label.Text = "因不慎落水 且不適水性 所以溺水了..." + "\n\n" + "快樂值 - 800";
                                pictureBox1.Load("溺水.png");
                            }
                            if(r == 3)
                            {
                                players[now_player].Money = players[now_player].Money - 1500;
                                information_label.Text = "與朋友發生爭執 一言不和..." + "\n\n" + "快樂值 - 1500";
                                pictureBox1.Load("打架.png");
                            }
                            if(r == 4)
                            {
                                players[now_player].Money = players[now_player].Money - 1500;
                                information_label.Text = "不幸從馬背上落下 跌得四腳朝天..." + "\n\n" + "快樂值 - 1500";
                                pictureBox1.Load("(湯姆)試騎馬時落馬.png");
                            }
                        }
                        if( now_player == 1)
                        {
                            evenrand = new Random();
                            r = evenrand.Next(4) + 1;

                            if (r == 1)
                            {
                                players[now_player].Money = players[now_player].Money - 300;
                                information_label.Text = "想在河中尋寶 但是卻空手而回..." + "\n\n" + "快樂值 - 300";
                                pictureBox1.Load("尋寶失敗.png");
                            }
                            if (r == 2)
                            {
                                players[now_player].Money = players[now_player].Money - 800;
                                information_label.Text = "因不慎落水 且不適水性 所以溺水了..." + "\n\n" + "快樂值 - 800";
                                pictureBox1.Load("溺水.png");
                            }
                            if (r == 3)
                            {
                                players[now_player].Money = players[now_player].Money - 1500;
                                information_label.Text = "與朋友發生爭執 一言不和..." + "\n\n" + "快樂值 - 1500";
                                pictureBox1.Load("打架.png");
                            }
                            if (r == 4)
                            {
                                players[now_player].Money = players[now_player].Money - 1500;
                                information_label.Text = "偷採農夫叔叔的水果 被當場抓個正著..." + "\n\n" + "快樂值 - 1000";
                                pictureBox1.Load("做壞事被抓(哈克).png");
                            }
                        }
                        if( now_player == 2)
                        {
                            evenrand = new Random();
                            r = evenrand.Next(2) + 1;

                            if (r == 1)
                            {
                                players[now_player].Money = players[now_player].Money - 300;
                                information_label.Text = "想在河中尋寶 但是卻空手而回..." + "\n\n" + "快樂值 - 300";
                                pictureBox1.Load("尋寶失敗.png");
                            }
                            if (r == 2)
                            {
                                players[now_player].Money = players[now_player].Money - 800;
                                information_label.Text = "因不慎落水 且不適水性 所以溺水了..." + "\n\n" + "快樂值 - 800";
                                pictureBox1.Load("溺水.png");
                            }
                        }
                        if( now_player == 3)
                        {
                            evenrand = new Random();
                            r = evenrand.Next(3) + 1;

                            if (r == 1)
                            {
                                players[now_player].Money = players[now_player].Money - 300;
                                information_label.Text = "想在河中尋寶 但是卻空手而回..." + "\n\n" + "快樂值 - 300";
                                pictureBox1.Load("尋寶失敗.png");
                            }
                            if (r == 2)
                            {
                                players[now_player].Money = players[now_player].Money - 800;
                                information_label.Text = "因不慎落水 且不適水性 所以溺水了..." + "\n\n" + "快樂值 - 800";
                                pictureBox1.Load("溺水.png");
                            }
                            if (r == 3)
                            {
                                players[now_player].Money = players[now_player].Money - 1500;
                                information_label.Text = "與朋友發生爭執 一言不和..." + "\n\n" + "快樂值 - 1500";
                                pictureBox1.Load("打架.png");
                            }
                        }
                    }
                    else
                    if (map[players[now_player].BlockIndex] is LuckyBlock)
                    {
                        if(now_player == 0)
                        {
                            evenrand = new Random();
                            r = evenrand.Next(6) + 1;

                            if (r == 1)
                            {
                                players[now_player].Money = players[now_player].Money + 3000;
                                information_label.Text = "發現大量黃金 一定是我平常做了很多好事!!!!!" + "\n\n" + "快樂值 + 3000";
                                pictureBox1.Load("發現黃金.png");
                            }
                            if (r == 2)
                            {
                                players[now_player].Money = players[now_player].Money + 300;
                                information_label.Text = "得到新的棒球 真是太幸運了~~" + "\n\n" + "快樂值 + 300";
                                pictureBox1.Load("發現全新棒球.png");
                            }
                            if (r == 3)
                            {
                                players[now_player].Money = players[now_player].Money + 600;
                                information_label.Text = "參加青蛙跳遠比賽 獲得冠軍!!!" + "\n\n" + "快樂值 + 600";
                                pictureBox1.Load("青蛙跳遠比賽.png");
                            }
                            if (r == 4)
                            {
                                players[now_player].Money = players[now_player].Money + 1200;
                                information_label.Text = "獲得觀看戲劇公演的機會 是一件幸福的事。" + "\n\n" + "快樂值 + 1200";
                                pictureBox1.Load("看公演.png");
                            }
                            if (r == 5)
                            {
                                players[now_player].Money = players[now_player].Money + 1000;
                                information_label.Text = "在小鎮中開心地玩耍 散播歡樂散播愛 是我們小孩子的職責。" + "\n\n" + "快樂值 + 1000";
                                pictureBox1.Load("在小鎮玩耍.png");
                            }
                            if (r == 6)
                            {
                                players[now_player].Money = players[now_player].Money + 2000;
                                information_label.Text = "與可愛的佩琪野餐是我生命中最幸福的一件事。" + "\n\n" + "快樂值 + 2000";
                                pictureBox1.Load("野餐(湯姆佩琪).png");
                            }
                        }
                        if(now_player == 1)
                        {
                            evenrand = new Random();
                            r = evenrand.Next(5) + 1;

                            if (r == 1)
                            {
                                players[now_player].Money = players[now_player].Money + 3000;
                                information_label.Text = "發現大量黃金 一定是我平常做了很多好事!!!!!" + "\n\n" + "快樂值 + 3000";
                                pictureBox1.Load("發現黃金.png");
                            }
                            if (r == 2)
                            {
                                players[now_player].Money = players[now_player].Money + 300;
                                information_label.Text = "得到新的棒球 真是太幸運了~~" + "\n\n" + "快樂值 + 300";
                                pictureBox1.Load("發現全新棒球.png");
                            }
                            if (r == 3)
                            {
                                players[now_player].Money = players[now_player].Money + 600;
                                information_label.Text = "參加青蛙跳遠比賽 獲得冠軍!!!" + "\n\n" + "快樂值 + 600";
                                pictureBox1.Load("青蛙跳遠比賽.png");
                            }
                            if (r == 4)
                            {
                                players[now_player].Money = players[now_player].Money + 1200;
                                information_label.Text = "獲得觀看戲劇公演的機會 是一件幸福的事。" + "\n\n" + "快樂值 + 1200";
                                pictureBox1.Load("看公演.png");
                            }
                            if (r == 5)
                            {
                                players[now_player].Money = players[now_player].Money + 1000;
                                information_label.Text = "在小鎮中開心地玩耍 散播歡樂散播愛 是我們小孩子的職責。" + "\n\n" + "快樂值 + 1000";
                                pictureBox1.Load("在小鎮玩耍.png");
                            }
                        }
                        if (now_player == 2)
                        {
                            evenrand = new Random();
                            r = evenrand.Next(6) + 1;

                            if (r == 1)
                            {
                                players[now_player].Money = players[now_player].Money + 3000;
                                information_label.Text = "發現大量黃金 一定是我平常做了很多好事!!!!!" + "\n\n" + "快樂值 + 3000";
                                pictureBox1.Load("發現黃金.png");
                            }
                            if (r == 2)
                            {
                                players[now_player].Money = players[now_player].Money + 300;
                                information_label.Text = "得到新的棒球 真是太幸運了~~" + "\n\n" + "快樂值 + 300";
                                pictureBox1.Load("發現全新棒球.png");
                            }
                            if (r == 3)
                            {
                                players[now_player].Money = players[now_player].Money + 600;
                                information_label.Text = "參加青蛙跳遠比賽 獲得冠軍!!!" + "\n\n" + "快樂值 + 600";
                                pictureBox1.Load("青蛙跳遠比賽.png");
                            }
                            if (r == 4)
                            {
                                players[now_player].Money = players[now_player].Money + 1200;
                                information_label.Text = "獲得觀看戲劇公演的機會 是一件幸福的事。" + "\n\n" + "快樂值 + 1200";
                                pictureBox1.Load("看公演.png");
                            }
                            if (r == 5)
                            {
                                players[now_player].Money = players[now_player].Money + 1000;
                                information_label.Text = "在小鎮中開心地玩耍 散播歡樂散播愛 是我們小孩子的職責。" + "\n\n" + "快樂值 + 1000";
                                pictureBox1.Load("在小鎮玩耍.png");
                            }
                            if (r == 6)
                            {
                                players[now_player].Money = players[now_player].Money + 2000;
                                information_label.Text = "與帥氣的湯姆野餐是我生命中最幸福的一件事。" + "\n\n" + "快樂值 + 2000";
                                pictureBox1.Load("野餐(湯姆佩琪).png");
                            }
                        }
                        if (now_player == 3)
                        {
                            evenrand = new Random();
                            r = evenrand.Next(5) + 1;

                            if (r == 1)
                            {
                                players[now_player].Money = players[now_player].Money + 3000;
                                information_label.Text = "發現大量黃金 一定是我平常做了很多好事!!!!!" + "\n\n" + "快樂值 + 3000";
                                pictureBox1.Load("發現黃金.png");
                            }
                            if (r == 2)
                            {
                                players[now_player].Money = players[now_player].Money + 300;
                                information_label.Text = "得到新的棒球 真是太幸運了~~" + "\n\n" + "快樂值 + 300";
                                pictureBox1.Load("發現全新棒球.png");
                            }
                            if (r == 3)
                            {
                                players[now_player].Money = players[now_player].Money + 600;
                                information_label.Text = "參加青蛙跳遠比賽 獲得冠軍!!!" + "\n\n" + "快樂值 + 600";
                                pictureBox1.Load("青蛙跳遠比賽.png");
                            }
                            if (r == 4)
                            {
                                players[now_player].Money = players[now_player].Money + 1200;
                                information_label.Text = "獲得觀看戲劇公演的機會 是一件幸福的事。" + "\n\n" + "快樂值 + 1200";
                                pictureBox1.Load("看公演.png");
                            }
                            if (r == 5)
                            {
                                players[now_player].Money = players[now_player].Money + 1000;
                                information_label.Text = "在小鎮中開心地玩耍 散播歡樂散播愛 是我們小孩子的職責。" + "\n\n" + "快樂值 + 1000";
                                pictureBox1.Load("在小鎮玩耍.png");
                            }
                        }
                    }
                    else
                    if (map[players[now_player].BlockIndex] is StartBlock)
                    {
                        information_label.Text = "回到最初的地方 這不會是結束 因為我們還沒長大。" + "\n\n" + "加速 1 回合\n   或\n快樂值 + 1000";
                        pictureBox1.Load("小鎮.png");
                    }
                    else
                    if (map[players[now_player].BlockIndex] is TomHouseBlock)
                    {
                        if(now_player == 0)
                        {
                            evenrand = new Random();
                            r = evenrand.Next(2) + 1;
                            //label34.Text = r.ToString();
                            if (r == 1)
                            {
                                players[now_player].State = PlayerState.Stop;
                                players[now_player].Money = players[now_player].Money - 1000;
                                information_label.Text = "今天真倒霉" + "\n嬸嬸命令我去刷油漆..." + "\n\n" + "囚禁 1 回合\n快樂值 - 1000";
                                pictureBox1.Load("油漆(湯姆).png");
                            }
                            else if(r == 2)
                            {
                                players[now_player].Money = players[now_player].Money - 1000;
                                information_label.Text = "嬸嬸不讓我回家" + "\n誰叫我是壞孩子..." + "\n\n" + "快樂值 - 1000";
                                pictureBox1.Load("祈禱.png");
                            }
                        }
                        if(now_player == 1)
                        {
                            players[now_player].Money = players[now_player].Money + 3000;
                            information_label.Text = "我最好朋友陪我一起穿西裝" + "這是很難得的機會。" + "\n\n" + "快樂值 + 3000";
                            pictureBox1.Load("打領結的哈克.png");
                        }
                        if(now_player == 2)
                        {
                            players[now_player].Money = players[now_player].Money + 3000;
                            information_label.Text = "獲得湯姆送給我的花束" + "\n看來今天是個幸運的一天。" + "\n\n" + "快樂值 + 3000";
                            pictureBox1.Load("(佩琪)獲得哈克的花.png");
                        }
                        if(now_player == 3)
                        {
                            evenrand = new Random();
                            r = evenrand.Next(4) + 1;
                            //label34.Text = r.ToString();
                            if(r == 1)
                            {
                                players[now_player].Money = players[now_player].Money - 1000;
                                information_label.Text = "真不走運晚上又作惡夢了..." + "\n\n" + "快樂值 - 1000";
                                pictureBox1.Load("(席德)做惡夢.png");
                            }
                            if (r == 2)
                            {
                                players[now_player].State = PlayerState.Stop;
                                players[now_player].Money = players[now_player].Money - 1000;
                                information_label.Text = "今天真倒霉" + "\n嬸嬸命令我去洗衣服..." + "\n\n" + "囚禁 1 回合\n快樂值 - 1000";
                                pictureBox1.Load("(席德)被媽媽叫去曬衣服.png");
                            }
                            if(r == 3)
                            {
                                players[now_player].Money = players[now_player].Money + 1500;
                                information_label.Text = "今天哥哥不在家 終於可以開心的看漫畫了~~" + "\n\n" + "快樂值 + 1500";
                                pictureBox1.Load("讀書(席德).png");
                            }
                            if(r == 4)
                            {
                                players[now_player].Money = players[now_player].Money + 500;
                                information_label.Text = "今天哥哥不在家 終於可以安靜唸書了。" + "\n\n" + "快樂值 + 500";
                                pictureBox1.Load("(席德)看書.png");
                            }
                        }
                       // else
                       // {
                       //     information_label.Text = "歡迎來到 湯姆 的家" + "\n\n" + "正常";
                       // }
                    }
                    else
                    if (map[players[now_player].BlockIndex] is SchoolBlock)
                    {
                        if (now_player == 0)
                        {
                            evenrand = new Random();
                            r = evenrand.Next(3) + 1;

                            if (r == 1)
                            {
                                players[now_player].State = PlayerState.Stop;
                                information_label.Text = "這次考試考不好 被老師留下來補習..." + "\n\n" + "囚禁 1 回合";
                                pictureBox1.Load("愛校服務.png");
                            }
                            if (r == 2)
                            {
                                players[now_player].Money = players[now_player].Money - 500;
                                information_label.Text = "上課上了一整天 讓人覺得無聊..." + "\n\n" + "快樂值 - 500";
                                pictureBox1.Load("上課一整天.png");
                            }
                            if (r == 3)
                            {
                                players[now_player].Money = players[now_player].Money - 1000;
                                information_label.Text = "做錯事被老師處罰..." + "\n\n" + "快樂值 - 1000";
                                pictureBox1.Load("做錯事被老師處罰.png");
                            }
                        }
                        if ( now_player == 1)
                        {
                            players[now_player].State = PlayerState.SpeedUp;
                            players[now_player].Money = players[now_player].Money + 600;
                            information_label.Text = "發現老師的真面目!" + "\n\n" + "加速 1 回合\n快樂值 + 600";
                            pictureBox1.Load("發現老師的真面目.png");
                        }
                        if ( now_player == 2)
                        {
                            players[now_player].State = PlayerState.Stop;
                            information_label.Text = "這次考試考不好 被老師留下來補習..." + "\n\n" + "囚禁 1 回合";
                            pictureBox1.Load("愛校服務.png");
                        }
                        if ( now_player == 3)
                        {
                            evenrand = new Random();
                            r = evenrand.Next(2) + 1;
                            if(r == 1)
                            {
                                players[now_player].State = PlayerState.Stop;
                                information_label.Text = "這次考試考不好 被老師留下來補習..." + "\n\n" + "囚禁 1 回合";
                                pictureBox1.Load("愛校服務.png");
                            }
                            if(r == 2)
                            {
                                players[now_player].State = PlayerState.DiceAgain;
                                players[now_player].Money = players[now_player].Money + 1000;
                                information_label.Text = "正義感使然 必須出面介入同學之間的爭執。" + "\n\n" + "再骰 1 次\n快樂值 + 1000";
                                pictureBox1.Load("伸張正義.png");
                            }
                        }
                       // else
                       // {
                       //     information_label.Text = "好孩子 今天的學習結束囉 可以回家了" + "\n\n" + "正常";
                       // }
                    }
                    else
                    if (map[players[now_player].BlockIndex] is HackTreeHouseBlock)
                    {
                        if (now_player == 0)
                        {
                            evenrand = new Random();
                            r = evenrand.Next(3) + 1;

                            if (r == 1)
                            {
                                players[now_player].State = PlayerState.Stop;
                                information_label.Text = "是時候該好好的修補這樹屋了" + "\n\n" + "囚禁 1 回合";
                                pictureBox1.Load("(湯姆,哈克)修理屋子.png");
                            }
                            if (r == 2)
                            {
                                players[now_player].State = PlayerState.SpeedUp;
                                players[now_player].Money = players[now_player].Money - 500;
                                information_label.Text = "唉呦 本來要去樹屋玩 卻被山豬追著跑..." + "\n\n" + "加速 1 回合\n快樂值 - 500";
                                pictureBox1.Load("被山豬追.png");
                            }
                            if (r == 3)
                            {
                                players[now_player].Money = players[now_player].Money + 1000;
                                information_label.Text = "哈克生病了 作為朋友能幫助他 真是太好了。" + "\n\n" + "快樂值 + 1000";
                                pictureBox1.Load("生病(哈克)照顧(湯姆).png");
                            }
                        }
                        if (now_player == 1)
                        {
                            evenrand = new Random();
                            r = evenrand.Next(3) + 1;

                            if (r == 1)
                            {
                                players[now_player].State = PlayerState.Stop;
                                information_label.Text = "是時候該好好的修補這樹屋了..." + "\n\n" + "囚禁 1 回合";
                                pictureBox1.Load("(湯姆,哈克)修理屋子.png");
                            }
                            if (r == 2)
                            {
                                players[now_player].Money = players[now_player].Money - 1000;
                                information_label.Text = "樹屋部分倒榻 修建過程並不順利..." + "\n\n" + "快樂值 - 1000";
                                pictureBox1.Load("蓋樹屋倒塌.png");
                            }
                            if (r == 3)
                            {
                                players[now_player].State = PlayerState.SpeedDown;
                                players[now_player].Money = players[now_player].Money + 500;
                                information_label.Text = "我生病了 但是有好朋友湯姆的照顧讓我很開心。" + "\n\n" + "快樂值 + 500";
                                pictureBox1.Load("生病(哈克)照顧(湯姆).png");
                            }
                        }
                        if (now_player == 2)
                        {
                            players[now_player].State = PlayerState.SpeedUp;
                            players[now_player].Money = players[now_player].Money - 500;
                            information_label.Text = "唉呦 本來要去樹屋玩 卻被山豬追著跑..." + "\n\n" + "加速 1 回合\n快樂值 - 500";
                            pictureBox1.Load("被山豬追.png");
                        }
                        if (now_player == 3)
                        {
                            players[now_player].State = PlayerState.SpeedUp;
                            players[now_player].Money = players[now_player].Money - 500;
                            information_label.Text = "唉呦 本來要去樹屋玩 卻被山豬追著跑..." + "\n\n" + "加速 1 回合\n快樂值 - 500";
                            pictureBox1.Load("被山豬追.png");
                        }
                       // else
                       // {
                       //     information_label.Text = "今天來到哈克的樹屋玩 真開心" + "\n\n" + "正常";
                       // }
                    }
                    else
                    if (map[players[now_player].BlockIndex] is BuyStoreBlock)
                    {
                        MessageBox.Show("歡迎來到 購買商店");
                        reloaditem();
                        button_item1_buy.Enabled = true;
                        button11.Enabled = true;
                        button12.Enabled = true;

                        pictureBox6.Load("購買商店.png");
                        pictureBox1.Load("商店開啟.png");
                        label44.Text = "歡迎光臨\n這裡是購物商店\n不可以販賣道具";

                        label45.Text = "$500";
                        label46.Text = "$1000";
                        label47.Text = "$2000";

                        button_item1_buy.Text = "購買";
                        button11.Text = "購買";
                        button12.Text = "購買";

                        panel8.Enabled = true;

                    }
                    else
                    if (map[players[now_player].BlockIndex] is ChurchBlock)
                    {
                        evenrand = new Random();
                        r = evenrand.Next(2) + 1;

                        if (now_player == 0)
                        {
                            if(r == 1)
                            {
                                players[now_player].Money = players[now_player].Money + 2000;
                                information_label.Text = "今天很高興能夠受邀參加結婚典禮~~" + "\n\n" + "快樂值 + 2000";
                                pictureBox1.Load("參加婚禮.png");
                            }
                            if(r == 2)
                            {
                                players[now_player].State = PlayerState.Stop;
                                information_label.Text = "今日又是做禮拜的日子 嬸嬸一定要我去..." + "\n\n" + "囚禁 1 回合";
                                pictureBox1.Load("做禮拜.png");
                            }
                        }
                        if (now_player == 1)
                        {
                            if (r == 1)
                            {
                                players[now_player].Money = players[now_player].Money + 2000;
                                information_label.Text = "今天很高興能夠受邀參加結婚典禮~~" + "\n\n" + "快樂值 + 2000";
                                pictureBox1.Load("參加婚禮.png");
                            }
                            if (r == 2)
                            {
                                players[now_player].Money = players[now_player].Money - 500;
                                information_label.Text = "今日是爸爸的忌日 雖然我對爸爸沒什麼映像..." + "\n\n" + "快樂值 - 500";
                                pictureBox1.Load("哈克爸爸忌日.png");
                            }
                        }
                        if (now_player == 2)
                        {
                            if (r == 1)
                            {
                                players[now_player].Money = players[now_player].Money + 2000;
                                information_label.Text = "今天很高興能夠受邀參加結婚典禮~~" + "\n\n" + "快樂值 + 2000";
                                pictureBox1.Load("參加婚禮.png");
                            }
                            if (r == 2)
                            {
                                players[now_player].Money = players[now_player].Money + 600;
                                information_label.Text = "今日又是做禮拜的日子 我還滿喜歡這類型的活動。" + "\n\n" + "快樂值 + 600";
                                pictureBox1.Load("做禮拜.png");
                            }
                        }
                        if (now_player == 3)
                        {
                            if (r == 1)
                            {
                                players[now_player].Money = players[now_player].Money + 2000;
                                information_label.Text = "今天很高興能夠受邀參加結婚典禮~~" + "\n\n" + "快樂值 + 2000";
                                pictureBox1.Load("參加婚禮.png");
                            }
                            if (r == 2)
                            {
                                players[now_player].State = PlayerState.Stop;
                                information_label.Text = "今日又是做禮拜的日子 嬸嬸一定要我去..." + "\n\n" + "囚禁 1 回合";
                                pictureBox1.Load("做禮拜.png");
                            }
                        }
                       // else
                       // information_label.Text = "禮拜天 是去教堂禮拜的時間" + "\n\n" + "被囚禁 1 回合";
                    }
                    if (map[players[now_player].BlockIndex] is PeikyHouseBlock)
                    {
                        if (now_player == 0)
                        {
                            evenrand = new Random();
                            r = evenrand.Next(3) + 1;

                            if (r == 1)
                            {
                                players[now_player].State = PlayerState.SpeedUp;
                                players[now_player].Money = players[now_player].Money - 1000;
                                information_label.Text = "佩琪家養的狗 又開始追我了..." + "\n\n" + "加速 1 回合\n快樂值 - 1000";
                                pictureBox1.Load("被狗追.png");
                            }
                            if (r == 2)
                            {
                                players[now_player].Money = players[now_player].Money + 3000;
                                information_label.Text = "獲得佩琪送給我的花" + "\n看來今天是個幸運的一天。" + "\n\n" + "快樂值 + 3000";
                                pictureBox1.Load("佩琪送的花(湯姆).png");
                            }
                            if (r == 3)
                            {
                                players[now_player].Money = players[now_player].Money + 1500;
                                information_label.Text = "今天很高興受邀到珮琪家裡玩" + "\n看來今天是個幸運的一天。" + "\n\n" + "快樂值 + 1500";
                                pictureBox1.Load("玩得開心(佩琪).png");
                            }
                        }
                        if (now_player == 1)
                        {
                            evenrand = new Random();
                            r = evenrand.Next(2) + 1;

                            if (r == 1)
                            {
                                players[now_player].State = PlayerState.SpeedUp;
                                players[now_player].Money = players[now_player].Money - 1000;
                                information_label.Text = "佩琪家養的狗 又開始追我了..." + "\n\n" + "加速 1 回合\n快樂值 - 1000";
                                pictureBox1.Load("被狗追.png");
                            }
                            if (r == 2)
                            {
                                players[now_player].Money = players[now_player].Money + 1500;
                                information_label.Text = "今天很高興受邀到珮琪家裡玩" + "\n看來今天是個幸運的一天。" + "\n\n" + "快樂值 + 1500";
                                pictureBox1.Load("玩得開心(佩琪).png");
                            }
                        }
                        if (now_player == 2)
                        {
                            players[now_player].State = PlayerState.SpeedUp;
                            players[now_player].Money = players[now_player].Money + 1500;
                            information_label.Text = "在外面玩太久 偶爾回到家中休息也是一種幸福。" + "\n\n" + "加速 1 回合\n快樂值 + 1500";
                            pictureBox1.Load("(佩琪)在家休息.png");
                        }
                        if (now_player == 3)
                        {
                            evenrand = new Random();
                            r = evenrand.Next(2) + 1;

                            if (r == 1)
                            {
                                players[now_player].State = PlayerState.SpeedUp;
                                players[now_player].Money = players[now_player].Money - 1000;
                                information_label.Text = "佩琪家養的狗 又開始追我了..." + "\n\n" + "加速 1 回合\n快樂值 - 1000";
                                pictureBox1.Load("被狗追.png");
                            }
                            if (r == 2)
                            {
                                players[now_player].Money = players[now_player].Money + 1500;
                                information_label.Text = "今天很高興受邀到珮琪家裡玩" + "\n看來今天是個幸運的一天。" + "\n\n" + "快樂值 + 1500";
                                pictureBox1.Load("玩得開心(佩琪).png");
                            }
                        }
                       // else
                       // {
                       //     players[now_player].State = PlayerState.SpeedUp;
                       //     information_label.Text = "佩琪家養的狗 又開始追我了" + "\n\n" + "加速 1 回合";
                       // }
                    }
                    else
                    if (map[players[now_player].BlockIndex] is DockBlock)
                    {
                        if (now_player == 0)
                        {
                            evenrand = new Random();
                            r = evenrand.Next(6) + 1;

                            if(r == 1)
                            {
                                players[now_player].Money = players[now_player].Money + 2000;
                                information_label.Text = "今天剛好有遊輪靠岸 而且還開放參觀" + "\n今天真是個幸運的一天~~" + "\n\n" + "快樂值 + 2000";
                                pictureBox1.Load("參觀碼頭.png");
                            }
                            if(r == 2)
                            {
                                players[now_player].Money = players[now_player].Money - 1000;
                                information_label.Text = "今天在碼頭遇到騙小孩的叔叔" + "\n害我被騙了一些零用錢" + "\n\n" + "快樂值 - 1000";
                                pictureBox1.Load("被賣假藥騙.png");
                            }
                            if(r == 3)
                            {
                                players[now_player].Money = players[now_player].Money - 2000;
                                information_label.Text = "今天約了我最討厭的人在碼頭一決高下" + "\n搞得兩敗俱傷..." + "\n\n" + "快樂值 - 2000";
                                pictureBox1.Load("(湯姆)與情敵打架.png");
                            }
                            if(r == 4)
                            {
                                players[now_player].State = PlayerState.DiceAgain;
                                players[now_player].Money = players[now_player].Money + 3000;
                                information_label.Text = "難得有機會出去玩 獲得旅遊機會。" + "\n\n" + "再骰 1 次\n快樂值 + 3000";
                                pictureBox1.Load("旅行.png");
                            }
                            if(r == 5)
                            {
                                players[now_player].Money = players[now_player].Money - 1000;
                                information_label.Text = "與哈克一起溜上船" + "\n但是還是被抓到了..." + "\n\n" + "快樂值 - 1000";
                                pictureBox1.Load("偷渡上船被抓到.png");
                            }
                            if(r == 6)
                            {
                                players[now_player].Money = players[now_player].Money + 1500;
                                information_label.Text = "剛好今天碼頭有戲團表演" + "\n運氣真好~~" + "\n\n" + "快樂值 + 1500";
                                pictureBox1.Load("碼頭看戲.png");
                            }

                       //     information_label.Text = "父親因為工作的緣故 認識了碼頭的人 所以我可以免費參觀輪船 嘿嘿" + "\n\n" + "加速 1 回合";
                        }
                        if (now_player == 1)
                        {
                            evenrand = new Random();
                            r = evenrand.Next(4) + 1;

                            if (r == 1)
                            {
                                players[now_player].Money = players[now_player].Money + 2000;
                                information_label.Text = "今天剛好有遊輪靠岸 而且還開放參觀" + "\n今天真是個幸運的一天~~" + "\n\n" + "快樂值 + 2000";
                                pictureBox1.Load("參觀碼頭.png");
                            }
                            if (r == 2)
                            {
                                players[now_player].Money = players[now_player].Money - 1000;
                                information_label.Text = "今天在碼頭遇到騙小孩的叔叔" + "\n害我被騙了一些零用錢" + "\n\n" + "快樂值 - 1000";
                                pictureBox1.Load("被賣假藥騙.png");
                            }
                            if (r == 3)
                            {
                                players[now_player].Money = players[now_player].Money - 1000;
                                information_label.Text = "與湯姆一起溜上船" + "\n但是還是被抓到了..." + "\n\n" + "快樂值 - 1000";
                                pictureBox1.Load("偷渡上船被抓到.png");
                            }
                            if (r == 4)
                            {
                                players[now_player].Money = players[now_player].Money + 1500;
                                information_label.Text = "剛好今天碼頭有戲團表演" + "\n運氣真好~~" + "\n\n" + "快樂值 + 1500";
                                pictureBox1.Load("碼頭看戲.png");
                            }
                        }
                        if (now_player == 2)
                        {
                            evenrand = new Random();
                            r = evenrand.Next(4) + 1;

                            if (r == 1)
                            {
                                players[now_player].Money = players[now_player].Money + 2000;
                                information_label.Text = "今天剛好有遊輪靠岸 而且還開放參觀" + "\n今天真是個幸運的一天~~" + "\n\n" + "快樂值 + 2000";
                                pictureBox1.Load("參觀碼頭.png");
                            }
                            if (r == 2)
                            {
                                players[now_player].Money = players[now_player].Money - 1000;
                                information_label.Text = "今天在碼頭遇到騙小孩的叔叔" + "\n害我被騙了一些零用錢" + "\n\n" + "快樂值 - 1000";
                                pictureBox1.Load("被賣假藥騙.png");
                            }
                            if (r == 3)
                            {
                                players[now_player].State = PlayerState.DiceAgain;
                                players[now_player].Money = players[now_player].Money + 3000;
                                information_label.Text = "難得有機會出去玩 獲得旅遊機會。" + "\n\n" + "再骰 1 次\n快樂值 + 3000";
                                pictureBox1.Load("旅行.png");
                            }
                            if (r == 4)
                            {
                                players[now_player].Money = players[now_player].Money + 1500;
                                information_label.Text = "剛好今天碼頭有戲團表演" + "\n運氣真好~~" + "\n\n" + "快樂值 + 1500";
                                pictureBox1.Load("碼頭看戲.png");
                            }
                        }
                        if (now_player == 3)
                        {
                            evenrand = new Random();
                            r = evenrand.Next(3) + 1;

                            if (r == 1)
                            {
                                players[now_player].Money = players[now_player].Money + 2000;
                                information_label.Text = "今天剛好有遊輪靠岸 而且還開放參觀" + "\n今天真是個幸運的一天~~" + "\n\n" + "快樂值 + 2000";
                                pictureBox1.Load("參觀碼頭.png");
                            }
                            if (r == 2)
                            {
                                players[now_player].Money = players[now_player].Money - 1000;
                                information_label.Text = "今天在碼頭遇到騙小孩的叔叔" + "\n害我被騙了一些零用錢" + "\n\n" + "快樂值 - 1000";
                                pictureBox1.Load("被賣假藥騙.png");
                            }
                            if (r == 3)
                            {
                                players[now_player].Money = players[now_player].Money + 1500;
                                information_label.Text = "剛好今天碼頭有戲團表演" + "\n運氣真好~~" + "\n\n" + "快樂值 + 1500";
                                pictureBox1.Load("碼頭看戲.png");
                            }
                        }
                       // else
                       // {
                       //     players[now_player].Money -= 50;
                       //     information_label.Text = "雖然門票很貴 但是機會難得 我一定要上船參觀" + "\n\n" + "加速 1 回合";
                       // }
                    }
                    else
                    if (map[players[now_player].BlockIndex] is ForestBlock)
                    {
                        if (now_player == 0)
                        {
                            evenrand = new Random();
                            r = evenrand.Next(4) + 1;

                            if (r == 1)
                            {
                                players[now_player].State = PlayerState.SpeedDown;
                                players[now_player].Money = players[now_player].Money + 1000;
                                information_label.Text = "今天到樹林裡與朋友玩海盜遊戲" + "\n是個愉快又疲勞的一天~~" + "\n\n" + "減速 1 回合\n快樂值 + 1000";
                                pictureBox1.Load("樹林玩耍(湯姆).png");
                            }
                            if (r == 2)
                            {
                                players[now_player].Money = players[now_player].Money - 3000;
                                information_label.Text = "遇到恐怖的印地安喬 運氣真不好..."  + "\n\n" + "快樂值 - 3000";
                                pictureBox1.Load("在洞穴遇到殺人犯印地安橋.png");
                            }
                            if (r == 3)
                            {
                                players[now_player].Money = players[now_player].Money + 2000;
                                information_label.Text = "在樹林中玩耍 無意中發現彩虹 象徵今日的幸運。" + "\n\n" + "快樂值 + 2000";
                                pictureBox1.Load("遇見彩虹.png");
                            }
                            if (r == 4)
                            {
                                players[now_player].State = PlayerState.SpeedDown;
                                players[now_player].Money = players[now_player].Money + 500;
                                information_label.Text = "參加倒立行走比賽" + "\n是個又累又快樂的一天~~" + "\n\n" + "減速 1 回合\n快樂值 + 500";
                                pictureBox1.Load("參加倒立比賽.png");
                            }
                        }
                        if (now_player == 1)
                        {
                            evenrand = new Random();
                            r = evenrand.Next(3) + 1;

                            if (r == 1)
                            {
                                players[now_player].Money = players[now_player].Money - 3000;
                                information_label.Text = "遇到恐怖的印地安喬 運氣真不好..." + "\n\n" + "快樂值 - 3000";
                                pictureBox1.Load("在洞穴遇到殺人犯印地安橋.png");
                            }
                            if (r == 2)
                            {
                                players[now_player].Money = players[now_player].Money + 2000;
                                information_label.Text = "在樹林中玩耍 無意中發現彩虹 象徵今日的幸運。" + "\n\n" + "快樂值 + 2000";
                                pictureBox1.Load("遇見彩虹.png");
                            }
                            if (r == 3)
                            {
                                players[now_player].State = PlayerState.SpeedDown;
                                players[now_player].Money = players[now_player].Money + 500;
                                information_label.Text = "參加倒立行走比賽" + "\n是個又累又快樂的一天~~" + "\n\n" + "減速 1 回合\n快樂值 + 500";
                                pictureBox1.Load("參加倒立比賽.png");
                            }
                        }
                        if (now_player == 2)
                        {
                            evenrand = new Random();
                            r = evenrand.Next(2) + 1;

                            if (r == 1)
                            {
                                players[now_player].Money = players[now_player].Money - 3000;
                                information_label.Text = "遇到恐怖的印地安喬 運氣真不好..." + "\n\n" + "快樂值 - 3000";
                                pictureBox1.Load("在洞穴遇到殺人犯印地安橋.png");
                            }
                            if (r == 2)
                            {
                                players[now_player].Money = players[now_player].Money + 2000;
                                information_label.Text = "在樹林中玩耍 無意中發現彩虹 象徵今日的幸運。" + "\n\n" + "快樂值 + 2000";
                                pictureBox1.Load("遇見彩虹.png");
                            }
                        }
                        if (now_player == 3)
                        {
                            evenrand = new Random();
                            r = evenrand.Next(3) + 1;

                            if (r == 1)
                            {
                                players[now_player].Money = players[now_player].Money - 3000;
                                information_label.Text = "遇到恐怖的印地安喬 運氣真不好..." + "\n\n" + "快樂值 - 3000";
                                pictureBox1.Load("在洞穴遇到殺人犯印地安橋.png");
                            }
                            if (r == 2)
                            {
                                players[now_player].Money = players[now_player].Money + 2000;
                                information_label.Text = "在樹林中玩耍 無意中發現彩虹 象徵今日的幸運。" + "\n\n" + "快樂值 + 2000";
                                pictureBox1.Load("遇見彩虹.png");
                            }
                            if (r == 3)
                            {
                                players[now_player].State = PlayerState.SpeedDown;
                                players[now_player].Money = players[now_player].Money + 500;
                                information_label.Text = "參加倒立行走比賽" + "\n是個又累又快樂的一天~~" + "\n\n" + "減速 1 回合\n快樂值 + 500";
                                pictureBox1.Load("參加倒立比賽.png");
                            }
                        }
                       // information_label.Text = "再樹林中玩遊戲 實在是太累了" + "\n\n" + "減速 1 回合";
                    }
                    else
                    if (map[players[now_player].BlockIndex] is SellStoreBlock)
                    {
                        MessageBox.Show("歡迎來到 販賣商店");
                        reloaditem();
                        button_item1_buy.Enabled = true;
                        button11.Enabled = true;
                        button12.Enabled = true;

                        pictureBox6.Load("販賣商店.png");
                        pictureBox1.Load("商店開啟.png");
                        label44.Text = "歡迎光臨\n這裡是販賣商店\n你可以回收道具";

                        label45.Text = "$200";
                        label46.Text = "$500";
                        label47.Text = "$1000";

                        button_item1_buy.Text = "販賣";
                        button11.Text = "販賣";
                        button12.Text = "販賣";

                        panel8.Enabled = true;
                    }
                    else
                    if (map[players[now_player].BlockIndex] is PayMoneyBlock)
                    {
                        if (now_player == 1)
                        {
                            players[now_player].Money += 200;
                            information_label.Text = "自從爸爸死後之後 我也不必受到鎮上大人的欺負了..." + "\n\n" + "免疫 降低快樂值";
                            pictureBox1.Load("哈克特寫.png");
                        }
                        else
                        {
                            information_label.Text = "小孩子 就是會受到大人的欺負 希望長大後我不會成為這樣的人..." + "\n\n" + "快樂值 - 200";
                            pictureBox1.Load("付費.png");
                        }
                    }
                    

                    Label_Money.Text = player.Money.ToString();
                    SetUI(TurnPhase.End);
                }
                else
                {
                    map[player.BlockIndex].PassAction(ref player);
                    Label_Money.Text = player.Money.ToString();
                }
            }
        }

        private void PictureBox_Map_Paint(object sender, PaintEventArgs e)
        {
           // Pen black = new Pen(Color.Black, 10);
           // Graphics g = e.Graphics;
           // g.DrawEllipse(black, new Rectangle(312,46, 10, 10));
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
           // Pen black = new Pen(Color.Black, 9);
           // Graphics g = e.Graphics;
           // g.DrawEllipse(black, new Rectangle(325, 0, 10, 10));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (d_status == State.Rotating || d_status == State.Slowing)
            {
                d_count += d_speed;

                if (d_count >= DiceChange)
                {
                    d_count -= DiceChange;
                    int temp = rand.Next(6);
                    while (d_place == temp)
                    {
                        temp = rand.Next(6);
                    }
                    d_place = temp;
                }

                if (d_status == State.Slowing)
                {
                    d_speed -= rand.NextDouble() * 0.99;
                }

                if (d_speed <= 0)
                {
                    d_status = State.Stop;
                }

                pictureBox_dice.Image = d[d_place];
                pictureBox13.Image = d[d_place];
                pictureBox_dice.Refresh();
                pictureBox13.Refresh();
            }
            else if (d_status == State.Stop)
            {
                d_status = State.NonUse;
                label_dice.Text = (d_place + 1).ToString();
                label49.Text = (d_place + 1).ToString();
                step = Convert.ToInt32(label_dice.Text);
                step = Convert.ToInt32(label49.Text);
                SetUI(TurnPhase.Walk);
                button_start.Enabled = true;
                button21.Enabled = true;
            }
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            if (button_start.Text == "Start" && button21.Text == "Start")
            {
                d_speed = DiceSpeed;
                d_count = 0;
                d_place = 0;
                d_status = State.Rotating;

                button_start.Text = "Stop";
                button21.Text = "Stop";
            }
            else
            {
                d_status = State.Slowing;

                button_start.Text = "Start";
                button21.Text = "Start";
                button_start.Enabled = false;
                button21.Enabled = false;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            step = Convert.ToInt32(textBox1.Text);
            SetUI(TurnPhase.Walk);
            textBox1.Clear();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (map[players[now_player].BlockIndex] is BuyStoreBlock)
            {
                players[now_player].Money = players[now_player].Money - 500;
                players[now_player].Item1++;
                reload();
                reloaditem();
                label44.Text = "謝謝惠顧\n一次只能購買\n一項商品喔。";
                button_item1_buy.Enabled = false;
                button11.Enabled = false;
                button12.Enabled = false;
                you_cant_use_item();
            }
            else if (map[players[now_player].BlockIndex] is SellStoreBlock)
            {
                if (players[now_player].Item1 > 0)
                {
                    players[now_player].Money = players[now_player].Money + 200;
                    players[now_player].Item1--;
                    reload();
                    reloaditem();
                    label44.Text = "這是你應得的。\n快樂值 + 200";
                }
                else
                {
                    button_item1_buy.Enabled = false;
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (players[now_player].Item1 > 0)
            {
                players[now_player].State = PlayerState.SpeedUp;
                players[now_player].Item1--;
                reload();
                reloaditem();
                button_item1_buy.Enabled = false;
                button15.Enabled = false;
                button14.Enabled = false;
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            max_round = Convert.ToInt16(comboBox1.Text);
            label34.Text = max_round.ToString();
            panel11.Enabled = true;
            panel11.Visible = true;
            panel10.Enabled = false;
            panel10.Visible = false;
            first = false;
            //label34.Text = max_round.ToString();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (map[players[now_player].BlockIndex] is BuyStoreBlock)
            {
                players[now_player].Money = players[now_player].Money - 1000;
                players[now_player].Item2++;
                reload();
                reloaditem();
                label44.Text = "謝謝惠顧\n一次只能購買\n一項商品喔。";
                button_item1_buy.Enabled = false;
                button11.Enabled = false;
                button12.Enabled = false;
                you_cant_use_item();
            }
            else if (map[players[now_player].BlockIndex] is SellStoreBlock)
            {
                if (players[now_player].Item2 > 0)
                {
                    players[now_player].Money = players[now_player].Money + 500;
                    players[now_player].Item2--;
                    reload();
                    reloaditem();
                    label44.Text = "這是你應得的。\n快樂值 + 500";
                }
                else
                {
                    button11.Enabled = false;
                }
            }
        }

        private void reloaditem()
        {
            label33.Text = players[now_player].Item1.ToString();
            label39.Text = players[now_player].Item2.ToString();
            label40.Text = players[now_player].Item3.ToString();


            if (map[players[now_player].BlockIndex] is SellStoreBlock)
            {
                if (players[now_player].Item1 <= 0) button_item1_buy.Enabled = false;
                if (players[now_player].Item2 <= 0) button11.Enabled = false;
                if (players[now_player].Item3 <= 0) button12.Enabled = false;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (map[players[now_player].BlockIndex] is BuyStoreBlock)
            {
                players[now_player].Money = players[now_player].Money - 2000;
                players[now_player].Item3++;
                reload();
                reloaditem();
                label44.Text = "謝謝惠顧\n一次只能購買\n一項商品喔。";
                button_item1_buy.Enabled = false;
                button11.Enabled = false;
                button12.Enabled = false;
                you_cant_use_item();
            }
            else if (map[players[now_player].BlockIndex] is SellStoreBlock)
            {
                if (players[now_player].Item3 > 0)
                {
                    players[now_player].Money = players[now_player].Money + 1000;
                    players[now_player].Item3--;
                    reload();
                    reloaditem();
                    label44.Text = "這是你應得的。\n快樂值 + 1000";
                }
                else
                {
                    button12.Enabled = false;
                }
            }
        }

        private void leave_store()
        {
            pictureBox6.Load("商店關閉1.png");
            label44.Text = "";
            panel8.Enabled = false;

            label45.Text = "價格";
            label46.Text = "價格";
            label47.Text = "價格";

            button_item1_buy.Text = "按鈕";
            button11.Text = "按鈕";
            button12.Text = "按鈕";
        }

        private void use_item_check()
        {
            if (players[now_player].Item1 <= 0)
            {
                button10.Enabled = false;
            }
            else
            {
                button10.Enabled = true;
            }
            if (players[now_player].Item2 <= 0)
            {
                button14.Enabled = false;
            }
            else
            {
                button14.Enabled = true;
            }
            if (players[now_player].Item3 <= 0)
            {
                button15.Enabled = false;
            }
            else
            {
                if (players[now_player].State == PlayerState.SpeedDown || players[now_player].State == PlayerState.Stop)
                {
                    button15.Enabled = true;
                }
                else
                {
                    button15.Enabled = false;
                }
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (players[now_player].Item2 > 0)
            {
                players[now_player].State = PlayerState.DiceAgain;
                players[now_player].Item2--;
                reload();
                reloaditem();
                button_item1_buy.Enabled = false;
                button15.Enabled = false;
                button14.Enabled = false;
                SetUI(TurnPhase.Initial);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (players[now_player].Item3 > 0)
            {
                players[now_player].State = PlayerState.Normal;
                players[now_player].Item3--;
                reload();
                reloaditem();
                button_item1_buy.Enabled = false;
                button15.Enabled = false;
                button14.Enabled = false;
                SetUI(TurnPhase.Initial);
            }
        }

        private void you_cant_use_item()
        {
            button10.Enabled = false;
            button14.Enabled = false;
            button15.Enabled = false;
        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F1)
            {
                if (systemmode == false)
                {
                    panel11.Enabled = false;
                    panel11.Visible = false;
                    panel1.Enabled = true;
                    panel1.Visible = true;
                    systemmode = true;
                }
                else
                {
                    panel11.Enabled = true;
                    panel11.Visible = true;
                    panel1.Enabled = false;
                    panel1.Visible = false;
                    systemmode = false;
                }
            }
            
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }


    }
}