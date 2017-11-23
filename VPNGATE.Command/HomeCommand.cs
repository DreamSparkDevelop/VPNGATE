using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPNGATE.Command
{
    public class HomeCommand : BaseCommand
    {
        public override bool _CanExecute(object parameter)
        {
            return true;
        }

        public _Command ExcuteCommand;

        public override void _Execute(object parameter)
        {
            ExcuteCommand(parameter);
        }
    }
}
