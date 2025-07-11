﻿﻿﻿﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WebDocMobile.Pages.Desktop;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Services;

namespace WebDocMobile.PageModels
{
    public partial class SelectEntityCodePageViewModel : ObservableObject
    {
        private readonly IAlertService _alertService;
        private readonly ISettingsService _settingsService;

        [ObservableProperty]
        private string codigoEntidade;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotLoading))]
        [NotifyPropertyChangedFor(nameof(IsNormalState))]
        private bool isLoading;

        public bool IsNotLoading => !IsLoading;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNormalState))]
        private bool isWrongCode;

        public bool IsNormalState => !IsLoading && !IsWrongCode;

        public SelectEntityCodePageViewModel(IAlertService alertService, ISettingsService settingsService)
        {
            _alertService = alertService;
            _settingsService = settingsService;
        }

        private bool IsEntityCodeValid()
        {
            return CodigoEntidade == "1994" || CodigoEntidade == "1995" || CodigoEntidade?.ToUpper() == "DEMO";
        }

        [RelayCommand]
        private async Task ValidateEntityCodeClicked()
        {
            IsLoading = true;
            IsWrongCode = false;

            if (IsEntityCodeValid() && !string.IsNullOrEmpty(CodigoEntidade))
            {
                switch (CodigoEntidade)
                {
                    case "1994":
                        _settingsService.BaseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:7061" : "https://localhost:7061";
                        _settingsService.CodigoEntidade = CodigoEntidade;
                        break;
                    case "1995":
                        _settingsService.BaseAddress = "http://192.168.1.20:8091";
                        _settingsService.CodigoEntidade = CodigoEntidade;
                        break;
                    case "DEMO":
                        _settingsService.BaseAddress = "https://wsdemo.ambisig.com/api/";
                        _settingsService.CodigoEntidade = CodigoEntidade;
                        break;
                }
#if ANDROID || IOS
                await Shell.Current.GoToAsync($"//{nameof(LoginPageMobile)}?codEntidade={CodigoEntidade}");
#else
                await Shell.Current.GoToAsync($"//{nameof(LoginPageDesktop)}?codEntidade={CodigoEntidade}");
#endif
            }
            else
            {
                IsWrongCode = true;
            }
            IsLoading = false;
        }

        [RelayCommand]
        private async Task HowToObtainEntityCodeClicked()
        {
            await _alertService.ShowAlertAsync("Erro", "Funcionalidade não implementada");
        }

        [RelayCommand]
        private async Task UseQRCodeClicked()
        {
            await _alertService.ShowAlertAsync("Erro", "Funcionalidade não implementada");
        }

        [RelayCommand]
        private void ErroInserirCodigoEntidade()
        {
            IsWrongCode = false;
        }
        [RelayCommand]
        private async Task ErroSicronizarComQrCode()
        {
            await _alertService.ShowAlertAsync("Erro", "Funcionalidade não implementada");
        }
    }
}
