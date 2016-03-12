using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using Microsoft.DirectX.AudioVideoPlayback;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;
using System.Windows.Input;
using System.Collections.Generic;

namespace LPlayer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            isclicked = false;
            TimerTxt.Visible = false;
            SubsLabel.BackColor = Color.Transparent;
        }
        private int position = 0;
        private Video video;

        private void OpenDialog_Click(object sender, EventArgs e)
        {
            var opend = new OpenFileDialog();
            if (opend.ShowDialog() == DialogResult.OK)
            {
                video = new Video(opend.FileName, true);
                video.Owner = panel1;
                isclicked = true;
                TimerTxt.Visible = true;
                panel1.Dock = DockStyle.Fill;
                var size = new Size();
                size.Width = video.Size.Width;
                size.Height = video.Size.Height;
                Height = size.Height + 38;
                Width = size.Width + 16;
            }
        }
        private bool isclicked;

        private void Stopbtn_Click(object sender, EventArgs e)
        {
            video.Pause();
            isclicked = false;
        }
        private void StartBtn_Click(object sender, EventArgs e)
        {
            video.Play();
            isclicked = true;
        }
        private int sec = 0, min = 0, hour = 0;
        private bool isFscroll = false;
        private void FScrollBtn_Click(object sender, EventArgs e)
        {
            video.CurrentPosition += min + 200;
            min = Convert.ToInt32(video.CurrentPosition / 60);
            sec = Convert.ToInt32(video.CurrentPosition % 60);
            isFscroll = true;
        }

        private static bool isclickedsubsbutton;
        private static List<Subtitles> subs;

        private void SubsBtn_Click(object sender, EventArgs e)
        {
            if (subs != null)
            {
                subs.Clear();
            }
            OpenFileDialog openfaildialod = new OpenFileDialog();
            if (openfaildialod.ShowDialog() == DialogResult.OK)
            {
                isclickedsubsbutton = true;
                subs = Subtitles.GetSubs(openfaildialod.FileName);
            }
        }

        private VideoTime videotime = new VideoTime();
        private Subtitles subtitle = new Subtitles();
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (isclicked)
            {
                position = Convert.ToInt32(video.CurrentPosition);
                if (position > 0)
                {
                    if (position > 60)
                    {
                        sec = position % 60;
                        min = position / 60;
                    }
                    else
                    {
                        sec = position;
                    }
                }
            }
            if (isclickedsubsbutton)
            {
                SubsLabel.Text = subtitle.PrintSubs(subs, sec, min, hour, isFscroll);
                isFscroll = false;
            }
            TimerTxt.Text = videotime.PrintTime(sec,min,hour);
        }
    }
}
