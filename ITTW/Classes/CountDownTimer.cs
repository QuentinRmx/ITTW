using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Threading;

namespace ITTW.Classes
{
    /// <summary>
    /// Encapsulates the System.Windows.Forms.Timer class to gives a way
    /// to have countdown timers.
    /// <see cref="http://stackoverflow.com/a/54119639/8462331"/>.
    /// </summary>
    public class CountDownTimer : IDisposable
    {
        // ATTRIBUTES

        public Action TimeChanged { get; set; }

        public Action CountDownFinished { get; set; }

        private DispatcherTimer _timer;

        public bool IsRunning => _timer.IsEnabled;

        private DateTime _maxTime = new DateTime(1, 1, 1, 0, 30, 6);

        private DateTime _minTime = new DateTime(1, 1, 1, 0, 0, 0);

        public int IntervalMs
        {
            get => _timer.Interval.Seconds;
            set => _timer.Interval = TimeSpan.FromSeconds(value);
        }

        public DateTime TimeLeft { get; private set; }

        private long TimeLeftMs => TimeLeft.Ticks / TimeSpan.TicksPerMillisecond;

        public string TimeLeftString => TimeLeft.ToString("mm:ss");

        public string TimeLeftMsString => TimeLeft.ToString("mm:ss.fff");

        // CONSTRUCTORS

        public CountDownTimer(DispatcherTimer timer, int min, int sec)
        {
            _timer = timer;
            SetTime(min, sec);
            Init();
        }

        public CountDownTimer(DispatcherTimer timer, DateTime dateTime)
        {
            _timer = timer;
            SetTime(dateTime);
            Init();
;        }

        public CountDownTimer(DispatcherTimer timer)
        {
            _timer = timer;
            Init();
        }

        // METHODS

        /// <inheritdoc />
        public void Dispose()
        {
            
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            Pause();
            Reset();
        }

        public void Pause() => _timer.Stop();

        public void Reset()
        {
            TimeLeft = _maxTime;
        }

        public void Init()
        {
            TimeLeft = _maxTime;
            IntervalMs = 1000;
            _timer.Tick += TimerTick;
        }

        public void SetTime(DateTime dateTime)
        {
            TimeLeft = _maxTime = dateTime;
            TimeChanged?.Invoke();
        }

        public void SetTime(int min, int sec = 0) => SetTime(new DateTime(1, 1, 1, 0, min, sec));

        public void TimerTick(object sender, EventArgs e)
        {
            if (TimeLeftMs > _timer.Interval.Milliseconds)
            {
                TimeLeft = TimeLeft.AddMilliseconds(-_timer.Interval.Milliseconds);
                TimeChanged?.Invoke();
            }
            else
            {
                Stop();
                TimeLeft = _minTime;
                TimeChanged?.Invoke();
                CountDownFinished?.Invoke();
            }
        }
    }
}
