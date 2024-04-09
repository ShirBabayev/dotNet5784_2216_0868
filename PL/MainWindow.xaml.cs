using BlApi;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IBl s_bl = BlApi.Factory.Get();
        bool isOpen = true;



        public DateTime Clock
        {
            get { return (DateTime)GetValue(ClockProperty); }
            set { SetValue(ClockProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Clock.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClockProperty =
            DependencyProperty.Register("Clock", typeof(DateTime), typeof(MainWindow));


        DateTime clock { get; set; }
        public MainWindow()
        {
            Clock = s_bl.Clock;
            InitializeComponent();
            RunClock();
        }
        private void BtnManagerWindow_Click(object sender, RoutedEventArgs e)
        {
            new ManagerWindow().Show();
        }

        private void RunClock()
        {
            new Thread(() =>
            {
                while (isOpen)
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(() => { Clock = Clock.AddSeconds(1); });
                    Thread.Sleep(1000);
                }
            }
            ).Start();

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            isOpen = false;
            base.OnClosing(e);
        }

        private void BtnEngineerDetailesWindow_Click(object sender, RoutedEventArgs e)
        {
            new IdentificationWindow().Show();
        }

        private void ChangeClock(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            try
            {

                if (sender is Button btn)
                {
                    switch (btn.Content.ToString())
                    {
                        case "Add Hour":
                            Clock = Clock.AddHours(1);
                            break;
                        case "Add Day":
                            Clock = Clock.AddDays(1);
                            break;
                        case "Add Month":
                            Clock = Clock.AddMonths(1);
                            break;
                        case "Reset":
                            Clock = DateTime.Now;
                            break;

                    }
                    s_bl.Clock = Clock;
                }
            }
            catch { }   
        }
    }
}

