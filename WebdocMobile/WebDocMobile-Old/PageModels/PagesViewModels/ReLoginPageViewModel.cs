using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocMobile.Helpers.WsMethods;
using WebDocMobile.Models;
using WebDocMobile.Pages.Desktop;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Services;

namespace WebDocMobile.PageModels
{
    public partial class ReLoginPageViewModel
    {
        private readonly INavigation _navigationService;
        private readonly IDocumentService _documentService;
        private readonly IAlertService _alertService;

        private string currentPIN;
        private int currentDigit;

        public ReLoginPageViewModel(INavigation navigationService)
        {
            _navigationService = navigationService;
            _alertService = new AlertService();
            _documentService = new DocumentService();
            InitialPINSet();
            BiometricButtonPressed();
        }

        public void Void() { return; }

        public async Task GetDocuments(IDocumentService _documentService)
        {
            var myDocuments = await _documentService.ListMyDocuments(App.UserDetails.strHashCode, 1000, 1);
            var allDocuments = await _documentService.ListDocuments(App.UserDetails.strHashCode, 1000, 1);
            var allMyDocuments = await _documentService.ListAllMyDocuments(App.UserDetails.strHashCode, 1000, 1, "");

            var departmentDocuments = new List<GDDocument>();
            var knownDocuments = new List<GDDocument>();


            foreach(GDDocument document in allDocuments)
            {
                bool isKnownDoc = true;
                foreach(GDDocument myDoc in allMyDocuments)
                {
                    if(document.Code == myDoc.Code)
                    {
                        isKnownDoc = false;
                    }
                }
                if(isKnownDoc)
                {
                    knownDocuments.Add(document);
                }
            }
            foreach(GDDocument document in allMyDocuments)
            {
                bool isDepDoc = true;
                foreach(GDDocument doc in myDocuments)
                {
                    if(document.Code == doc.Code)
                    {
                        isDepDoc = false;
                    }
                }
                if (isDepDoc)
                {
                    departmentDocuments.Add(document);
                }
            }

            if (Preferences.ContainsKey(nameof(App.allDocuments)) || Preferences.ContainsKey(nameof(App.myDocuments)) ||
                Preferences.ContainsKey(nameof(App.departmentDocuments)) || Preferences.ContainsKey(nameof(App.knownDocuments)))
            {
                Preferences.Remove(nameof(App.allDocuments));
                Preferences.Remove(nameof(App.myDocuments));
                Preferences.Remove(nameof(App.departmentDocuments));
                Preferences.Remove(nameof(App.knownDocuments));
            }
            Preferences.Set(nameof(App.allDocuments), JsonConvert.SerializeObject(allDocuments));
            Preferences.Set(nameof(App.myDocuments), JsonConvert.SerializeObject(myDocuments));
            Preferences.Set(nameof(App.departmentDocuments), JsonConvert.SerializeObject(departmentDocuments));
            Preferences.Set(nameof(App.knownDocuments), JsonConvert.SerializeObject(knownDocuments));
            App.allDocuments = allDocuments;
            App.myDocuments = myDocuments;
            App.departmentDocuments = departmentDocuments;
            App.knownDocuments = knownDocuments;
        }

        private async void PINComplete()
        {
            var userInfo = JsonConvert.DeserializeObject<UserBasicInfo>(Preferences.Get(nameof(App.UserDetails), ""));
            App.UserDetails = userInfo;
            if (currentDigit == 6 && currentPIN.Length == 6)
            {
                if (App.UserDetails.PIN == "")
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
                    if (App.UserDetails.PIN != currentPIN)
                    {
                        _alertService.ShowAlert("Error", "Incorrect Pin");
                        InitialPINSet();
                        return;
                    }
                }
#if ANDROID || IOS
                await GetDocuments(_documentService);
                Void();
                var list = _navigationService.NavigationStack;
                int x = 0;
                while (x < list.Count)
                {
                    Page p = list[x];
                    if (!(list[x] is MainMenuPageMobile))
                    {
                        if (list[x] is ReLoginPageMobile)
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
                    if (!(list[x] is FirstPageDesktop))
                    {
                        if (list[x] is ReLoginPageDesktop)
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
            if (currentDigit == 6) { PINComplete(); }
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
        private void BiometricButtonPressed()
        {
            //_alertService.ShowAlert("Biometric", "This will be the biometric");
        }

        private void InitialPINSet()
        {
            currentDigit = 0;
            currentPIN = string.Empty;
        }
    }
}
