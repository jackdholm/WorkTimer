using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmDialogs;

namespace WorkTimer.ViewModels
{
    class SettingsViewModel : ViewModelBase, IModalDialogViewModel
    {
        public int Hours 
        { 
            get { return _hours; }
            set
            {
                _hours = value;
                HoursText = String.Format("{0:D2}", _hours);
            }
        }
        public int Minutes 
        { 
            get { return _minutes; }
            set
            {
                _minutes = value;
                MinutesText = String.Format("{0:D2}", _minutes);
            }
        }
        public int BreakHours 
        { 
            get { return _breakHours; }
            set
            {
                _breakHours = value;
                BreakHoursText = String.Format("{0:D2}", _breakHours);
            }
        }
        public int BreakMinutes 
        { 
            get { return _breakMinutes; }
            set
            {
                _breakMinutes = value;
                BreakMinutesText = String.Format("{0:D2}", _breakMinutes);
            }
        }
        
        public int PauseLimit { get; set; }
        public double BreakRatio { get; set; }

        public bool? DialogResult { get { return _changed; } }
        public string HoursText { get; set; }
        public string MinutesText { get; set; }
        public string BreakHoursText { get; set; }
        public string BreakMinutesText { get; set; }
        public string PauseLimitText { get; set; }
        public string BreakRatioText { get; set; }
        public ICommand Start { get; set; }
        public ICommand Reset { get; set; }
        public bool WasReset { get; set; }
        
        
        private int _hours;
        private int _minutes;
        private int _breakHours;
        private int _breakMinutes;
        private IDialogService _dialogService;
        private bool _changed;

        public SettingsViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            _changed = false;
            Start = new Commands.GenericCommand(StartTimer);
            Reset = new Commands.GenericCommand(ResetTimer);
            Hours = 0;
            Minutes = 0;
            BreakHours = 0;
            BreakMinutes = 0;
        }

        private void StartTimer()
        {
            Hours = Convert.ToInt32(HoursText);
            Minutes = Convert.ToInt32(MinutesText);
            BreakHours = Convert.ToInt32(BreakHoursText);
            BreakMinutes = Convert.ToInt32(BreakMinutesText);
            PauseLimit = Convert.ToInt32(PauseLimitText);
            BreakRatio = Convert.ToDouble(BreakRatioText);
            _changed = true;
            _dialogService.Close(this);
        }

        private void ResetTimer()
        {
            WasReset = true;
            _changed = true;
            _dialogService.Close(this);
        }
    }
}
