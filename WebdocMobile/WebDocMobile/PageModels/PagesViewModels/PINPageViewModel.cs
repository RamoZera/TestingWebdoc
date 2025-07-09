﻿﻿﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using WebDocMobile.Models;
using WebDocMobile.Pages.Desktop;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Services;

namespace WebDocMobile.PageModels
{
    // Inherit from ObservableObject for MVVM helpers.
    public partial class PINPageViewModel : ObservableObject
    {
        // --- Properties ---
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanDeleteDigit))]
        private string pin = string.Empty;

        public ObservableCollection<PinDigitViewModel> PinDigits { get; }

        // --- Services ---
        private readonly IAlertService _alertService;
        private readonly ISettingsService _settingsService;

        // --- Constructor ---
        public PINPageViewModel(IAlertService alertService, ISettingsService settingsService)
        {
            _alertService = alertService;
            _settingsService = settingsService;

            // Initialize the collection that the UI will bind to.
            PinDigits = new ObservableCollection<PinDigitViewModel>(
                Enumerable.Range(0, 6).Select(_ => new PinDigitViewModel())
            );
        }

        // --- Computed Properties ---
        public bool CanDeleteDigit => !string.IsNullOrEmpty(Pin);

        // --- Commands ---
        [RelayCommand]
        private async Task AddDigit(string digit)
        {
            if (Pin?.Length >= 6)
            {
                return;
            }

            Pin += digit;
            UpdatePinCircles();

            if (Pin.Length == 6)
            {
                await CheckPinAsync();
            }
        }

        [RelayCommand(CanExecute = nameof(CanDeleteDigit))]
        private void DeleteDigit()
        {
            if (CanDeleteDigit)
            {
                Pin = Pin.Substring(0, Pin.Length - 1);
                UpdatePinCircles();
            }
        }

        private async Task CheckPinAsync()
        {
            // Use the injected service to get user info.
            var userInfo = _settingsService.UserInfo;
            if (userInfo is null)
            {
                await _alertService.ShowAlert("Error", "User session not found. Please log in again.");
                await Shell.Current.GoToAsync($"//{nameof(LoginPageMobile)}");
                return;
            }

            // Scenario 1: First time setting the PIN.
            if (string.IsNullOrEmpty(userInfo.PIN))
            {
                var newUserInfo = userInfo with { PIN = this.Pin };

                // Use the service to save the updated user info.
                _settingsService.UserInfo = newUserInfo;

                await _alertService.ShowAlert("Success", "PIN has been set!");
                await GoToMainMenuAsync();
            }
            // Scenario 2: Verifying an existing PIN.
            else
            {
                // On the initial PIN setup page, if a PIN already exists, we just proceed.
                // A more robust implementation might ask for PIN confirmation.
                await GoToMainMenuAsync();
            }
        }

        [RelayCommand]
        private async Task ConfigureLaterButtonPressed()
        {
            await _alertService.ShowAlert("Warning", "We advise you to have a PIN for the protection of your data and information");
            await GoToMainMenuAsync();
        }

        [RelayCommand]
        private async Task ConfigureBiometricsLaterButtonPressed()
        {
            await _alertService.ShowAlert("Warning", "You should have the biometrics for more security unless it's not implemented :)");
            await GoToMainMenuAsync();
        }

        [RelayCommand]
        private async Task ActivateBiometricsButtonPressed()
        {
            await _alertService.ShowAlert("Error", "Functionality not implemented");
        }

        private Task GoToMainMenuAsync()
        {
            return Shell.Current.GoToAsync($"//{nameof(MainMenuPageMobile)}");
        }

        private void UpdatePinCircles()
        {
            for (int i = 0; i < PinDigits.Count; i++)
            {
                // If the current index is less than the PIN length, the circle should be filled.
                PinDigits[i].IsFilled = i < Pin.Length;
            }
            OnPropertyChanged(nameof(CanDeleteDigit)); // Manually trigger update for the command
        }
    }
}
