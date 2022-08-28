using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTimer.Utilities
{
    class Locator : MvvmDialogs.DialogTypeLocators.IDialogTypeLocator
    {
        public Type Locate(INotifyPropertyChanged viewModel)
        {
            Type t = viewModel.GetType();
            if (t == typeof(ViewModels.SettingsViewModel))
                return typeof(Views.Settings);
            else
                return null;
        }
    }
}
