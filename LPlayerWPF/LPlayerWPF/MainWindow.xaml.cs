using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
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
		private int _currentThemeBe;
		private List<ResourceDictionary> Themes = new List<ResourceDictionary>();
		private Settings ProgramSettings = new Settings();
		private DispatcherTimer timer = new DispatcherTimer();
		//private int videoLenght;

		public MainWindow()
		{
			_currentThemeBe = ProgramSettings.currentTheme;

			AddResources();

			UpdateTheme();

			InitializeComponent();

			//videoLenght = (int)Video.NaturalDuration.TimeSpan.TotalSeconds;
		}

		private void ChangeTheme_OnClick(object sender, RoutedEventArgs e)
		{
			_currentThemeBe++;

			if (_currentThemeBe >= Themes.Count)
			{
				_currentThemeBe = 0;
			}
			UpdateTheme();
		}

		

		private void MainWindow_OnClosing(object sender, CancelEventArgs e)
		{
			//MessageBox.Show( "Closing called" );
			ProgramSettings.currentTheme = _currentThemeBe;
			ProgramSettings.Save();
		}

		private void UIElement_OnMouseMove(object sender, MouseEventArgs e)
		{
		   if (timer != null)
		   {
			  timer.Tick-= timer_HideBtn;
		   }
		   timer_ShowBtn();
		   timer.Interval = new TimeSpan(0, 0, 3);
		   timer.Tick += timer_HideBtn;
		   timer.Start();
		}

		
		private void Open_Video_OnClick(object sender, RoutedEventArgs e)
		{
			//throw new NotImplementedException();
			//var fbd = new OpenFileDialog();
			//var result = fbd.FileName;
			
			//string[] filees = Directory.GetFiles( result);
			// // + nameOfNewFile;

			//try
			//{

			//	// Delete the file if it exists.
			//	if (File.Exists(result ) )
			//	{
			//		ProgramSettings.currentMovie = result;
					
			//		OpenVideoLabel.Content = "Open Video at : " + ProgramSettings.currentMovie;
			//		//..
			//	}
			//	else
			//	{
			//		MessageBox.Show("Not found");
			//	}

			//}
			//catch
			//{
			//	throw new FileLoadException();
			//}
		}

		private void ForwStop_OnClick(object sender, RoutedEventArgs e)
		{
			Video.Stop();
		}

		private void StepOver_OnClick(object sender, RoutedEventArgs e)
		{
		
			if (Video.Position.TotalSeconds < Video.NaturalDuration.TimeSpan.TotalSeconds - 10)
			{
				Video.Position += TimeSpan.FromSeconds(10);
			}
			else
			{
				Video.Stop();
				Video.Play();
			}
		}

		
		private void Start_OnClick(object sender, RoutedEventArgs e)
		{
			if (GetMediaState(Video) == MediaState.Pause || GetMediaState( Video ) == MediaState.Close ) 
			{
				Video.Play();
				Start.Content = "❚❚";
				return;
			}
			
			Video.Pause();
			Start.Content = "➤";
			return;
		}

		private void StepBack_OnClick(object sender, RoutedEventArgs e)
		{
			if ( Video.Position.TotalSeconds > Video.NaturalDuration.TimeSpan.TotalSeconds - 10 )
			{
				Video.Position -= TimeSpan.FromSeconds(10);
			}
			else
			{
				Video.Stop();
				Video.Play();
			}
		}

		private void Pause_OnClick(object sender, RoutedEventArgs e)
		{
			if (Video.CanPause)
			{
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
			Video.IsMuted = !Video.IsMuted;
		}

		private void UpdateTheme()
		{
			Application.Current.Resources.MergedDictionaries.Clear();
			Application.Current.Resources.MergedDictionaries.Add( Themes[_currentThemeBe] );
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
			Home.Visibility = Visibility.Hidden;
			PlayList.Visibility = Visibility.Hidden;
			SubsShortcut.Visibility = Visibility.Hidden;
			ChgTheme.Visibility = Visibility.Hidden;
			None5.Visibility = Visibility.Hidden;
			Start.Visibility = Visibility.Hidden;
			ForwStop.Visibility = Visibility.Hidden;
			StepOver.Visibility = Visibility.Hidden;
			StepBack.Visibility = Visibility.Hidden;
			MenuShow.Visibility = Visibility.Hidden;
			none.Visibility = Visibility.Hidden;
			none1.Visibility = Visibility.Hidden;
			none2.Visibility = Visibility.Hidden;
			SavePhoto.Visibility = Visibility.Hidden;
			none4.Visibility = Visibility.Hidden;
			Mute.Visibility = Visibility.Hidden;
			Mouse.OverrideCursor = Cursors.None;
		}

		void timer_ShowBtn()
		{
			Home.Visibility = Visibility.Visible;
			PlayList.Visibility = Visibility.Visible;
			SubsShortcut.Visibility = Visibility.Visible;
			ChgTheme.Visibility = Visibility.Visible;
			None5.Visibility = Visibility.Visible;
			Start.Visibility = Visibility.Visible;
			ForwStop.Visibility = Visibility.Visible;
			StepOver.Visibility = Visibility.Visible;
			StepBack.Visibility = Visibility.Visible;
			MenuShow.Visibility = Visibility.Visible;
			none.Visibility = Visibility.Visible;
			none1.Visibility = Visibility.Visible;
			none2.Visibility = Visibility.Visible;
			SavePhoto.Visibility = Visibility.Visible;
			none4.Visibility = Visibility.Visible;
			Mute.Visibility = Visibility.Visible;
			Mouse.OverrideCursor = Cursors.Arrow;

		}

		private MediaState GetMediaState(MediaElement myMedia)
		{
			FieldInfo hlp = typeof( MediaElement ).GetField( "_helper", BindingFlags.NonPublic | BindingFlags.Instance );
			object helperObject = hlp.GetValue( myMedia );
			FieldInfo stateField = helperObject.GetType().GetField( "_currentState", BindingFlags.NonPublic | BindingFlags.Instance );
			MediaState state = (MediaState)stateField.GetValue( helperObject );
			return state;
		}

	}
}
