using System;
using System.Windows.Threading;
using Prism.Commands;
using Prism.Mvvm;

namespace ITTW.ViewModels
{
    /// <summary>
    /// View model of the main window. It manages the timer and all the features the main window has.
    /// </summary>
    public class MainWindowViewModel : BindableBase
    {
        // ATTRIBUTES

        /// <summary>
        /// Title of the window. It's displayed in the system UI.
        /// </summary>
        private string _windowWindowTitle = "It's time to work";

        /// <summary>
        /// Title of the window. Bound to the Window.Title property.
        /// </summary>
        public string WindowTitle
        {
            get => _windowWindowTitle;
            set => SetProperty(ref _windowWindowTitle, value, nameof(WindowTitle));
        }

        /// <summary>
        /// Used to time the current work session. The duration can be changed in the UI.
        /// </summary>
        private DispatcherTimer _timer;

        /// <summary>
        /// Upper limit of the work session duration.
        /// </summary>
        private DateTime _maxTime = new DateTime(1, 1, 1, 0, 30, 6);

        /// <summary>
        /// Time displayed when the timer reaches the limit set by the user.
        /// </summary>
        private readonly DateTime _minTime = new DateTime(1, 1, 1, 0, 0, 0);

        /// <summary>
        /// Time left before the timer is over.
        /// </summary>
        private DateTime _timeLeft = new DateTime();
        
        /// <summary>
        /// Time left in millisecond before the timer is over.
        /// </summary>
        private long TimeLeftMs => _timeLeft.Ticks / TimeSpan.TicksPerMillisecond;

        /// <summary>
        /// Text displayed by the Button TriggerButton in MainWindow.xaml.
        /// </summary>
        private string _textButtonTriggerTimer = "Start";

        /// <summary>
        /// Text displayed by the Button TriggerButton in MainWindow.xaml.
        /// </summary>
        public string TextButtonTriggerTimer
        {
            get => _textButtonTriggerTimer;
            set => SetProperty(ref _textButtonTriggerTimer, value, nameof(TextButtonTriggerTimer));
        }

        /// <summary>
        /// The text displayed for the timer countdown.
        /// </summary>
        private string _textTimer;
        
        /// <summary>
        /// The text displayed for the timer countdown.
        /// It is bound to the LabelTimer Label in MainWindow.xaml.
        /// It is set by the method <see cref="UpdateTimerText"/>.
        /// </summary>
        public string TextTimer
        {
            get => _textTimer;
            set => SetProperty(ref _textTimer, value, nameof(TextTimer));
        }

        /// <summary>
        /// Binds the UI button that start/stop/reset the timer to the actual method.
        /// It is bound to the ButtonTriggerTimer.Command.
        /// </summary>
        public DelegateCommand TriggerTimerCommand => new DelegateCommand(TriggerTimer);

        /// <summary>
        /// Interval between each time the time fires its Tick event.
        /// <seealso cref="DispatcherTimer"/>
        /// <seealso cref="DispatcherTimer.Tick"/>
        /// </summary>
        private const double TimerIntervalMs = 100;

        // CONSTRUCTORS

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public MainWindowViewModel()
        {
            TextButtonTriggerTimer = "Start";
        }

        // METHODS

        /// <summary>
        /// Starts the timer and change the button's text to "Stop".
        /// </summary>
        private void TriggerTimer()
        {
            TextButtonTriggerTimer = "Stop";
            _timer.Start();
        }

        /// <summary>
        /// Called when the timer is over. This method stops the timer, sets the <see cref="_timeLeft"/> property to
        /// <see cref="_minTime"/>, refreshes the timer text and the button text.
        /// </summary>
        private void CountdownFinished()
        {
            _timer.Stop();
            _timeLeft = _minTime;
            UpdateTimerText();
            TextButtonTriggerTimer = "Reset";
        }

        /// <summary>
        /// Initializes the <see cref="_timer"/> to the parameter and sets it up to be used. It sets its interval and
        /// registers the <see cref="Timer_Tick"/> method to its Tick event.
        /// <seealso cref="DispatcherTimer.Tick"/>.
        /// </summary>
        /// <param name="timer"></param>
        public void SetTimer(DispatcherTimer timer)
        {
            _timeLeft = _maxTime = new DateTime(1, 1, 1, 0, 0, 10);
            _timer = timer;
            _timer.Interval = TimeSpan.FromMilliseconds(TimerIntervalMs);
            _timer.Tick += Timer_Tick;
            UpdateTimerText();
        }

        /// <summary>
        /// EMethod for the <see cref="DispatcherTimer"/> Tick event.
        /// </summary>
        /// <param name="sender">Object calling this event method, a DispatcherTimer here.</param>
        /// <param name="e">The event arguments passed by the object.</param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (TimeLeftMs > _timer.Interval.Milliseconds)
            {
                _timeLeft = _timeLeft.AddMilliseconds(-_timer.Interval.Milliseconds);
            }
            else
            {
                CountdownFinished();
            }

            UpdateTimerText();
        }


        /// <summary>
        /// Updates the displayed timer's text.
        /// </summary>
        private void UpdateTimerText()
        {
            TextTimer = $"Remaining time: {_timeLeft:mm:ss.fff}";
        }
    }
}