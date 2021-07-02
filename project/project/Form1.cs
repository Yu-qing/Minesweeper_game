using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace project
{
    public partial class Form1 : Form
    {
        Random rnd = new Random();
        Label[,] grids = new Label[10, 10];
        int[,] map = new int[10, 10];
        int min, sec;
        int type, read, index;
        int btn;
        int flag;
        string s;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label2.Text = "";
            label3.Text = "";

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    grids[i, j] = new Label();
                    grids[i, j].Width = 50;
                    grids[i, j].Height = 50;
                    grids[i, j].BorderStyle = BorderStyle.Fixed3D;
                    grids[i, j].BackColor = Color.Gray;
                    grids[i, j].ForeColor = Color.Gray;
                    grids[i, j].Left =50 * j;
                    grids[i, j].Top = 50 * i;
                    grids[i, j].Visible = true;
                    grids[i, j].Font = new Font("微軟正黑體", 20, FontStyle.Bold);
                    grids[i, j].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                    this.Controls.Add(grids[i, j]);
                }
            }
        }

        private void start_time()
        {
            label1.Visible = false;

            button1.Visible = false;
            button1.Enabled = false;
            button2.Visible = false;
            button2.Enabled = false;
            button3.Visible = false;
            button3.Enabled = false;
            button4.Visible = false;
            button4.Enabled = false;
            button5.Visible = false;
            button5.Enabled = false;
            button6.Visible = true;
            button6.Enabled = true;

            for (int i=0; i<10; i++)
            {
                for (int j = 0; j < 10; j++)
                    grids[i, j].Click -= new EventHandler(this.labels_Click);
            }

            read = 10;
            index = 0;
            btn = 0;

            StreamReader str = new StreamReader(System.IO.Directory.GetCurrentDirectory() + "\\"+ Convert.ToString(type+1) +".txt");
            while(read != 0)
            {
                s = str.ReadLine();
                foreach (char c in s)
                {
                    grids[(index / 10), (index % 10)].Click += new EventHandler(this.labels_Click);
                    grids[(index / 10), (index % 10)].Text = Convert.ToString(c);
                    grids[(index / 10), (index % 10)].ForeColor = Color.Gray;
                    grids[(index / 10), (index % 10)].BackColor = Color.Gray;
                    grids[(index / 10), (index % 10)].Enabled = true;
                    grids[(index / 10), (index % 10)].Visible = true;
                    if (c == 'X')
                    {
                        btn++;
                        label2.Text = btn + "顆炸彈";
                    }
                        
                    index++;
                }
                read--;
            }
            str.Close();

            for (int i=0; i<10; i++)
            {
                for (int j = 0; j < 10; j++)
                    map[i, j] = 0;
            }


            flag = 0;
            min = 0;
            sec = 0;
            timer1.Start();

            label3.Text = "time: "+min + "min\n        " + sec + "sec";
        }
        public void Open_Close()
        {
            label2.Text = "";
            label3.Text = "";
            for (int j = 0; j < 10; j++)
            {
                for (int k = 0; k < 10; k++)
                {
                    grids[j, k].Enabled = false;
                    grids[j, k].Visible = false;
                }
            }

            label1.Visible = true;
            button1.Visible = true;
            button1.Enabled = true;
            button2.Visible = true;
            button2.Enabled = true;
            button3.Visible = true;
            button3.Enabled = true;
            button4.Visible = true;
            button4.Enabled = true;
            button5.Visible = true;
            button5.Enabled = true;
            button6.Visible = false;
            button6.Enabled = false;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            sec++;
            if(sec == 60)
            {
                min ++;
                sec = 0;
            }

            if(btn==100)
            {
                timer1.Stop();
                MessageBox.Show("成功  time : " + min + "min" + sec + "sec", "result");
                Open_Close();
            }

            label3.Text= "time: " + min + "min\n        " + sec + "sec";
        }

        private void labels_Click(object sender, EventArgs e)
        {
            Label lab =  (Label) sender;
            lab.ForeColor = Color.Black;
            int x = lab.Top / 50;
            int y = lab.Left / 50;
            
            if(button6.Text=="旗")
            {
                lab.ForeColor = Color.Red;
                lab.BackColor = Color.Red;
                map[x, y] = 2;
            }

            else
            {
                if(map[x,y]==2)
                {
                    lab.ForeColor = Color.Gray;
                    lab.BackColor = Color.Gray;
                    map[x, y] = 0;
                    grids[x, y].Click += new EventHandler(this.labels_Click);
                }

                else if (lab.Text == "X")
                {
                    timer1.Stop();
                    MessageBox.Show("失敗", "result");
                    Open_Close();
                }

                else if (map[x, y] == 0)
                {
                    btn++;
                    map[x, y] = 1;

                    int num;
                    if (type <= 2)
                        num = rnd.Next(0, 15);
                    else
                        num = rnd.Next(18, 30);
                    for (int i = 0; i < num; i++)
                    {
                        x = rnd.Next(0, 9);
                        y = rnd.Next(0, 9);
                        if (grids[x, y].Text != "X" && map[x, y] == 0)
                        {
                            grids[x, y].ForeColor = Color.Black;
                            btn++;
                            map[x, y] = 1;
                        }
                    }
                }
            }

            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            type = 0;
            start_time();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            type = 1;
            start_time();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            type = 2;
            start_time();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            type = 3;
            start_time();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            type = 4;
            start_time();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            flag++;
            if (flag % 2 == 1)
                button6.Text = "旗";
            else
                button6.Text = "";
        }
    }
}
