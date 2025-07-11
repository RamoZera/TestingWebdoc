using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WebDocMobile.Models;
using WebDocMobile.PageModels.PagesViewModels;
using WebDocMobile.Pages.Desktop;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Services;

namespace WebDocMobile.PageModels
{
    public partial class PINPageViewModel : ObservableObject
    {
        private readonly IAlertService _alertService;
        private readonly ISettingsService _settingsService;

        [ObservableProperty]
        private string pin = string.Empty;

        [ObservableProperty]
        private bool isBiometricsPageVisible;

        public ObservableCollection<PinDigitViewModel> PinDigits { get; } = new();

        public PINPageViewModel(IAlertService alertService, ISettingsService settingsService)
        {
            _alertService = alertService;
            _settingsService = settingsService;

            for (int i = 0; i < 6; i++)
            {
                PinDigits.Add(new PinDigitViewModel());
            }
            UpdatePinCircles();
        }

        private void UpdatePinCircles()
        {
            for (int i = 0; i < PinDigits.Count; i++)
            {
                PinDigits[i].IsFilled = i < Pin.Length;
            }
        }

        partial void OnPinChanged(string value)
        {
            UpdatePinCircles();
            if (Pin.Length == 6)
            {
                PINComplete();
            }
        }

        [RelayCommand]
        private void AddDigit(string digit)
        {
            if (Pin.Length < 6)
            {
                Pin += digit;
            }
        }

        [RelayCommand]
        private void DeleteDigit()
        {
            if (Pin.Length > 0)
            {
                Pin = Pin.Substring(0, Pin.Length - 1);
            }
        }

        private void PINComplete()
        {
            var userInfo = _settingsService.UserInfo;
            if (userInfo == null) return;

            userInfo.PIN = Pin;
            _settingsService.UserInfo = userInfo; // Save the updated user info with the PIN

            IsBiometricsPageVisible = true;
        }

        [RelayCommand]
        private async Task ConfigureLaterButtonPressed()
        {
            await _alertService.ShowAlertAsync("Aviso", "Recomendamos que defina um PIN para a proteção dos seus dados.");
#if ANDROID || IOS
            await Shell.Current.GoToAsync($"//{nameof(MainMenuPageMobile)}");
#else
            await Shell.Current.GoToAsync($"//{nameof(MainMenuPageDesktop)}");
#endif
        }

        [RelayCommand]
        private async Task ReturnButtonPressed()
        {
            if (Shell.Current.Navigation.NavigationStack.Count > 1)
            {
                await Shell.Current.Navigation.PopAsync();
            }
        }

        [RelayCommand]
        private async Task ConfigureBiometricsLaterButtonPressed()
        {
            await _alertService.ShowAlertAsync("Aviso", "Pode ativar a biometria mais tarde nas definições.");
#if ANDROID || IOS
            await Shell.Current.GoToAsync($"//{nameof(MainMenuPageMobile)}");
#else
            await Shell.Current.GoToAsync($"//{nameof(MainMenuPageDesktop)}");
#endif
        }

        [RelayCommand]
        private async Task ActivateBiometricsButtonPressed()
        {
            await _alertService.ShowAlertAsync("Erro", "Funcionalidade não implementada.");
        }
    }
}
