using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;

namespace VPNGET.Views
{
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        public VPNGET.ViewModel.Home_ViewModel ViewModel { get; set; } = new VPNGET.ViewModel.Home_ViewModel();
        //IList<VPNGET.Model.VPN_Model> ViewModel { get; set; }
        public MainPage()
        {
            InitializeComponent();
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        async void ShowWarning()
        {
            ContentDialog cd = new ContentDialog();
            cd.Title = "声明";
            cd.Content = "1.使用本程序请遵守当地法律法规。如带来不良后果皆与本作者及本程序无关。\r\n 2.本程序仅供学习交流使用请勿用做它途。";
            cd.PrimaryButtonText = "拒绝";
            cd.PrimaryButtonClick += (_s, _e) =>
            {
                App.Current.Exit();
                return;
            };
            cd.SecondaryButtonText = "同意条款";
            cd.SecondaryButtonClick += (_s, _e) =>
            {
                new Common.LocalData().SetLocalConfig("IsShow", false);
                new Common.LocalData().SetLocalConfig("Ver", new Common.Common().GetVersionDescription());
            };
            cd.FullSizeDesired = true;
            await cd.ShowAsync();
        }

        async void Bind()
        {
            try
            {
                NetWork.NetWork netWork = new NetWork.NetWork();
                var Result = await netWork.Get();
                if (Result != null)
                {
                    ObservableCollection<VPNGET.Model.VPN_Model> OC = new ObservableCollection<Model.VPN_Model>();
                    foreach (Model.VPN_Model VM in new VPNGET.Common.Common().DeSerializationJson<ObservableCollection<VPNGET.Model.VPN_Model>>(Result.ToString()))
                    {
                        VM.CountryShort = "ms-appx:///Assets/flags/" + VM.CountryShort + ".png";
                        VM.IP = "IP:" + VM.IP;
                        VM.Ping ="延迟:"+ VM.Ping + " ms";
                        OC.Add(VM);
                    }
                    ViewModel.VPNServerList = OC;
                    PR.IsActive = false;
                    //var s = new VPNGET.Common.Common().DeSerializationJson(Result.ToString());
                }
            }
            catch (Exception ex)
            {
                Bind();
            }
        }


        private async void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView lv = sender as ListView;
            VPNGET.Model.VPN_Model vm = (VPNGET.Model.VPN_Model)lv.SelectedItem;
            if (vm == null)
                return;
            ViewModel.SelectVPN = vm;
            DataPackage dp = new DataPackage();
            dp.SetText(vm.IP.Replace("IP:",""));
            Clipboard.SetContent(dp);
            await new Windows.UI.Popups.MessageDialog(vm.IP + "复制到剪切版").ShowAsync();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if ((new Common.LocalData().GetLoaclConfig("IsShow") == null ? true : Convert.ToBoolean(new Common.LocalData().GetLoaclConfig("IsShow"))) || (new Common.Common().GetVersionDescription() != (new Common.LocalData().GetLoaclConfig("Ver") == null ? "" : new Common.LocalData().GetLoaclConfig("Ver").ToString())))
            {
                ShowWarning();
            }
            Bind();
            ViewModel.HCRefresh.ExcuteCommand = (object obj) =>
            {
                PR.IsActive = true;
                Bind();
            };
        }
    }
}
