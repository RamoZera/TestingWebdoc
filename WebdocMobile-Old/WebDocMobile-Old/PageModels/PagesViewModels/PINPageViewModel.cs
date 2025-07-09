using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocMobile.Models;
using WebDocMobile.Pages.Desktop;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Services;

namespace WebDocMobile.PageModels
{
    public partial class PINPageViewModel
    {
        private readonly INavigation _navigationService;
        private string currentPIN;
        private int currentDigit;
        private readonly IAlertService _alertService;

        public PINPageViewModel(INavigation navigation)
        {
            this._navigationService = navigation;

            _alertService = new AlertService();

            InitialPINSet();
        }

        private void PINComplete()
        {
            var userInfo = JsonConvert.DeserializeObject<UserBasicInfo>(Preferences.Get(nameof(App.UserDetails), ""));
            App.UserDetails = userInfo;
            if (currentDigit == 6 && currentPIN.Length == 6)
            {
                if(App.UserDetails.PIN == "")
                {
                    var newUserInfo = new UserBasicInfo
                    {
                        strHashCode = userInfo.strHashCode,
                        strName = userInfo.strName,
                        strDomain = userInfo.strDomain,
                        codEntidade = userInfo.codEntidade,
                        PIN = currentPIN,
                        hasBiometricsActive = false
                    };
                    if (Preferences.ContainsKey(nameof(App.UserDetails)))
                    {
                        Preferences.Remove(nameof(App.UserDetails));
                    }
                    string userDetailsStr = JsonConvert.SerializeObject(newUserInfo);
                    Preferences.Set(nameof(App.UserDetails), userDetailsStr);
                    App.UserDetails = newUserInfo;
                }
                else
                {
                    if(App.UserDetails.PIN != currentPIN)
                    {
                        _alertService.ShowAlert("Error", "Incorrect Pin");
                        InitialPINSet();
                        return;
                    }
                }
            }
            else
            {
                InitialPINSet();
            }
        }

        [RelayCommand]
        private void Digit1Pressed()
        {
            currentPIN = currentPIN + "1";
            currentDigit++;
            if(currentDigit == 6) { PINComplete(); }
        }
        [RelayCommand]
        private void Digit2Pressed()
        {
            currentPIN = currentPIN + "2";
            currentDigit++;
            if (currentDigit == 6) { PINComplete(); }
        }
        [RelayCommand]
        private void Digit3Pressed()
        {
            currentPIN = currentPIN + "3";
            currentDigit++;
            if (currentDigit == 6) { PINComplete(); }
        }
        [RelayCommand]
        private void Digit4Pressed()
        {
            currentPIN = currentPIN + "4";
            currentDigit++;
            if (currentDigit == 6) { PINComplete(); }
        }
        [RelayCommand]
        private void Digit5Pressed()
        {
            currentPIN = currentPIN + "5";
            currentDigit++;
            if (currentDigit == 6) { PINComplete(); }
        }
        [RelayCommand]
        private void Digit6Pressed()
        {
            currentPIN = currentPIN + "6";
            currentDigit++;
            if (currentDigit == 6) { PINComplete(); }
        }
        [RelayCommand]
        private void Digit7Pressed()
        {
            currentPIN = currentPIN + "7";
            currentDigit++;
            if (currentDigit == 6) { PINComplete(); }
        }
        [RelayCommand]
        private void Digit8Pressed()
        {
            currentPIN = currentPIN + "8";
            currentDigit++;
            if (currentDigit == 6) { PINComplete(); }
        }
        [RelayCommand]
        private void Digit9Pressed()
        {
            currentPIN = currentPIN + "9";
            currentDigit++;
            if (currentDigit == 6) { PINComplete(); }
        }
        [RelayCommand]
        private void Digit0Pressed()
        {
            currentPIN = currentPIN + "0";
            currentDigit++;
            if (currentDigit == 6) { PINComplete(); }
        }
        [RelayCommand]
        private void DeleteDigitPressed()
        {
            if (currentDigit > 0)
            {
                string newPIN = currentPIN.Substring(0, currentPIN.Length - 1);
                currentPIN = newPIN;
                currentDigit--;
            }
        }
        [RelayCommand]
        private void ConfigureLaterButtonPressed()
        {
#if ANDROID || IOS
            var list = _navigationService.NavigationStack;
            int x = 0;
            while (x < list.Count)
            {
                Page p = list[x];
                if (!(list[x] is MainMenuPageMobile))
                {
                    if (list[x] is PINPageMobile)
                    {
                        _navigationService.InsertPageBefore(new MainMenuPageMobile(), list[x]);
                    }
                    _navigationService.RemovePage(p);
                }
                else
                {
                    x++;
                }
            }
#else
                var list = _navigationService.NavigationStack;
                int x = 0;
                while (x < list.Count)
                {
                    Page p = list[x];
                    if (!(list[x] is MainMenuPageDesktop))
                    {
                        if (list[x] is PINPageDesktop)
                        {
                            _navigationService.InsertPageBefore(new MainMenuPageDesktop(), list[x]);
                        }
                        _navigationService.RemovePage(p);
                    }
                    else
                    {
                        x++;
                    }
                }
#endif
            _alertService.ShowAlert("Warning", "We advice you to have a PIN for the protection of your data and information");
        }
        [RelayCommand]
        private void ReturnButtonPressed()
        {
            if (_navigationService.NavigationStack.Count > 1)
            {
                _navigationService.RemovePage(_navigationService.NavigationStack[_navigationService.NavigationStack.Count - 1]);
            }
        }

        [RelayCommand]
        private void ConfigureBiometricsLaterButtonPressed()
        {
#if ANDROID || IOS
            var list = _navigationService.NavigationStack;
            int x = 0;
            while (x < list.Count)
            {
                Page p = list[x];
                if (!(list[x] is MainMenuPageMobile))
                {
                    if (list[x] is PINPageMobile)
                    {
                        _navigationService.InsertPageBefore(new MainMenuPageMobile(), list[x]);
                    }
                    _navigationService.RemovePage(p);
                }
                else
                {
                    x++;
                }
            }
#else
                var list = _navigationService.NavigationStack;
                int x = 0;
                while (x < list.Count)
                {
                    Page p = list[x];
                    if (!(list[x] is MainMenuPageDesktop))
                    {
                        if (list[x] is PINPageDesktop)
                        {
                            _navigationService.InsertPageBefore(new MainMenuPageDesktop(), list[x]);
                        }
                        _navigationService.RemovePage(p);
                    }
                    else
                    {
                        x++;
                    }
                }
#endif
            _alertService.ShowAlert("Warning", "You should have the biometrics for more security unless it's not implemented :)");
        }
        [RelayCommand]
        private void ActivateBiometricsButtonPressed()
        {
            _alertService.ShowAlert("Error", "Functionality not implemented");
        }

        private void InitialPINSet()
        {
            currentDigit = 0;
            currentPIN = string.Empty;
        }


    }
}
