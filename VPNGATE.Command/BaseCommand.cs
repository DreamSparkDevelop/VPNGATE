using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VPNGATE.Command
{
    public delegate void _Command(object parameter);
    public abstract class BaseCommand : ICommand
    {
        public abstract Boolean _CanExecute(object parameter);
        public abstract void _Execute(object parameter);
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _CanExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _Execute(parameter);
        }
    }
}
