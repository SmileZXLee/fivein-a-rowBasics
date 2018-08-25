using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using 五子棋AI_by照相.Properties;
using System.Threading;

namespace 五子棋AI_by照相
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        int userFlag = 1;
        int counts = 0;
        int computerTurn = 0;
        int end = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            //this.StartPosition = FormStartPosition.CenterScreen;
            Rectangle ScreenArea = System.Windows.Forms.Screen.GetWorkingArea(this);
            int area = ScreenArea.Height - 200;
            this.Size = new Size(area, area);
            area = ClientRectangle.Height;
            panel1.Size = new Size((int)(area - 150), (int)(area - 150));
            panel1.Location = new Point((this.Width - panel1.Width) / 2, (ClientRectangle.Height - panel1.Height) / 2 + 30);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Location = new Point((ScreenArea.Width - this.Width) / 2, (ScreenArea.Height - this.Height) / 2);
            panel2.Parent = panel1;
            panel2.BackColor = Color.Transparent;
            panel2.Visible = false ;
            panel2.Size = new Size(panel1.Size.Width - panel1.Size.Width / 20, panel1.Size.Width - panel1.Size.Width / 20);
            panel2.Location = new Point(panel1.Size.Width / 20 / 2, panel1.Size.Width / 20 / 2);
            this.Height += 40;
            panel1.Location = new Point(panel1.Location.X, panel1.Location.Y + 40);
            panel5.Location = new Point((this.Width - panel5.Width) / 2, 0 );

            //user.Enabled = true;
            computerTurn = 1;
            Alert a = new Alert();
            a.Show();
            a.TopMost = true;
            a.whoFirst = this.whoFirst2;
            button1.Enabled = false;
            
        }
        Graphics g = null;
        int gobalC = 0;
        Pen p = new Pen(Brushes.Blue);
        Point panel1L = new Point(0,0);
        private void drawLine(Panel pa,int count)//棋盘
        {
            if (g != null)
                g.Clear(pa.BackColor);
            g = pa.CreateGraphics();

            g.SmoothingMode = SmoothingMode.HighQuality;
            Point p1, p2;
            for (int i = 0; i < count; i++)//横线
            {
                //new Point(1.1,1.1)
                p1 = new Point(0, (pa.Height / count) * (i + 1));
                p2 = new Point(pa.Width, (pa.Height / count) * (i + 1));
                g.DrawLine(p, p1, p2);
            }
            for (int i = 0; i < count; i++)//竖线
            {
                p1 = new Point((pa.Height / count) * (i + 1), 0);
                p2 = new Point((pa.Height / count) * (i + 1), pa.Width);
                g.DrawLine(p, p1, p2);
            }
            pa.Size = new Size(count * (pa.Height / count), count * (pa.Height / count));//修正
            pa.Location = new Point((this.Width - pa.Width) / 2, (this.Height - pa.Height) / 2);
            //panel1L = pa.Location;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //drawLine(panel1,21);
           
            
        }
        List<PictureBox> cs = new List<PictureBox>();
        List<Label> ls = new List<Label>();
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            
        }
        //封装一个方法 判断这个棋子的横竖撇捺四个方向是否有n个相同棋子 其中C为B/W，n为需要判定的连续数量
        private void computer(String c,int n) {
            int w = 0;//用于判定是否符合
            //横向
            for (int i = forwordStep - 5; i < forwordStep + 5; i++)
            {
                int flag = 0;
                if (i >= 0 && i < 400)
                {
                    for (int j = i; j < i + 5; j++)
                    {
                        if (j < 400)
                        {
                            if (cs[j].Text == "W")
                            {
                                flag++;
                            }
                            else
                            {
                                flag = 0;
                            }
                            if (flag >= 5)
                            {
                                w = 1;
                            }
                        }
                    }

                }

            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            panel2.Show();
            timer1.Enabled = false;
            pictureBox1.Size = panel1.Size;
            pictureBox1.Parent = panel1;
            pictureBox1.Location = new Point(0,0);
            pictureBox1.Image = Resources.bac3;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.BringToFront();
            panel1.SendToBack();
            //panel1.BackgroundImage = Resources.bac;
            
            //panel2.BackgroundImage = Resources.stone_white;
            //添加按钮
            addAll();
            
            panel2.Parent = pictureBox1;
            panel2.BackColor = Color.Transparent;
            panel2.BringToFront();
        }
        //添加所有控件
       private void addAll() {
            for (int i = 0; i < 20 * 20; i++)
            {
                PictureBox l = new PictureBox();
                l.Tag = i;
                // l.Image = Resources.stone_black;
                l.SizeMode = PictureBoxSizeMode.Zoom;
                l.Size = new Size(panel2.Height / 20, panel2.Height / 20);
                int row, col;
                row = i / 20;
                col = i % 20;
                l.Location = new Point(col * panel2.Height / 20, row * panel2.Height / 20);
                //l.Parent = panel2;
                l.BackColor = Color.Transparent;
                //l.Font = new System.Drawing.Font("楷体", panel1.Height / 20 / 7, FontStyle.Bold);
                l.Click += clicked;
                //l.Click += l_Click;
                // allLabels.Add(l);
                cs.Add(l);
                Label ll = new Label();
                ll.Tag = i;
                
                l.Controls.Add(ll);
                ll.Size = l.Size;
                ll.TextAlign = ContentAlignment.MiddleCenter;
                ll.Hide();
                ll.Location = new Point(0,0); ;
                ll.Font = new Font("宋体",10);
                ll.ForeColor = Color.Black;
                ls.Add(ll);
                panel2.Controls.Add(l);
                
                //Maingridiron_Paint(new Object(), new PaintEventArgs());

            }
        }
        int forwordStep = -1;
        //判定输赢
        void whoWin() {
            if (cs[forwordStep].Text == "W") {
                label2.Text = "白";
                int win = 0;
                //竖列方向
                for (int i = forwordStep - 20 * 5; i < forwordStep + 20 * 5; i+=20) {
                    int flag = 0;
                    if (i >= 0 && i < 400) {
                        for (int j = i; j < i + 20 * 5; j += 20)
                        {
                            if (j < 400)
                            {
                                if (cs[j].Text == "W")
                                {
                                    flag++;
                                }
                                else
                                {
                                    flag = 0;
                                }
                               
                            }
                        }
                        if (flag >= 5)
                        {
                            win = 1;
                        }

                    }
                    
                }
                //横列方向
                for (int i = forwordStep -  5; i < forwordStep + 5; i ++)
                {
                    int flag = 0;
                    if (i >= 0 && i < 400)
                    {
                        for (int j = i; j < i +  5; j ++)
                        {
                            if (j < 400)
                            {
                                if (cs[j].Text == "W")
                                {
                                    flag++;
                                }
                                else
                                {
                                    flag = 0;
                                }
                               
                            }
                        }
                        if (flag >= 5)
                        {
                            win = 1;
                        }

                    }

                }
                //左斜
                for (int i = forwordStep - 20 * 5 - 5; i <= forwordStep; i += 21)
                {
                    int flag = 0;
                    if (i >= 0 && i < 400)
                    {
                        for (int j = i; j < i + 20 * 5+1; j += 21)
                        {
                            if (j < 400)
                            {
                                if (cs[j].Text == "W")
                                {
                                    flag++;
                                }
                                else
                                {
                                    flag = 0;
                                }
                               
                            }
                        }
                        if (flag >= 5)
                        {
                            win = 1;
                        }

                    }

                }
                //右斜
                for (int i = forwordStep - 20 * 5 + 5; i <= forwordStep; i += 19)
                {
                    int flag = 0;
                    if (i >= 0 && i < 400)
                    {
                        for (int j = i; j < i + 20 * 5 - 1; j += 19)
                        {
                            if (j < 400)
                            {
                                if (cs[j].Text == "W")
                                {
                                    flag++;
                                }
                                else
                                {
                                    flag = 0;
                                }
                                
                            }
                        }
                        if (flag >= 5)
                        {
                            win = 1;
                        }

                    }

                }
                if (win == 1)
                {


                    

                    timer2.Enabled = false;
                    user.Enabled = false;
                    com.Enabled = false;
                    panel2.Enabled = false;
                    end = 1;
                    forwordStep = -1;
                    //timer3.Enabled = true;
                   
                    MessageBox.Show("第" + counts + "手，白胜", "游戏结束", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    for (int k = 0; k < cs.Count; k++)
                    {
                        if (cs[k].Text != "")
                        {
                            ls[k].Text = ls[k].Tag.ToString();
                            ls[k].Show();
                            ls[k].ForeColor = Color.Red;
                            ls[k].BringToFront();
                        }

                    }
                }
            }
            else if (cs[forwordStep].Text == "B") {
                label2.Text = "黑";
                int win = 0;
                for (int i = forwordStep - 20 * 5; i < forwordStep + 20 * 5; i += 20)
                {
                    int flag = 0;
                    if (i >= 0 && i < 400)
                    {
                        for (int j = i; j < i + 20 * 5; j += 20)
                        {
                            if (j < 400)
                            {
                                if (cs[j].Text == "B")
                                {
                                    flag++;
                                }
                                else
                                {
                                    flag = 0;
                                }
                                
                            }
                        }
                        if (flag >= 5)
                        {
                            win = 1;
                        }

                    }

                }
                //横列方向
                for (int i = forwordStep - 5; i < forwordStep + 5; i++)
                {
                    int flag = 0;
                    if (i >= 0 && i < 400)
                    {
                        for (int j = i; j < i + 5; j++)
                        {
                            if (j < 400)
                            {
                                if (cs[j].Text == "B")
                                {
                                    flag++;
                                }
                                else
                                {
                                    flag = 0;
                                }
                               
                            }
                        }
                        if (flag >= 5)
                        {
                            win = 1;
                        }

                    }

                }
                //左斜
                for (int i = forwordStep - 20 * 5 - 5; i <= forwordStep ; i += 21)
                {
                    int flag = 0;
                    if (i >= 0 && i < 400)
                    {
                        for (int j = i; j < i + 20 * 5 + 1; j += 21)
                        {
                            if (j < 400)
                            {
                                if (cs[j].Text == "B")
                                {
                                    flag++;
                                }
                                else
                                {
                                    flag = 0;
                                }
                                
                            }
                        }
                        if (flag >= 5)
                        {
                            win = 1;
                        }

                    }

                }
                //右斜
                for (int i = forwordStep - 20 * 5 + 5; i <= forwordStep; i += 19)
                {
                    int flag = 0;
                    if (i >= 0 && i < 400)
                    {
                        for (int j = i; j < i + 20 * 5 - 1; j += 19)
                        {
                            if (j < 400)
                            {
                                if (cs[j].Text == "B")
                                {
                                    flag++;
                                }
                                else
                                {
                                    flag = 0;
                                }
                                
                            }
                           
                        }
                        if (flag >= 5)
                        {
                            win = 1;
                        }

                    }

                }
                if (win == 1)
                {
                    user.Enabled = false;
                    com.Enabled = false;
                    panel2.Enabled = false;
                    end = 1;
                    timer2.Enabled = false;
                    MessageBox.Show("第" + counts + "手，黑胜","游戏结束",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    
                    for (int k = 0; k < cs.Count; k++)
                    {
                        
                        if (cs[k].Text != "")
                        {
                            ls[k].Text = ls[k].Tag.ToString();
                            ls[k].Show();
                            ls[k].ForeColor = Color.FromArgb(1,255,139,100);
                            ls[k].BringToFront();
                        }

                    }
                    //timer4.Enabled = true;
                    
                }
            }
        }
       private void clicked(object sender, EventArgs e)
        {
            if (cleanFlag == 1) {
                MessageBox.Show("棋盘还在清理中，请稍后");
                return;
            }
            if (((PictureBox)sender).Text != "") {
                MessageBox.Show("这个地方不能下啦");
                return;
            }
            if (computerTurn == 1) {
                MessageBox.Show("现在不是你的回合！");
                return;
            }
            counts++;
            com.Enabled= true;
            user.Enabled = false;
            if (userFlag == 0)
            {
                ((PictureBox)sender).Image = Resources.black_red;
                ls[(int)((PictureBox)sender).Tag].Tag = counts;
                //((PictureBox)sender).Tag = counts;
                userFlag = 1;
                ((PictureBox)sender).Text = "B";
                if (forwordStep != -1)
                    cs[forwordStep].Image = Resources.white2;

            }
            else
            {
                ((PictureBox)sender).Image = Resources.white2_red;
                ls[(int)((PictureBox)sender).Tag].Tag = counts;
                //((PictureBox)sender).Tag = counts;
                userFlag = 0;
                ((PictureBox)sender).Text = "W";
                if (forwordStep != -1)
                {
                    cs[forwordStep].Image = Resources.black;
                }
            }
            //MessageBox.Show(((PictureBox)sender).Tag.ToString());
            forwordStep = (int)((PictureBox)sender).Tag;
            label1.Text = ((PictureBox)sender).Tag.ToString();
            whoWin();
            com.Enabled = true;
            user.Enabled = false;
            timer2.Enabled = true;
            

         }
        void computerAddCheer() {
            if (end == 1) return;
            counts++;
            int index = 0;
            int maxNum = 0;
            for (int i = 0; i < 400; i++)
            {
                if (cs[i].Text == "")
                {
                    int weight = getWeight(i);
                    if (maxNum < weight)
                    {
                        maxNum = getWeight(i);
                        index = i;
                    }
                }
            }
            cs[index].Image = Resources.black_red;
            ls[(int)cs[index].Tag].Tag = counts;
            userFlag = 1;
            cs[index].Text = "B";
            if (forwordStep != -1)
                cs[forwordStep].Image = Resources.white2;
            forwordStep = index;
            whoWin();
            computerTurn = 0;
            timer2.Enabled = false;
            com.Enabled = false;
            user.Enabled = true;
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

       
        }
        public delegate void ProcessDelegate();
        void finish() {
          
            cleanFlag = 0;
           // MessageBox.Show("wan");
        }
        int cleanFlag = 0;
        double usertime = 0;
        double comtime = 0;
        
        private void button1_Click(object sender, EventArgs e)
        {
            whoFirst2.Text = "";
            cleanFlag = 1;
            userFlag = 1;
            forwordStep = -1;
            counts = 0;
            user.Enabled = false;
            com.Enabled = false;
            panel2.Enabled = true;
            cTime.Text = "已用时0.0秒";
            uTime.Text = "已用时0.0秒";
            panel3.BackColor = Color.Transparent;
            panel4.BackColor = Color.Transparent;
            end = 0;
            usertime = 0;
            comtime = 0;
            Alert a = new Alert();
            a.Show();
            button1.Enabled = false;
            a.whoFirst = this.whoFirst2;
            begin.Enabled = true;
           
            new Thread(new ThreadStart(() =>
            {
                for (int k = 0; k < ls.Count; k++)
                {
                    ls[k].Text = ls[k].Tag.ToString();
                    ls[k].Tag = "";
                    ls[k].Hide();

                }
                for (int i = 0; i < cs.Count; i++){
                    cs[i].Image = null;
                    cs[i].Text = "";
                }
                
                ProcessDelegate showProcess = new ProcessDelegate(finish);
                label1.Invoke(showProcess);
            })).Start();
    
           }
        //调整攻守比重
       int[] weightTable = {1,200,4000,30000,80000,2,2000,15000,70000};

      //传入一个位置，获取其综合评分
       int getWeight(int index)
       {
           int weight = 0;
           //横向
           for (int i = index - 4; i <= index; i++)
           {
               if (i >= 0)
               {
                   int empty = 0, black = 0, white = 0;
                   for (int j = i; j < i + 5; j++)
                   {
                       if (j < 400)
                       {
                           if (cs[j].Text == "")
                           {
                               empty++;
                           }
                           if (cs[j].Text == "B")
                           {
                               black++;
                           }
                           if (cs[j].Text == "W")
                           {
                               white++;
                           }
                          

                       }
                   }
                   weight += countWeight(empty, white, black);
     
               }

           }
           //纵向
           for (int i = index - 20 * 4; i <= index; i += 20)
           {
               if (i >= 0)
               {
                   int empty = 0, black = 0, white = 0;
                   for (int j = i; j < i + 20 * 5; j += 20)
                   {
                       if (j < 400)
                       {
                           if (cs[j].Text == "")
                           {
                               empty++;
                           }
                           if (cs[j].Text == "B")
                           {
                               black++;
                           }
                           if (cs[j].Text == "W")
                           {
                               white++;
                           }
                           

                       }

                   }
                   weight += countWeight(empty, white, black);        
               }

           }
           //左斜
           for (int i = index - 20 * 4 - 4; i <= index; i += 21)
           {
               if (i >= 0)
               {
                   int empty = 0, black = 0, white = 0;
                   for (int j = i; j < i + 20 * 5 - 5; j += 21)
                   {
                       if (j < 400)
                       {
                           if (cs[j].Text == "")
                           {
                               empty++;
                           }
                           if (cs[j].Text == "B")
                           {
                               black++;
                           }
                           if (cs[j].Text == "W")
                           {
                               white++;
                           }

                          
                       }
                   }
                   weight += countWeight(empty, white, black);   
                 
               }

           }
           //右斜
           for (int i = index - 20 * 4 + 4; i <= index; i += 19)
           {
               if (i >= 0)
               {
                   int empty = 0, black = 0, white = 0;
                   for (int j = i; j < i + 20 * 5 - 5; j += 19)
                   {
                       if (j < 400)
                       {
                           if (cs[j].Text == "")
                           {
                               empty++;
                           }
                           if (cs[j].Text == "B")
                           {
                               black++;
                           }
                           if (cs[j].Text == "W")
                           {
                               white++;
                           }


                       }
                      
                   }
                   weight += countWeight(empty, white, black);
                       
               }
              
           }
           return weight;
       }
        
                   
       private int countWeight(int empty,int white,int black){
           int weight = 0;
           if (empty == 5)//五个都为空
           {
               weight += weightTable[0];
           }
           if (black == 1 && empty == 4)//一个黑四个空
           {
               weight += weightTable[1];
           }
           if (black == 2 && empty == 3)//两个黑三个空
           {
               weight += weightTable[2];
           }
           if (black == 3 && empty == 2)//三个黑两个空
           {
               weight += weightTable[3];
           }
           if (black == 4 && empty == 1)//四个黑一个空
           {
               weight += weightTable[4];
           }
           if (white == 1 && empty == 4)//白 同上
           {
               weight += weightTable[5];
           }
           if (white == 2 && empty == 3)
           {
               weight += weightTable[6];
           }
           if (white == 3 && empty == 2)
           {
               weight += weightTable[7];
           }
           if (white == 4 && empty == 1)
           {
               
               weight += weightTable[8];
           }
           if (white != 0 && black != 0)
           {

               weight -= 8;
           }
           return weight;
       }
        private void user_Tick(object sender, EventArgs e)
        {
            if (end != 1)
            {
                usertime += 0.1;
                uTime.Text = "已用时" + Math.Round(usertime, 1) + "秒";
                panel4.BackColor = Color.Yellow;
                panel3.BackColor = Color.Transparent;
            }
        }

        private void com_Tick(object sender, EventArgs e)
        {   
            if(end != 1)
            {
                comtime += 0.1;
                cTime.Text = "已用时" + Math.Round(comtime, 1) + "秒";
                panel3.BackColor = Color.Yellow;
                panel4.BackColor = Color.Transparent;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {   
            if(end !=1)
            computerAddCheer();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
           
        }

        private void begin_Tick(object sender, EventArgs e)
        {
            if (whoFirst2.Text == "") return;
            button1.Enabled = true;
            if (whoFirst2.Text == "B")
            {
                com.Enabled = true;
                counts++; 
                cs[189].Image = Resources.black_red;
                userFlag = 1;
                cs[189].Text = "B";
                if (forwordStep != -1)
                    cs[forwordStep].Image = Resources.white;
                forwordStep = 189;
                whoWin();
                computerTurn = 0;
                timer2.Enabled = false;
                com.Enabled = false;
                user.Enabled = true;
                begin.Enabled = false;
                ls[(int)cs[189].Tag].Tag = counts;
            }
            else { 
                computerTurn =0;
                begin.Enabled = false;
                user.Enabled = true;
            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            timer4.Enabled = false;
            this.Text = "五子棋-by照相";
        }
        
        
    }
   
}
