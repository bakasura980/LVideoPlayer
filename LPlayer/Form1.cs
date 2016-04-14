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
using System.Speech.Recognition;
using System.Threading;
using System.Reflection;

namespace LPlayer
{//Disable
   
    public partial class Form1 : Form
    {
        private delegate void SetThreadCallback();
        private SpeechRecognitionEngine recengine = new SpeechRecognitionEngine();
        public Form1()
        {
            InitializeComponent();
            Reflection();
            timer2.Enabled = false;
            isclicked = false;
            TimerTxt.Visible = false;
            SubsLabel.BackColor = Color.Transparent;
            FScrollBtn.Visible = false;
            OpenDialog.Visible = false;
            SubsBtn.Visible = false;
            StartBtn.Visible = false;
        }
        private int position = 0;
        private Video video;
        //Make it for music formats , not only for video formats
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
                timer2.Enabled = true;
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

        private void button1_Click(object sender, EventArgs e)
        {
            if ((Application.OpenForms["PlayList"] as PlayList) != null)
            {
                MessageBox.Show("This Form is already opened");
            }
            else
            {
                PlayList playlistform = new PlayList();
                playlistform.Show();
            }
        }
        private Thread backgroundthread;
        private void Form1_Load(object sender, EventArgs e)
        {
            this.backgroundthread = new Thread(() => SetRecognizer());
            backgroundthread.Start();
        }

        private void SetRecognizer()
        {
            Choices choices = new Choices();
            choices.Add(new string[] { "Start", "Begin", "Stop", "Pause", "Down", "Scroll forward", "Scroll backward", "Playlist", "Choose", "Subs", "Subtitles", "Continue", "Disable" });

            GrammarBuilder grbuilder = new GrammarBuilder();
            grbuilder.Append(choices);

            Grammar grammar = new Grammar(grbuilder);
            recengine.LoadGrammarAsync(grammar);

            recengine.SpeechRecognized += Recengine_SpeechRecognized;
            recengine.SetInputToDefaultAudioDevice();
            recengine.RecognizeAsync(RecognizeMode.Multiple);
        }

        private void Recengine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text)
            {
                case "Start":
                    StartBtn_Click(this, new EventArgs());
                    break;
                case "Continue":
                    break; 
                case "Begin":
                    break;
                case "Stop":
                    break;
                case "Pause":
                    break;
                case "Scroll forward":
                    FScrollBtn_Click(this, new EventArgs());
                    break;
                case "Scroll backward":
                    break;
                case "Playlist":
                    break;
                case "Choose":
                    OpenDialog_Click(new Form1(), new EventArgs());
                    break;
                case "Subs": 
                case "Subtitles":
                    SubsBtn_Click(this, new EventArgs());
                    break;
                case "Disable":
                    InvokeReq("Disable");
                    break;
                default:
                    MessageBox.Show("Say disable to show buttons or just speek");
                    break;
            }
        }

        private void Reflection()
        {
           Type maincontroltype = (typeof(MainControl));
           MethodInfo[] methods = maincontroltype.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
           button1.Text = methods.Length == 0 ? "Yep" : methods[methods.Length].ToString();
        }

        private void InvokeReq(string command)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SetThreadCallback(delegate  {
                    MainControl.SetVisibleValue(true, FScrollBtn, OpenDialog, SubsBtn, StartBtn); }));
            }
            else
            {
                MainControl.SetVisibleValue(true, FScrollBtn, OpenDialog, SubsBtn, SubsBtn);
            }
        }

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
