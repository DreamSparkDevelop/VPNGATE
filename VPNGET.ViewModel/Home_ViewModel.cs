using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPNGET.ViewModel
{
    public partial class Home_ViewModel : INotifyPropertyChanged
    {
        public Home_ViewModel()
        {
            VPNServerList = new ObservableCollection<VPNGET.Model.VPN_Model>();
            HCRefresh = new VPNGATE.Command.HomeCommand();
            //HCDataPackage = new VPNGATE.Command.HomeCommand();
            SelectVPN = new Model.VPN_Model();
        }

        private ObservableCollection<VPNGET.Model.VPN_Model> vpnServerList;
        public ObservableCollection<VPNGET.Model.VPN_Model> VPNServerList
        {
            get
            {
                return vpnServerList;
            }

            set
            {
                vpnServerList = value;
                if (null != PropertyChanged)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("VPNServerList"));
            }
        }

        public VPNGATE.Command.HomeCommand HCRefresh { get; set; }
        //public VPNGATE.Command.HomeCommand HCDataPackage{ get; set; }

        private VPNGET.Model.VPN_Model selectVPN;
        public VPNGET.Model.VPN_Model SelectVPN
        {
            get
            {
                return selectVPN;
            }
            set
            {
                selectVPN = value;
                if (null != PropertyChanged)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SelectVPN"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

}
