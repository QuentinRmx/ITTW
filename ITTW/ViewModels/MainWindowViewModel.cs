using System;
using System.ComponentModel.DataAnnotations;
using System.Timers;
using System.Windows.Controls.Ribbon;
using ITTW.Classes;
using Prism.Commands;
using Prism.Mvvm;

namespace ITTW.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "It's time to work'";
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private Timer _timer;

        private int _counter = (int) Config.TimerSpan.TotalSeconds;

        private DateTime _elapsedTime;

        public DateTime ElapsedTime
        {
            get => _elapsedTime;
            set => SetProperty(ref _elapsedTime, value, nameof(TextTimer));
        }


        public DateTime StartedTime { get; set; }

        public DateTime TimeToReach { get; set; }

        private TimeSpan _elapsedSpan;

        public TimeSpan ElapsedSpan
        {
            get => _elapsedSpan;
            set => SetProperty(ref _elapsedSpan, value, nameof(TextTimer));
        }

        public string _textTimer;
        public string TextTimer
        {
            get
            {
                string text; 
                text = $"Remaining time : {ElapsedSpan.Hours:00}:{ElapsedSpan.Minutes:00}:{ElapsedSpan.Seconds:00}";
                return text;
            }
            set => _textTimer = value;
        }

        public DelegateCommand StartTimerCommand => new DelegateCommand(StartTimer);

        private void StartTimer()
        {
            if (_timer == null)
            {
                _timer = new Timer();
            }

            _timer.Interval = 1000;
            _timer.Elapsed += TimerOnElapsed;
            StartedTime = DateTime.Now;
            TimeToReach = DateTime.Now + Config.TimerSpan;
            _timer.Start();
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            _counter--;
            ElapsedSpan = ElapsedSpan.Subtract(new TimeSpan(0, 0, 1));
            if (_counter == 0)
            {
                _timer.Stop();
            }
        }

        public MainWindowViewModel()
        {
            _timer = new Timer();
            ElapsedSpan = Config.TimerSpan;
        }
    }
}
