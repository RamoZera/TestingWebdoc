using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebDocMobile.Helpers;
using WebDocMobile.Models;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Services;

namespace WebDocMobile.PageModels
{
    public partial class PINPageViewModel : ObservableObject
    {
        private readonly INavigation _navigationService;
        private string currentPIN;
        private int currentDigit;
        private readonly IAlertService _alertService;
        [ObservableProperty]
        public bool gbLoader;

        public PINPageViewModel(INavigation navigation)
        {
            _navigationService = navigation;
            _alertService = new AlertService();
            InitialPINSet();
        }

        private async void PINComplete()
        {
            if (currentDigit == 6 && currentPIN.Length == 6)
            {
                GbLoader = true;
                await Task.Delay(100);
                App.UserDetails.PIN = currentPIN;
                App.UserDetails.Save();
                await _navigationService._PushAsyncWithCleanup(new MainMenuPageMobile());
            }
            else
            {
                InitialPINSet();
            }
        }

        [RelayCommand]
        private void Digit1Pressed()
        {
            currentPIN += "1";
            currentDigit++;
            if (currentDigit == 6) { PINComplete(); }
        }
        [RelayCommand]
        private void Digit2Pressed()
        {
            currentPIN += "2";
            currentDigit++;
            if (currentDigit == 6) { PINComplete(); }
        }
        [RelayCommand]
        private void Digit3Pressed()
        {
            currentPIN += "3";
            currentDigit++;
            if (currentDigit == 6) { PINComplete(); }
        }
        [RelayCommand]
        private void Digit4Pressed()
        {
            currentPIN += "4";
            currentDigit++;
            if (currentDigit == 6) { PINComplete(); }
        }
        [RelayCommand]
        private void Digit5Pressed()
        {
            currentPIN += "5";
            currentDigit++;
            if (currentDigit == 6) { PINComplete(); }
        }
        [RelayCommand]
        private void Digit6Pressed()
        {
            currentPIN += "6";
            currentDigit++;
            if (currentDigit == 6) { PINComplete(); }
        }
        [RelayCommand]
        private void Digit7Pressed()
        {
            currentPIN += "7";
            currentDigit++;
            if (currentDigit == 6) { PINComplete(); }
        }
        [RelayCommand]
        private void Digit8Pressed()
        {
            currentPIN += "8";
            currentDigit++;
            if (currentDigit == 6) { PINComplete(); }
        }
        [RelayCommand]
        private void Digit9Pressed()
        {
            currentPIN += "9";
            currentDigit++;
            if (currentDigit == 6) { PINComplete(); }
        }
        [RelayCommand]
        private void Digit0Pressed()
        {
            currentPIN += "0";
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
        private async void ConfigureLaterButtonPressed()
        {
            GbLoader = true;
            await Task.Delay(100);
            await _navigationService._PushAsyncWithCleanup(new MainMenuPageMobile());
            //_alertService.ShowAlert("Warning", "We advice you to have a PIN for the protection of your data and information");
        }
        [RelayCommand]

        private void InitialPINSet()
        {
            currentDigit = 0;
            currentPIN = string.Empty;
        }
    }
}
