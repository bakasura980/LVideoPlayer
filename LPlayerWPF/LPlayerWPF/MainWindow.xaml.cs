using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using LPlayerWPF.Properties;
using Application = System.Windows.Application;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Speech.Recognition;


namespace LPlayerWPF
{
    //Volume message ReWork !!!
	public partial class MainWindow : Window
	{
        private SpeechRecognitionEngine recengine = new SpeechRecognitionEngine();
        private int _currentTheme;
		private List<ResourceDictionary> Themes = new List<ResourceDictionary>();
		private Settings ProgramSettings = new Settings();
		private DispatcherTimer ShowBtnTimer = new DispatcherTimer();
		private bool StopSubtitlesBlock = false;
        private bool isFscroll = false;
        private Subtitles subtitle = new Subtitles();
        private VideoTime videotime = new VideoTime();


        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
       

		public MainWindow()
		{
			_currentTheme = ProgramSettings.settingsCurrentTheme;

			AddResources();

			UpdateTheme();

            dispatcherTimer.Tick += DispatcherTimer_Tick; ;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            

            InitializeComponent();

			this.KeyDown += KeyEventHandler;
		}

        

        private void KeyEventHandler(object sender, KeyEventArgs e)
		{
			//MessageBox.Show("Key: " + e.Key + " !");

			// трябва да намерим начин да не позволяваме на прозореца,
			// при натискане на стрелките, да отива на следващия/предишния бутон/tab
			// Затова има алтернативите А и D

			if (e.Key == Key.Left || e.Key == Key.A) // <- and A step back
			{
				this.StepBack_OnClick(sender,e);
			}

			if ( e.Key == Key.Right || e.Key == Key.D ) // -> and D step over
			{
				this.StepOver_OnClick( sender, e );
			}
			if ( e.Key == Key.M ) // Mute
			{
				this.Mute_OnClick( sender, e );
			}
			if ( e.Key == Key.Up || e.Key == Key.W) // Volume ++
			{
                BalanceSlider.Value += 0.1;
				Notify("Volume has been increased to " + Video.Volume*100 );
			}
			if ( e.Key == Key.Down || e.Key == Key.S ) // Volume --
			{
                BalanceSlider.Value -= 0.1;
				Notify( "Volume has been decreased to " + Video.Volume*100 );
			}
			if ( e.Key == Key.Space || e.Key == Key.Enter ) // <- and A step back
			{
				this.Start_OnClick( sender, e );
			}
		}

		private void ChangeTheme_OnClick(object sender, RoutedEventArgs e)
		{
			_currentTheme++;

			if (_currentTheme >= Themes.Count)
			{
				_currentTheme = 0;
			}
			UpdateTheme();
		}

		

		private void MainWindow_OnClosing(object sender, CancelEventArgs e)
		{
			//MessageBox.Show( "Closing called" );
			ProgramSettings.settingsCurrentTheme = _currentTheme;
			ProgramSettings.Save();
		}

		private async void Notify(string notification)
		{
			if (NotificationBlock == null) return;
            StopSubtitlesBlock = true;

            NotificationBlock.Text = notification;
            StopSubtitlesBlock = false;
			try
			{
				await Task.Delay(3000, new CancellationToken( StopSubtitlesBlock ) ); // if 'StopSubtitlesBlock' is true will stop process
				NotificationBlock.Text = "";
			}
			catch
			{
				MessageBox.Show( "Process was been canceled" ); // for now never !
				NotificationBlock.Text = "";
			}
		}

		private void UIElement_OnMouseMove(object sender, MouseEventArgs e)
		{
			if ( ShowBtnTimer != null)
			{
				ShowBtnTimer.Tick-= timer_HideBtn;
			}
			timer_ShowBtn();
			ShowBtnTimer.Interval = new TimeSpan(0, 0, 3);
			ShowBtnTimer.Tick += timer_HideBtn;
			ShowBtnTimer.Start();
		}

		
		private void Open_Video_OnClick(object sender, RoutedEventArgs e)
		{
		}

		private void ForwStop_OnClick(object sender, RoutedEventArgs e)
		{
			if (Video.HasVideo || Video.HasAudio)
			{
				Video.Stop();
				Video.Visibility = Visibility.Hidden;
				Start.Content = "➤";
			}
		}

		private void StepOver_OnClick(object sender, RoutedEventArgs e)
		{
			if (Video.HasVideo || Video.HasAudio )// if havent media-> doesnt throw exeption
			//Problem when have nothing at MediaElement and press this key is fixed	
			{
				if (Video.Position.TotalSeconds < Video.NaturalDuration.TimeSpan.TotalSeconds - 10)
				{
					Notify("10 seconds step over");
					Video.Position += TimeSpan.FromSeconds(10);
                    isFscroll = true;
                }
				else
				{
					Notify("Restarted");
					Video.Stop();
					Video.Play();
				}
			}
		}


