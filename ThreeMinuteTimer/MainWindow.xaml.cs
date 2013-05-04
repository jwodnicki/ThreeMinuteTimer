using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Shell;

namespace ThreeMinuteTimer
{
	public partial class MainWindow : Window
	{
		private Fortune cookie;

		public MainWindow()
		{
			InitializeComponent();
			WindowState = WindowState.Minimized;
		}

		private void eventLoad(object sender, RoutedEventArgs e)
		{
			amrac();
			cookie = new Fortune();
		}

		private void amrac()
		{
			int decisecondsToRun = 1800;

			TaskbarItemInfo.ProgressState = TaskbarItemProgressState.Normal;
			int deciSecondsRemaining = decisecondsToRun;
			var andre = new BackgroundWorker();
			andre.WorkerReportsProgress = true;
			andre.ProgressChanged += (sender, e) => TaskbarItemInfo.ProgressValue = (double)e.ProgressPercentage / 100;
			andre.DoWork += (sender, e) =>
			{
				while (deciSecondsRemaining-- > 0)
				{
					Application.Current.Dispatcher.Invoke(() => textBlock.Text = (deciSecondsRemaining / 10).ToString());
					Thread.Sleep(100);
					int progressPercent = 100 * deciSecondsRemaining / decisecondsToRun;
					andre.ReportProgress(progressPercent);
					if (progressPercent < 33)
					{
						Application.Current.Dispatcher.Invoke(() => TaskbarItemInfo.ProgressState = TaskbarItemProgressState.Error);
					}
					else if (progressPercent < 67)
					{
						Application.Current.Dispatcher.Invoke(() => TaskbarItemInfo.ProgressState = TaskbarItemProgressState.Paused);
					}
				}
			};
			andre.RunWorkerCompleted += (sender, e) =>
			{
				MessageBox.Show(cookie.GetRandom());
				try
				{
					Application.Current.Shutdown();
				}
				catch { }
			};
			andre.RunWorkerAsync();
		}
	}
}
