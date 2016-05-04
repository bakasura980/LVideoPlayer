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

namespace LPlayerWPF
{
	public partial class MainWindow : Window
	{
		private int _currentTheme;
		private List<ResourceDictionary> Themes = new List<ResourceDictionary>();
		private Settings ProgramSettings = new Settings();
		private DispatcherTimer ShowBtnTimer = new DispatcherTimer();
		private bool StopSubtitlesBlock = false;
		
		public MainWindow()
		{
			_currentTheme = ProgramSettings.settingsCurrentTheme;

			AddResources();

			UpdateTheme();

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
				if ( Video.Volume >= 90 )
				{
					Video.Volume = 100;
				}
				else Video.Volume += 10;
				Notify("Volume has been increased to " + Video.Volume);
			}
			if ( e.Key == Key.Down || e.Key == Key.S ) // Volume --
			{
				if (Video.Volume <= 10)
				{
					Video.Volume = 0;
				}else Video.Volume -= 10;

				Notify( "Volume has been decreased to " + Video.Volume );
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

			NotificationBlock.Text = notification;
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

		private void StepBack_OnClick(object sender, RoutedEventArgs e)
		{
			if (Video.HasVideo || Video.HasAudio) // if havent media-> doesnt throw exeption
				//Problem when have nothing at MediaElement and press this key is fixed	
				{
					if (Video.Position.TotalSeconds < Video.NaturalDuration.TimeSpan.TotalSeconds - 10)
					{
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

		private void SubsShortcut_OnClick(object sender, RoutedEventArgs e)
		{
			
		}

		private void PlayList_OnClick(object sender, RoutedEventArgs e)
		{
			
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
			none.Visibility		= Visibility.Hidden;
			none1.Visibility	= Visibility.Hidden;
			none2.Visibility	= Visibility.Hidden;
			SavePhot.Visibility	= Visibility.Hidden;
			none4.Visibility	= Visibility.Hidden;
			Mute.Visibility		= Visibility.Hidden;

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
			none.Visibility		= Visibility.Visible;
			none1.Visibility	= Visibility.Visible;
			none2.Visibility	= Visibility.Visible;
			SavePhot.Visibility = Visibility.Visible;
			none4.Visibility	= Visibility.Visible;
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
			// test method 
			if (Video.Source.ToString().Contains("mp3"))
			{
				Video.Source = new Uri( "C:\\Users\\Bobo-PC\\Desktop\\GOT_Best_Scene.mp4" );
			}
			else Video.Source = new Uri("C:\\Users\\Bobo-PC\\Downloads\\MahmutOrhan-Feelfeat.SenaSener(Official Video).mp3");
		}
	}
}
