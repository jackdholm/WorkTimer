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
        public delegate void UpdateHandler(TimeSpan total, TimeSpan countdown);

        private DispatcherTimer _dispatcherTimer;
        private UpdateHandler _handler;
        private TimeSpan _totalTime;
        private TimeSpan _countdown;
        private TimeSpan _originalCountdown;
        private bool _notificationShown = false;

        public Timer(UpdateHandler handler)
        {
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += new EventHandler(TimerTick);
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            _handler = handler;
            _totalTime = new TimeSpan(0, 0, 0);
            Set(0, 30);
        }
        public void Set(int hours, int minutes)
        {
            _originalCountdown = new TimeSpan(hours, minutes, 0);
            _countdown = _originalCountdown;
            _handler(_totalTime, _countdown);
        }
        public void Start()
        {
            _dispatcherTimer.Start();
        }

        public void Pause()
        {
            _dispatcherTimer.Stop();
        }

        public void Stop()
        {
            _dispatcherTimer.Stop();
            _countdown = _originalCountdown;
            _handler(_totalTime, _countdown);
            _notificationShown = false;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            _totalTime += new TimeSpan(0, 0, 1);
            _countdown -= new TimeSpan(0, 0, 1);

            if (!_notificationShown && _countdown.TotalSeconds < 0)
            {
                new ToastContentBuilder()
                    .AddText("Timer Done")
                    .Show();
                _notificationShown = true;
            }
            _handler(_totalTime, _countdown);
        }
    }
}
