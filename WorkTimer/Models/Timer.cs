using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Microsoft.Toolkit.Uwp.Notifications;

namespace WorkTimer.Models
{
    class Timer
    {
        public delegate void UpdateHandler(TimeSpan total, TimeSpan countdown, TimeSpan breakTotal, TimeSpan breakCountdown);

        private DispatcherTimer _dispatcherTimer;
        private UpdateHandler _handler;

        private TimeSpan _totalTime;
        private TimeSpan _totalBreakTime;
        private TimeSpan _countdown;
        private TimeSpan _breakCountdown;
        private TimeSpan _originalBreak;
        private TimeSpan _originalCountdown;

        private bool _notificationShown = false;
        private bool _onBreak = false;

        public Timer(UpdateHandler handler)
        {
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += new EventHandler(TimerTick);
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            _handler = handler;
            _totalTime = new TimeSpan(0, 0, 0);
            _totalBreakTime = new TimeSpan(0, 0, 0);
            Set(0, 30, 0, 7);
        }
        public void Set(int hours, int minutes)
        {
            _originalCountdown = new TimeSpan(hours, minutes, 0);
            _countdown = _originalCountdown;
            _handler(_totalTime, _countdown, _totalBreakTime, _breakCountdown);
        }
        public void Set(int mainHours, int mainMinutes, int breakHours, int breakMinutes)
        {
            _originalCountdown = new TimeSpan(mainHours, mainMinutes, 0);
            _originalBreak = new TimeSpan(breakHours, breakMinutes, 0);
            _countdown = _originalCountdown;
            _breakCountdown = _originalBreak;
            _handler(_totalTime, _countdown, _totalBreakTime, _breakCountdown);
        }
        public void Reset()
        {
            _originalCountdown = new TimeSpan(0, 30, 0);
            _originalBreak = new TimeSpan(0, 7, 0);
            _countdown = _originalCountdown;
            _breakCountdown = _originalBreak;
            _totalTime = new TimeSpan(0, 0, 0);
            _totalBreakTime = new TimeSpan(0, 0, 0);

            _handler(_totalTime, _countdown, _totalBreakTime, _breakCountdown);
        }
        public void Start()
        {
            _dispatcherTimer.Start();
        }

        public void Pause()
        {
            _dispatcherTimer.Stop();
        }

        public void Break()
        {
            _onBreak = true;
        }

        public void Stop()
        {
            _dispatcherTimer.Stop();
            _countdown = _originalCountdown;
            _breakCountdown = _originalBreak;
            _handler(_totalTime, _countdown, _totalBreakTime, _breakCountdown);
            _notificationShown = false;
            _onBreak = false;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            TimeSpan second = new TimeSpan(0, 0, 1);
            if (_onBreak)
            {
                _totalBreakTime += second;
                _breakCountdown -= second;
            }
            else
            {
                _totalTime += second;
                _countdown -= second;
            }

            if (!_notificationShown && _countdown.TotalSeconds < 0)
            {
                new ToastContentBuilder()
                    .AddText("Timer Done")
                    .Show();
                _notificationShown = true;
            }
            _handler(_totalTime, _countdown, _totalBreakTime, _breakCountdown);
        }
    }
}
