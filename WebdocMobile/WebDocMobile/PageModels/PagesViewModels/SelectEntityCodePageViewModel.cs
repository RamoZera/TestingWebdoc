﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Services;

namespace WebDocMobile.PageModels
{
    // Use ObservableObject for MVVM helpers
    public partial class SelectEntityCodePageViewModel : ObservableObject
    {
        // --- Services ---
        private readonly IAlertService _alertService;
        private readonly ISettingsService _settingsService;

        // --- Properties ---
        [ObservableProperty]
        private string codigoEntidade;

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private bool isWrongCode;

        // --- Constructor ---
        // Receive dependencies via constructor injection
        public SelectEntityCodePageViewModel(IAlertService alertService, ISettingsService settingsService)
        {
            _alertService = alertService;
            _settingsService = settingsService;
        }

        private bool IsEntityCodeValid()
        {
            return CodigoEntidade == "1994" || CodigoEntidade == "1995";
        }

        [RelayCommand]
        private async Task ValidateEntityCode()
        {
            IsLoading = true;
            await Task.Delay(500); // Simulating network delay

            if (IsEntityCodeValid())
            {
                string address = "";
                switch (CodigoEntidade)
                {
                    case "1994":
                        address = DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:7061" : "https://localhost:7061";
                        break;
                    case "1995":
                        address = "http://192.168.1.20:8091";
                        break;
                }

                // Use the settings service to persist the configuration.
                _settingsService.BaseAddress = address;
                _settingsService.CodigoEntidade = CodigoEntidade;

                IsLoading = false;

                // Use modern Shell navigation to go to the Login page, passing the entity code as a parameter.
                // The "//" clears the navigation stack so the user can't go back to this page.
                await Shell.Current.GoToAsync($"//{nameof(LoginPageMobile)}?codEntidade={CodigoEntidade}");
            }
            else
            {
                IsLoading = false;
                IsWrongCode = true;
            }
        }

        [RelayCommand]
        private async Task HowToObtainEntityCode()
        {
            await _alertService.ShowAlert("Info", "Functionality not implemented");
        }

        [RelayCommand]
        private async Task UseQRCode()
        {
            await _alertService.ShowAlert("Info", "Functionality not implemented");
        }

        [RelayCommand]
        private void RetryInsertCode()
        {
            // Clear previous state to allow the user to try again.
            _settingsService.CodigoEntidade = null;
            IsLoading = false;
            IsWrongCode = false;
            CodigoEntidade = string.Empty;
        }
    }
}
