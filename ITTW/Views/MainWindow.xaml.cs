using System;
using System.Windows;
using System.Windows.Threading;
using ITTW.Classes;
using ITTW.ViewModels;

namespace ITTW.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private MainWindowViewModel _context;

        private DispatcherTimer _timer;
        
        public MainWindow()
        {
            InitializeComponent();
            Loaded += Setup;
        }

        private void Setup(object sender, EventArgs args)
        {
            if (DataContext is MainWindowViewModel model)
            {
                _context = model;
                _timer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromMilliseconds(1)
                };
                _context.SetTimer(_timer);
//                _timer.Tick += _context.UpdateTimerText;
//                _timer.Start();
            }
        }


        private void timer_Tick(object sender, EventArgs e)
        {
            LabelTimer.Content = DateTime.Now.ToString("hh:mm:ss");
        }
    }
}
