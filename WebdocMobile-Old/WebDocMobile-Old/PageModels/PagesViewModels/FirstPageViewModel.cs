using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Pages.Desktop;
using WebDocMobile.Services;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using WebDocMobile.Models;

namespace WebDocMobile.PageModels
{
    public partial class FirstPageViewModel
    {
        private AlertService _alertService;
        private INavigation _navigationService;
        public FirstPageViewModel(INavigation navigation)
        {
            this._navigationService = navigation;
            this._alertService = new AlertService();
        }

        //This code has to go to the App.xaml.cs in the Constructor
        [RelayCommand]
        private async void HandleGoToLogInPageClicked()
        {
            if (Preferences.ContainsKey(nameof(App.UserDetails)) && Preferences.ContainsKey(nameof(App.baseAddress)))
            {
                var address = JsonConvert.DeserializeObject<string>(Preferences.Get(nameof(App.baseAddress), ""));
                var userInfo = JsonConvert.DeserializeObject<UserBasicInfo>(Preferences.Get(nameof(App.UserDetails), ""));
                App.UserDetails = userInfo;
                App.baseAddress = address;
#if ANDROID || IOS
                await _navigationService.PushAsync(new ReLoginPageMobile());
#else
                await _navigationService.PushAsync(new ReLoginPageDesktop());
#endif
            }
            else
            {
                _alertService.ShowAlert("Warning", "There is no previous login");
#if ANDROID || IOS
                await _navigationService.PushAsync(new SelectEntityCodePageMobile());
#else
                await _navigationService.PushAsync(new SelectEntityCodePageDesktop());
#endif
            }
        }

    }
}
