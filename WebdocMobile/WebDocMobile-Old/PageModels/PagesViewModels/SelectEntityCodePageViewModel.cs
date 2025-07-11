using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocMobile.Pages.Desktop;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Services;

namespace WebDocMobile.PageModels
{
    public partial class SelectEntityCodePageViewModel
    {
        private INavigation _navigationService;
        private readonly IAlertService _alertService;
        public string codigoEntidade { get; set; }

        public SelectEntityCodePageViewModel(INavigation navigation)
        {
            this._navigationService = navigation;
            _alertService = new AlertService();
        }

        private bool IsEntityCodeValid()
        {
            return codigoEntidade == "1994" || codigoEntidade == "1995";
        }

        [RelayCommand]
        private async void ValidateEntityCodeClicked()
        {
            App.SE_LoadingScreen = true;
            await Task.Delay(500); //Testing Purposes
            if (IsEntityCodeValid())
            {
                string address = "";
                switch (codigoEntidade)
                {
                    case "1994":
                        if (Preferences.ContainsKey(nameof(App.baseAddress))) { Preferences.Remove(nameof(App.baseAddress)); }
                        if (Preferences.ContainsKey(nameof(App.codigoEntidade))) { Preferences.Remove(nameof(App.codigoEntidade)); }
                        address = DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:7061" : "https://localhost:7061";
                        Preferences.Set(nameof(App.baseAddress), JsonConvert.SerializeObject(address));
                        Preferences.Set(nameof(App.codigoEntidade), JsonConvert.SerializeObject(codigoEntidade));
                        App.baseAddress = address;
                        App.codigoEntidade = codigoEntidade;
                        break;
                    case "1995":
                        if (Preferences.ContainsKey(nameof(App.baseAddress))) { Preferences.Remove(nameof(App.baseAddress)); }
                        if (Preferences.ContainsKey(nameof(App.codigoEntidade))) { Preferences.Remove(nameof(App.codigoEntidade)); }
                        address = "http://192.168.1.20:8091";
                        Preferences.Set(nameof(App.baseAddress), address);
                        Preferences.Set(nameof(App.codigoEntidade), codigoEntidade);
                        App.baseAddress = address;
                        App.codigoEntidade = codigoEntidade;
                        break;
                }
                App.SE_LoadingScreen = false;
                await Task.Delay(400); //Testing Purposes
#if ANDROID || IOS
                var list = _navigationService.NavigationStack;
                int x = 0;
                while (x < list.Count)
                {
                    Page p = list[x];
                    if (!(list[x] is LoginPageMobile))
                    {
                        if (list[x] is SelectEntityCodePageMobile)
                        {
                            _navigationService.InsertPageBefore(new LoginPageMobile(codigoEntidade), list[x]);
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
                    if (!(list[x] is LoginPageDesktop))
                    {
                        if (list[x] is SelectEntityCodePageDesktop)
                        {
                            _navigationService.InsertPageBefore(new LoginPageDesktop(codigoEntidade), list[x]);
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
                //_alertService.ShowAlert("Erro", "Código de Entidade Desconhecido");
                App.SE_LoadingScreen = false;
                App.SE_WrongCode = true;
            }

        }

        [RelayCommand]
        private async void HowToObtainEntityCodeClicked()
        {
            _alertService.ShowAlert("Erro", "Funcionalidade não implementada");
        }

        [RelayCommand]
        private async void UseQRCodeClicked()
        {
            _alertService.ShowAlert("Erro", "Funcionalidade não implementada");
        }

        [RelayCommand]
        private async void ErroInserirCodigoEntidade()
        {
            Preferences.Remove(nameof(App.codigoEntidade));
            App.codigoEntidade = null;
            App.SE_LoadingScreen = false;
            App.SE_WrongCode = false;
        }
        [RelayCommand]
        private async void ErroSicronizarComQrCode()
        {
            _alertService.ShowAlert("Erro", "Funcionalidade não implementada");
        }


    }
}
