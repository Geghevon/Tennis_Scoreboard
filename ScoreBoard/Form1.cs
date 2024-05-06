using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScoreBoard
{
    
    public partial class Tennis_Scoreboard : Form
    {
        string path = File.ReadAllText(Environment.CurrentDirectory + "\\config.txt");
        int ball_index = 0;
        int p1_now = 0, p2_now=0;
        int set = 1;
        string[,] setters = new string[4,7];
        int ball_start_pos = 0;
        int end_of_set = 0;
        private void Init_Massive()
        {
            setters[0, 0] = "S1P1";
            setters[0, 1] = "S2P1";
            setters[0, 2] = "S3P1";
            setters[0, 3] = "S4P1";
            setters[0, 4] = "S5P1";
            setters[0, 5] = "S6P1";
            setters[0, 6] = "S7P1";
            setters[2, 0] = "S1P2";
            setters[2, 1] = "S2P2";
            setters[2, 2] = "S3P2";
            setters[2, 3] = "S4P2";
            setters[2, 4] = "S5P2";
            setters[2, 5] = "S6P2";
            setters[2, 6] = "S7P2";
            setters[1, 0] = (path + "P1S1.txt");
            setters[1, 1] = (path + "P1S2.txt");
            setters[1, 2] = (path + "P1S3.txt");
            setters[1, 3] = (path + "P1S4.txt");
            setters[1, 4] = (path + "P1S5.txt");
            setters[1, 5] = (path + "P1S6.txt");
            setters[1, 6] = (path + "P1S7.txt");
            setters[3, 0] = (path + "P2S1.txt");
            setters[3, 1] = (path + "P2S2.txt");
            setters[3, 2] = (path + "P2S3.txt");
            setters[3, 3] = (path + "P2S4.txt");
            setters[3, 4] = (path + "P2S5.txt");
            setters[3, 5] = (path + "P2S6.txt");
            setters[3, 6] = (path + "P2S7.txt");
        }
        public Tennis_Scoreboard()
        {
            InitializeComponent();
            Fill_Player();
            Init_Massive();
        }

        private void PLUS_1_fp_Click(object sender, EventArgs e)
        {
            Plus_with_set("S" + set.ToString(), "P"+"1");
            Ball_Control();
        }
        private void PLUS_1_sp_Click(object sender, EventArgs e)
        {
            Plus_with_set("S" + set.ToString(), "P" + "2");
            Ball_Control();
        }
        private void MINUS_1_fp_Click(object sender, EventArgs e)
        {
            Minus_with_set("S" + set.ToString(), "P" + "1");
            if (p1_now == 0)
                return;
            Ball_Control_Minus();
        }

        private void MINUS_1_sp_Click(object sender, EventArgs e)
        {
            Minus_with_set("S" + set.ToString(), "P" + "2");
            if (p2_now == 0)
                return;
            Ball_Control_Minus();
        }
        private void Minus_with_set(string sett, string player)
        {
            for (int i = 0, j = 0, m = 0; i < setters.GetLength(1); i++)
            {
                if (setters[j, i] == sett + player)
                {
                    if (j == 0)
                    {
                        if (p1_now == 0)
                            return;
                        p1_now--;
                        File.WriteAllText(setters[j + 1, i], p1_now.ToString());
                        switch (set)
                        {
                            case 1:
                                S1P1.Text = p1_now.ToString();
                                break;
                            case 2:
                                S2P1.Text = p1_now.ToString();
                                break;
                            case 3:
                                S3P1.Text = p1_now.ToString();
                                break;
                            case 4:
                                S4P1.Text = p1_now.ToString();
                                break;
                            case 5:
                                S5P1.Text = p1_now.ToString();
                                break;
                            case 6:
                                S6P1.Text = p1_now.ToString();
                                break;
                            case 7:
                                S7P1.Text = p1_now.ToString();
                                break;
                            default:
                                break;
                        }
                    }
                    else if (j == 2)
                    {
                        if (p2_now == 0)
                            return;
                        p2_now--;
                        File.WriteAllText(setters[j + 1, i], p2_now.ToString());
                        switch (set)
                        {
                            case 1:
                                S1P2.Text = p2_now.ToString();
                                break;
                            case 2:
                                S2P2.Text = p2_now.ToString();
                                break;
                            case 3:
                                S3P2.Text = p2_now.ToString();
                                break;
                            case 4:
                                S4P2.Text = p2_now.ToString();
                                break;
                            case 5:
                                S5P2.Text = p2_now.ToString();
                                break;
                            case 6:
                                S6P2.Text = p2_now.ToString();
                                break;
                            case 7:
                                S7P2.Text = p2_now.ToString();
                                break;
                            default:
                                break;
                        }

                    }
                    m = 1;
                }
                if (m == 0 && i == 6)
                {
                    j += 2;
                    i = -1;
                    m = 2;
                }
                if (m == 1)
                {
                    if ((p2_now - p1_now >= 2 || p1_now - p2_now >= 2) && (p2_now > 10 || p1_now > 10))
                    {
                        set++;
                        File.WriteAllText(path + "Set" + set.ToString() + "Score.txt", p1_now.ToString() + "-" + p2_now.ToString());
                        p1_now = 0;
                        p2_now = 0;
                        end_of_set = 1;
                    }
                    Score_Now();
                    break;
                }
            }
        }
        private void Ball_Control_Minus()
        {
            if (p1_now >= 10 && p2_now >= 10)
            {
                if (checkBox1.Checked == true)
                    checkBox2.Checked = true;
                else if (checkBox2.Checked == true)
                    checkBox1.Checked = true;
                ball_index = 0;
                return;
            }
            else if (ball_index == 1)
            {
                ball_index = 0;
                return;
            }
            else if (ball_index == 0)
            {
                if (checkBox1.Checked == true)
                    checkBox2.Checked = true;
                else if (checkBox2.Checked == true)
                    checkBox1.Checked = true;
                ball_index = 1;
                return;
            }
            ball_index++;
        }
        private void Ball_Control()
        {
            if (end_of_set == 1)
            {

                if (ball_start_pos == 1)
                {
                    checkBox2.Checked = true;
                    end_of_set = 0;
                    ball_index = 0;
                    ball_start_pos = 2;
                }
                else if (ball_start_pos == 2)
                {
                    checkBox1.Checked = true;
                    end_of_set = 0;
                    ball_index = 0;
                    ball_start_pos = 1;
                }
                end_of_set = 0;
                return ;
            }
            if(p1_now >= 10 && p2_now >= 10)
            {
                if (checkBox1.Checked == true)
                    checkBox2.Checked = true;
                else if (checkBox2.Checked == true)
                    checkBox1.Checked = true;
                ball_index = 0;
                return;
            }
            else if (ball_index == 1)
            {
                if(checkBox1.Checked == true)
                    checkBox2.Checked = true;
                else if(checkBox2.Checked == true)
                    checkBox1.Checked = true;
                ball_index = 0;
                return;
            }
            ball_index++;
        }
        private void Plus_with_set(string sett, string player)
        {
            for (int i = 0, j = 0, m = 0; i < setters.GetLength(1); i++)
            {
                if (setters[j,i] == sett + player)
                {
                    if (j == 0)
                    {
                        p1_now++;
                        File.WriteAllText(setters[j + 1, i], p1_now.ToString());
                        switch (set)
                        {
                            case 1:
                                S1P1.Text = p1_now.ToString();
                                break;
                            case 2:
                                S2P1.Text = p1_now.ToString();
                                break;
                            case 3:
                                S3P1.Text = p1_now.ToString();
                                break;
                            case 4:
                                S4P1.Text = p1_now.ToString();
                                break;
                            case 5:
                                S5P1.Text = p1_now.ToString();
                                break;
                            case 6:
                                S6P1.Text = p1_now.ToString();
                                break;
                            case 7:
                                S7P1.Text = p1_now.ToString();
                                break;
                            default:
                                break;
                        } 
                    }
                    else if (j == 2)
                    {
                        p2_now++;
                        File.WriteAllText(setters[j + 1, i], p2_now.ToString());
                        switch (set)
                        {
                            case 1:
                                S1P2.Text = p2_now.ToString();
                                break;
                            case 2:
                                S2P2.Text = p2_now.ToString();
                                break;
                            case 3:
                                S3P2.Text = p2_now.ToString();
                                break;
                            case 4:
                                S4P2.Text = p2_now.ToString();
                                break;
                            case 5:
                                S5P2.Text = p2_now.ToString();
                                break;
                            case 6:
                                S6P2.Text = p2_now.ToString();
                                break;
                            case 7:
                                S7P2.Text = p2_now.ToString();
                                break;
                            default:
                                break;
                        }
                        
                    }
                    m = 1;
                }
                if (m == 0 && i == 6)
                {
                    j += 2;
                    i = -1;
                    m = 2;
                }
                if (m == 1)
                {
                    if ((p2_now - p1_now >= 2 || p1_now - p2_now >= 2) && (p2_now > 10 || p1_now >10))
                    {
                        set++;
                        File.WriteAllText(path + "Set" + set.ToString() + "Score.txt", p1_now.ToString() + "-" + p2_now.ToString());
                        p1_now = 0;
                        p2_now = 0;
                        end_of_set = 1;

                    }
                    Score_Now();
                    break;
                }
            }
        }
        private void Score_Now()
        {
            File.WriteAllText(path + "P1CurrentScore.txt", p1_now.ToString());
            File.WriteAllText(path + "P2CurrentScore.txt", p2_now.ToString());
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string fullpath = path + "player1name.txt";
            File.WriteAllText(fullpath, Player_1_name.Text);
        }
        private void Player_2_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            string fullpath = path + "player2name.txt";
            File.WriteAllText(fullpath, Player_2_name.Text);
        }
        private void Fill_Player()
        {
            string fullpath = path + "players.txt";
            foreach (string line in File.ReadLines(fullpath))
            {
                Player_1_name.Items.Add(line);
                Player_2_name.Items.Add(line);
            }
        }

        private void S1P1_TextChanged(object sender, EventArgs e)
        {
            string fullpath = path + "P1S1.txt";
            File.WriteAllText(fullpath, S1P1.Text);
        }

        private void S2P1_TextChanged(object sender, EventArgs e)
        {
            string fullpath = path + "P1S2.txt";
            File.WriteAllText(fullpath, S2P1.Text);
        }

        private void S3P1_TextChanged(object sender, EventArgs e)
        {
            string fullpath = path + "P1S3.txt";
            File.WriteAllText(fullpath, S3P1.Text);
        }

        private void S4P1_TextChanged(object sender, EventArgs e)
        {
            string fullpath = path + "P1S4.txt";
            File.WriteAllText(fullpath, S4P1.Text);
        }

        private void S5P1_TextChanged(object sender, EventArgs e)
        {
            string fullpath = path + "P1S5.txt";
            File.WriteAllText(fullpath, S5P1.Text);
        }

        private void S6P1_TextChanged(object sender, EventArgs e)
        {
            string fullpath = path + "P1S6.txt";
            File.WriteAllText(fullpath, S6P1.Text);
        }

        private void S7P1_TextChanged(object sender, EventArgs e)
        {
            string fullpath = path + "P1S7.txt";
            File.WriteAllText(fullpath, S7P1.Text);
        }

        private void S1P2_TextChanged(object sender, EventArgs e)
        {
            string fullpath = path + "P2S1.txt";
            File.WriteAllText(fullpath, S1P2.Text);
        }

        private void S2P2_TextChanged(object sender, EventArgs e)
        {
            string fullpath = path + "P2S2.txt";
            File.WriteAllText(fullpath, S2P2.Text);
        }

        private void S3P2_TextChanged(object sender, EventArgs e)
        {
            string fullpath = path + "P2S3.txt";
            File.WriteAllText(fullpath, S3P2.Text);
        }

        private void S4P2_TextChanged(object sender, EventArgs e)
        {
            string fullpath = path + "P2S4.txt";
            File.WriteAllText(fullpath, S4P2.Text);
        }

        private void S5P2_TextChanged(object sender, EventArgs e)
        {
            string fullpath = path + "P2S5.txt";
            File.WriteAllText(fullpath, S5P2.Text);
        }

        private void S6P2_TextChanged(object sender, EventArgs e)
        {
            string fullpath = path + "P2S6.txt";
            File.WriteAllText(fullpath, S6P2.Text);
        }

        private void S7P2_TextChanged(object sender, EventArgs e)
        {
            string fullpath = path + "P2S7.txt";
            File.WriteAllText(fullpath, S7P2.Text);
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true && checkBox1.Checked == true)
                checkBox2.Checked = false;
            if (S1P1.Text == "0" && S1P2.Text == "0" && S2P2.Text == "0" && S3P2.Text == "0" && S4P2.Text == "0"
                && S5P2.Text == "0" && S6P2.Text == "0" && S7P2.Text == "0" && S2P1.Text == "0" && S3P1.Text == "0"
                && S4P1.Text == "0" && S5P1.Text == "0" && S6P1.Text == "0" && S7P1.Text == "0")
                ball_start_pos = 1;
            ball_index = 0;
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true && checkBox1.Checked == true)
                checkBox1.Checked = false;
            if (S1P1.Text == "0" && S1P2.Text == "0" && S2P2.Text == "0" && S3P2.Text == "0" && S4P2.Text == "0"
                && S5P2.Text == "0" && S6P2.Text == "0" && S7P2.Text == "0" && S2P1.Text == "0" && S3P1.Text == "0"
                && S4P1.Text == "0" && S5P1.Text == "0" && S6P1.Text == "0" && S7P1.Text == "0")
                ball_start_pos = 2;
            ball_index = 1;
        }
        private void RESET_Click(object sender, EventArgs e)
        {
            S1P1.Text = "0";
            S1P2.Text = "0";
            S2P1.Text = "0";
            S2P2.Text = "0";
            S3P1.Text = "0";
            S3P2.Text = "0";
            S4P1.Text = "0";
            S4P2.Text = "0";
            S5P1.Text = "0";
            S5P2.Text = "0";
            S6P1.Text = "0";
            S6P2.Text = "0";
            S7P1.Text = "0";
            S7P2.Text = "0";
            ball_index = 0;
            p1_now = 0;
            p2_now = 0;
            Player_1_name.Text = "Player 1";
            Player_2_name.Text = "Player 2";
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            ball_start_pos = 0;
            set = 1;
        }
    }
}
