using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.DirectX.AudioVideoPlayback;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;
using System.Windows.Input;

namespace LPlayer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            isclicked = true;
            SubsLabel.ForeColor = Color.Black;
        }
        int position = 0;
        public Video video;

        private void OpenDialog_Click(object sender, EventArgs e)
        {
            var opend = new OpenFileDialog();
            if (opend.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Dock = DockStyle.None;

                video = new Video(opend.FileName, true);
                label1.Text = video.CurrentPosition.ToString();
                video.Owner = pictureBox1;
                isclicked = false;
                var size = new Size();
                size.Width = video.Size.Width;
                size.Height = video.Size.Height;
                Height = size.Height + 38;
                Width = size.Width + 16;
                pictureBox1.Dock = DockStyle.Fill;
                TimerTxt.Dock = DockStyle.Bottom;
                SubsLabel.Dock = DockStyle.Bottom;
            }
        }
        bool isclicked;

        private void Stopbtn_Click(object sender, EventArgs e)
        {
            video.Pause();
            isclicked = true;
        }
        private void StartBtn_Click(object sender, EventArgs e)
        {
            video.Play();
            isclicked = false;

        }
        int sec = 0, min = 0, hour = 0;
        private void FScrollBtn_Click(object sender, EventArgs e)
        {
            video.CurrentPosition += min + 200;
            min = Convert.ToInt32(video.CurrentPosition / 60);
            sec = Convert.ToInt32(video.CurrentPosition % 60);
        }

        private static OpenFileDialog opensubs = new OpenFileDialog();
        private static bool isclickedsubsbutton;


        private void SubsBtn_Click(object sender, EventArgs e)
        {
            
            if (opensubs.ShowDialog() == DialogResult.OK)
            {
                isclickedsubsbutton = true;
            }
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            TimerTxt.BackColor = Color.Transparent;

            if (isclicked != true)
            {
                if (label1.Text == 0.ToString())
                {
                    position = Convert.ToInt32(video.CurrentPosition);
                }
                if (position > 0)
                {
                    position = Convert.ToInt32(video.CurrentPosition);
                    if (position > 60)
                    {
                        sec = position % 60;
                    }
                    else
                    {
                        sec = position;
                    }
                    if (position % 60 == 0)
                    {
                        min = position / 60;
                        sec = 0;
                    }
                }
            }
            if (isclickedsubsbutton == true)
            {
                SubsLabel.Text = Subs.PrintSubs(opensubs.FileName, sec, min, hour);
            }
            TimerTxt.Text = new PrintTime().PrintTimeVideo(sec,min,hour).ToString();
        }
    }
}
