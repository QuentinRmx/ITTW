using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ITTW.Classes
{
    /// <summary>
    /// Encapsulates the System.Windows.Forms.Timer class to gives a way
    /// to have countdown timers.
    /// <see cref="https://stackoverflow.com/a/54119639/8462331"/>.
    /// </summary>
    public class CountDownTimer : IDisposable
    {
        // ATTRIBUTES

        public Action TimeChanged { get; set; }

        public Action CountDownFinished { get; set; }

        private Timer _timer = new Timer();

        public bool IsRunning => _timer.Enabled;

        private DateTime _maxTime = new DateTime(1, 1, 1, 0, 30, 6);

        private DateTime _minTime = new DateTime(1, 1, 1, 0, 0, 0);

        public int TimerInterval
        {
            get => _timer.Interval;
            set => _timer.Interval = value;
        }

        public DateTime TimeLeft { get; private set; }

        private long TimeLeftMs => TimeLeft.Ticks / TimeSpan.TicksPerMillisecond;

        public string TimeLeftString => TimeLeft.ToString("mm:ss");

        public string TimeLeftMsString => TimeLeft.ToString("mm:ss.fff");

        // CONSTRUCTORS

        public CountDownTimer(int min, int sec)
        {
            SetTime(min, sec);
            Init();
        }

        public CountDownTimer(DateTime dateTime)
        {
            SetTime(dateTime);
            Init();
;        }

        // METHODS

        /// <inheritdoc />
        public void Dispose()
        {
            _timer?.Dispose();
        }

        public void Init()
        {

        }

        public void SetTime(DateTime dateTime)
        {
            TimeLeft = _maxTime = dateTime;
            TimeChanged?.Invoke();
        }

        public void SetTime(int min, int sec = 0) => SetTime(new DateTime(1, 1, 1, 0, min, sec));
    }
}
