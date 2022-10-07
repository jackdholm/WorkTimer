using MvvmDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WorkTimer.ViewModels
{
    class TimerViewModel : ViewModelBase
    {
        public string TotalTime { get; set; }
        public string Countdown { get; set; }
        public string TotalBreakTime { get; set; }
        public string BreakCountdown { get; set; }
        public string StartButtonName { get; set; }
        public bool BreakClickable { get; set; }

        public ICommand StartOrPause { get; set;}
        public ICommand Break { get; set; }
        public ICommand Stop { get; set;}
        public ICommand OpenSettings { get; set; }

        private DialogService _dialogService;
        private bool _paused = true;
        private Models.Timer _timer;

        public TimerViewModel()
        {
            _dialogService = new DialogService(null, new Utilities.Locator(), null);
            StartButtonName = "Start";
            _timer = new Models.Timer(UpdateTimers, StartOrPauseTimer);
            StartOrPause = new Commands.GenericCommand(StartOrPauseTimer);
            Break = new Commands.GenericCommand(BreakTimer);
            Stop = new Commands.GenericCommand(StopTimer);
            OpenSettings = new Commands.GenericCommand(OpenSettingsWindow);
            BreakClickable = true;
        }

        private void UpdateTimers(TimeSpan total, TimeSpan countdown, TimeSpan breakTotal, TimeSpan breakCountdown)
        {
            string sign = "";
            if (countdown.TotalSeconds < 0)
            {
                countdown = countdown.Duration();
                sign = "-";
            }
            Countdown = sign + String.Format("{0:D2}:{1:D2}:{2:D2}", countdown.Hours, countdown.Minutes, countdown.Seconds);
            TotalTime = String.Format("{0:D2}:{1:D2}:{2:D2}", total.Hours, total.Minutes, total.Seconds);
            BreakCountdown = String.Format("{0:D2}:{1:D2}:{2:D2}", breakCountdown.Hours, breakCountdown.Minutes, breakCountdown.Seconds);
            TotalBreakTime = String.Format("{0:D2}:{1:D2}:{2:D2}", breakTotal.Hours, breakTotal.Minutes, breakTotal.Seconds);
            OnPropertyChanged("Countdown");
            OnPropertyChanged("TotalTime");
            OnPropertyChanged("BreakCountdown");
            OnPropertyChanged("TotalBreakTime");
        }

        private void StartOrPauseTimer()
        {
            if (_paused)
            {
                StartButtonName = "Pause";
                _timer.Start();
                _paused = false;
            }
            else
            {
                StartButtonName = "Start";
                _timer.Pause();
                _paused = true;
            }
            OnPropertyChanged("StartButtonName");
        }

        private void BreakTimer()
        {
            _timer.Break();
            BreakClickable = false;
            OnPropertyChanged("BreakClickable");
        }

        private void StopTimer()
        {
            StartButtonName = "Start";
            BreakClickable = true;
            _paused = true;
            _timer.Stop();
            OnPropertyChanged("StartButtonName");
            OnPropertyChanged("BreakClickable");
        }

        private void OpenSettingsWindow()
        {
            ViewModels.SettingsViewModel vm = new ViewModels.SettingsViewModel(_dialogService);
            if (_dialogService.ShowDialog(this, vm) == true)
            {
                _timer.Set(vm.Hours, vm.Minutes, vm.BreakHours, vm.BreakMinutes);
                _timer.PauseLimit = vm.PauseLimit;
                if (vm.WasReset)
                    _timer.Reset();
            }
        }
    }
}
