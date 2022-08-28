using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WorkTimer.Commands
{
    class GenericCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public delegate void GenericDelegate();
        private GenericDelegate _delegate;

        public GenericCommand(GenericDelegate d)
        {
            _delegate = d;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _delegate();
        }
    }
}