		private void Start_OnClick(object sender, RoutedEventArgs e)
		{
			var absolutePath = Video.Source.AbsolutePath;
			var a = absolutePath.Split('.');
			if (a[a.Length - 1] == "mp3")
			{
				Notify("Playing song: " + a + "!");
			}
			switch (GetMediaState(Video))
			{
				case MediaState.Stop:
					Video.Visibility = Visibility.Visible;
					Notify("Playing");
					Video.Play();
					Start.Content = "❚❚";
					//MessageBox.Show("VIDEO WAS STOOPED");
					return;
				case MediaState.Pause:
				case MediaState.Close:
					Notify("Playing");
					Video.Play();
					Start.Content = "❚❚";
					break;
				default:
					if (Video.CanPause)
					{
						Notify( "Paused" );
						Video.Pause();
						Start.Content = "➤";
					}
					break;
			}
		}
        //Play next video from the list<string>();
        private void StepBack_OnClick(object sender, RoutedEventArgs e)
        {
            if (Video.HasVideo || Video.HasAudio) // if havent media-> doesnt throw exeption
                                                  //Problem when have nothing at MediaElement and press this key is fixed	
            {
                if (Video.Position.TotalSeconds < Video.NaturalDuration.TimeSpan.TotalSeconds - 10)
                {
                    isFscroll = true;
                    Video.Position -= TimeSpan.FromSeconds(10);
                    Notify("10 seconds step back");
                }
                else
                {
                    Video.Stop();
                    Video.Play();
                    Notify("Restarted");
                }
            }
        }

		private void Pause_OnClick(object sender, RoutedEventArgs e)
		{
			// NEVER USE THIS METHOD
			if (Video.CanPause)
			{
				Notify("Paused");
				Video.Pause();
			}
		}

		private void MenuShow_OnClick(object sender, RoutedEventArgs e)
		{
			
		}

		private void Home_OnClick(object sender, RoutedEventArgs e)
		{
			
		}

        private static List<Subtitles> subs = new List<Subtitles>();
        private static bool subsbtnclicked;

		private void SubsShortcut_OnClick(object sender, RoutedEventArgs e)
		{
            if (subs != null)
            {
                subs.Clear();
            }
            var openfiledialog = new Microsoft.Win32.OpenFileDialog();
            if (openfiledialog.ShowDialog() == true)
            {
                subsbtnclicked = true;
                subs = Subtitles.GetSubs(openfiledialog.FileName);
            }
		}

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (subsbtnclicked)
            {
                SubtitlesText.Text = subtitle.PrintSubs(subs, Video.Position.Seconds, Video.Position.Minutes,
                    Video.Position.Hours, isFscroll);
                isFscroll = false;
            }
            TimerTxt.Text = videotime.PrintTime(Video.Position.Seconds, Video.Position.Minutes, Video.Position.Hours);
        }


        private PlayList playlistwindow = new PlayList();
        private PlayListVideosList playlist = new PlayListVideosList();

        private void PlayList_OnClick(object sender, RoutedEventArgs e)
		{
            if (playlistwindow.IsOpen == true)
            {
                MessageBox.Show("This Form is already opened");
            }
            else
            {
                playlistwindow = new PlayList();
                playlistwindow.IsOpen = true;
                playlistwindow.FormClosed += Playlistwindow_FormClosed;
                playlistwindow.Show();
                this.WindowState = WindowState.Minimized;
            }
        }
        
