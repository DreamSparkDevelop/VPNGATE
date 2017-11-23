using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace VPNGET.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class StartPage : Page
    {
        public StartPage()
        {
            this.InitializeComponent();
        }

        async void WLAN()
        {
            /*
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                await new Windows.UI.Popups.MessageDialog("当前设备没有联接网络！", "提示").ShowAsync();
                return;
            }
            */
            Windows.Networking.Connectivity.ConnectionProfile cp = Windows.Networking.Connectivity.NetworkInformation.GetInternetConnectionProfile();
            if (cp == null)
            {
                await new Windows.UI.Popups.MessageDialog("当前设备没有联接网络！", "提示").ShowAsync();
                PR.IsActive = false;
                return;
            }
            if (cp.IsWwanConnectionProfile)
            {
                ContentDialog dialog = new ContentDialog()
                {
                    Title = "提示",
                    Content = "当前使用的是蜂窝网络数据，可能会产生额外的费用。",
                    PrimaryButtonText = "确定",
                    SecondaryButtonText = "取消",
                    FullSizeDesired = false
                };
                dialog.PrimaryButtonClick += (sender, args) =>
                {
                    //Visbale = Windows.UI.Xaml.Visibility.Collapsed;
                    //(Windows.UI.Xaml.Window.Current.Content as Windows.UI.Xaml.Controls.Frame).Navigate(typeof(WinMain.MainPage));
                    var frame = Window.Current.Content as Frame;
                    if (frame != null)
                    {
                        PR.IsActive = false;
                        frame.Navigate(typeof(MainPage));
                    }
                    //this.Frame.Navigate(typeof(WinMain.MainPage));
                };
                dialog.SecondaryButtonClick += (sender, args) =>
                {
                    App.Current.Exit();
                    return;
                };

                await dialog.ShowAsync();
            }
            else if (cp.IsWlanConnectionProfile)
            {
                //Visbale = Windows.UI.Xaml.Visibility.Collapsed;

                //(Windows.UI.Xaml.Window.Current.Content as Windows.UI.Xaml.Controls.Frame).Navigate(typeof(WinMain.MainPage));
                //this.Frame.Navigate(typeof(WinMain.MainPage));

                var frame = Window.Current.Content as Frame;
                if (frame != null)
                {
                    PR.IsActive = false;
                    frame.Navigate(typeof(MainPage));
                }
            }
            else
            {
                await new Windows.UI.Popups.MessageDialog("未知的网络", "网络错误").ShowAsync();
                App.Current.Exit();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
            WLAN();
            //Frame.Navigate(typeof(WinMain.MainPage));
        }
    }
}
