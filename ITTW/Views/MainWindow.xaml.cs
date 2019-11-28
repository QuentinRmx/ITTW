using System;
using System.Windows;
using System.Windows.Threading;
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

        /// <summary>
        /// Instantiates the <see cref="DispatcherTimer"/> and passes it to the viewmodel.
        /// It is bound to the <see cref="Window.Loaded"/> event and therefore needs to have standard event parameters.
        /// </summary>
        /// <param name="sender">Object calling this event method. Unused.</param>
        /// <param name="args">The event arguments. Unused.</param>
        private void Setup(object sender, EventArgs args)
        {
            if (DataContext is MainWindowViewModel model)
            {
                _context = model;
                _timer = new DispatcherTimer();
                _context.SetTimer(_timer);
            }
        }
    }
}