        private void Playlistwindow_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            this.WindowState = WindowState.Normal;
            BinaryFormatter formatter = new BinaryFormatter();
            if (playlistwindow.CurrentStream.Length > 0)
            {
                using (playlistwindow.CurrentStream)
                {
                    playlist = (PlayListVideosList)formatter.Deserialize(playlistwindow.CurrentStream);
                }
            }
            if (playlist.ListOfVideos.Count > 0)
            {
                foreach (var item in playlist.ListOfVideos)
                {
                    Video.Source = new Uri(item);
                    break;
                }
                Video.Play();
            }
        }

        private void Mute_OnClick(object sender, RoutedEventArgs e)
		{
			if (Video.HasAudio) // ESCAPE EXEPTION WHEN HAVENT VIDEO/MUSIC
			{

				Notify(Video.IsMuted ? "Unmuted" : "Muted");

				Video.IsMuted = !Video.IsMuted;
			}
		}

		private void UpdateTheme()
		{
			Application.Current.Resources.MergedDictionaries.Clear();
			Application.Current.Resources.MergedDictionaries.Add( Themes[_currentTheme] );
			Notify("Changed Theme" /*" to" +Themes[_currentThemeBe]*/);
		}

		protected void AddResources()
		{
			ResourceDictionary RedTheme = new ResourceDictionary();
			ResourceDictionary BlueTheme = new ResourceDictionary();
			ResourceDictionary GreenTheme = new ResourceDictionary();

			RedTheme.Source = new Uri( "/Themes/RedTheme.xaml", UriKind.Relative );
			BlueTheme.Source = new Uri( "/Themes/BlueTheme.xaml", UriKind.Relative );
			GreenTheme.Source = new Uri( "/Themes/GreenTheme.xaml", UriKind.Relative );

			Themes.Add( RedTheme );
			Themes.Add( BlueTheme );
			Themes.Add( GreenTheme );
		}

		void timer_HideBtn(object sender, EventArgs e)
		{

			Home.Visibility		= Visibility.Hidden;
			PlayList.Visibility	= Visibility.Hidden;
			SubSrtct.Visibility = Visibility.Hidden;
			ChgTheme.Visibility	= Visibility.Hidden;
			None5.Visibility	= Visibility.Hidden;
			Start.Visibility	= Visibility.Hidden;
			ForwStop.Visibility	= Visibility.Hidden;
			StepOver.Visibility	= Visibility.Hidden;
			StepBack.Visibility	= Visibility.Hidden;
			MenuShow.Visibility	= Visibility.Hidden;
			OpenBtn.Visibility		= Visibility.Hidden;
			none1.Visibility	= Visibility.Hidden;
			none2.Visibility	= Visibility.Hidden;
			SavePhot.Visibility	= Visibility.Hidden;
			Mute.Visibility		= Visibility.Hidden;
            BalanceSlider.Visibility = Visibility.Hidden;
            VolumeSlider.Visibility = Visibility.Hidden;

			Mouse.OverrideCursor	= Cursors.None;
		}

		void timer_ShowBtn()
		{
			Home.Visibility		= Visibility.Visible;
			PlayList.Visibility = Visibility.Visible;
			SubSrtct.Visibility = Visibility.Visible;
			ChgTheme.Visibility = Visibility.Visible;
			None5.Visibility	= Visibility.Visible;
			Start.Visibility	= Visibility.Visible;
			ForwStop.Visibility	= Visibility.Visible;
			StepOver.Visibility	= Visibility.Visible;
			StepBack.Visibility	= Visibility.Visible;
			MenuShow.Visibility	= Visibility.Visible;
			OpenBtn.Visibility		= Visibility.Visible;
			none1.Visibility	= Visibility.Visible;
			none2.Visibility	= Visibility.Visible;
			SavePhot.Visibility = Visibility.Visible;
			Mute.Visibility		= Visibility.Visible;

			Mouse.OverrideCursor	= Cursors.Arrow;

		}

		private MediaState GetMediaState(MediaElement myMedia)
		{
			FieldInfo hlp = typeof( MediaElement ).GetField( "_helper", BindingFlags.NonPublic | BindingFlags.Instance );
			object helperObject = hlp.GetValue( myMedia );
			FieldInfo stateField = helperObject.GetType().GetField( "_currentState", BindingFlags.NonPublic | BindingFlags.Instance );
			MediaState state = (MediaState)stateField.GetValue( helperObject );
			return state;
		}

		private void None4_OnClick(object sender, RoutedEventArgs e)
		{
		}

        private void OpenBtn_Click(object sender, RoutedEventArgs e)
        {
            var open = new Microsoft.Win32.OpenFileDialog();
            if (open.ShowDialog() == true)
            {
                Video.Source = new Uri(open.FileName);
                Video.Play();
                dispatcherTimer.Start();
            }
        }

        private void none4_MouseEnter(object sender, MouseEventArgs e)
        {
            
        }
        //Buffer trying !!!
        private void BalanceSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Video.Balance = e.NewValue + 1;
        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Video.Volume = e.NewValue + 1;
        }

        private void SetRecognizer()
        {
            Choices choices = new Choices();
            choices.Add(new string[] { "Begin", "Play", "Stop", "Pause", "Mute", "Scroll forward", "Scroll back",
                "Sroll backward" ,  "Playlist", "Choose", "Subs", "Subtitles", "Continue"});

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
                case "Scroll backward":
                    StepBack_OnClick(this, new RoutedEventArgs());
                    break;
                case "Mute":
                    Mute_OnClick(this, new RoutedEventArgs());
                    break;
                case "Continue":
                    Start_OnClick(this, new RoutedEventArgs());
                    break;
                case "Play":
                    Start_OnClick(this, new RoutedEventArgs());
                    break;
                case "Begin":
                    Start_OnClick(this, new RoutedEventArgs());
                    break;
                case "Stop":
                    Pause_OnClick(this, new RoutedEventArgs());
                    break;
                case "Pause":
                    Pause_OnClick(this, new RoutedEventArgs());
                    break;
                case "Scroll forward":
                    StepOver_OnClick(this, new RoutedEventArgs());
                    break;
                case "Scroll back":
                    StepBack_OnClick(this, new RoutedEventArgs());
                    break;
                case "Playlist":
                    PlayList_OnClick(this, new RoutedEventArgs());
                    break;
                case "Choose":
                    OpenBtn_Click(this, new RoutedEventArgs());
                    break;
                case "Subs":
                case "Subtitles":
                    SubsShortcut_OnClick(this, new RoutedEventArgs());
                    break;
                default:
                    break;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetRecognizer();
        }

        private void Mute_MouseEnter(object sender, MouseEventArgs e)
        {
            VolumeSlider.Visibility = Visibility.Visible;
            BalanceSlider.Visibility = Visibility.Visible;
        }
    }
}
