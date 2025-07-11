﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using WebDocMobile.Helpers.WsMethods;
using WebDocMobile.Models;
using WebDocMobile.PageModels.PagesViewModels;
using WebDocMobile.Pages.Desktop;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Services;

namespace WebDocMobile.PageModels
{
    public partial class ReLoginPageViewModel : ObservableObject
    {
        private readonly IDocumentService _documentService;
        private readonly IAlertService _alertService;
        private readonly IAppStateService _appStateService;
        private readonly ISettingsService _settingsService;

        [ObservableProperty]
        private string pin = string.Empty;

        public ObservableCollection<PinDigitViewModel> PinDigits { get; } = new();

        public ReLoginPageViewModel(IDocumentService documentService, IAlertService alertService, IAppStateService appStateService, ISettingsService settingsService)
        {
            _documentService = documentService;
            _alertService = alertService;
            _appStateService = appStateService;
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

        private async void PINComplete()
        {
            var userInfo = _settingsService.UserInfo;
            if (userInfo == null)
            {
                // Should not happen, but as a safeguard
                await _alertService.ShowAlertAsync("Erro", "Os dados do utilizador não foram encontrados. Por favor, inicie a sessão novamente.");
                await Shell.Current.GoToAsync($"//{nameof(SelectEntityCodePageMobile)}");
                return;
            }

            if (userInfo.PIN == Pin)
            {
                _appStateService.UserDetails = userInfo;
                await GetDocuments();
#if ANDROID || IOS
                await Shell.Current.GoToAsync($"//{nameof(MainMenuPageMobile)}");
#else
                await Shell.Current.GoToAsync($"//{nameof(MainMenuPageDesktop)}");
#endif
            }
            else
            {
                await _alertService.ShowAlertAsync("Erro", "PIN Incorreto");
                Pin = string.Empty; // Reset PIN
            }
        }

        public async Task GetDocuments()
        {
            if (_appStateService.UserDetails == null) return;

            var myDocuments = await _documentService.ListMyDocuments(_appStateService.UserDetails.strHashCode, 1000, 1);
            var allDocuments = await _documentService.ListDocuments(_appStateService.UserDetails.strHashCode, 1000, 1);
            var allMyDocuments = await _documentService.ListAllMyDocuments(_appStateService.UserDetails.strHashCode, 1000, 1, "");

            var departmentDocuments = new List<GDDocument>();
            var knownDocuments = new List<GDDocument>();

            if (allDocuments != null && allMyDocuments != null)
            {
                knownDocuments = allDocuments.Where(doc => !allMyDocuments.Any(myDoc => myDoc.Code == doc.Code)).ToList();
            }
            if (allMyDocuments != null && myDocuments != null)
            {
                departmentDocuments = allMyDocuments.Where(myDoc => !myDocuments.Any(doc => doc.Code == myDoc.Code)).ToList();
            }

            _appStateService.AllDocuments = allDocuments ?? new List<GDDocument>();
            _appStateService.MyDocuments = myDocuments ?? new List<GDDocument>();
            _appStateService.DepartmentDocuments = departmentDocuments;
            _appStateService.KnownDocuments = knownDocuments;
        }

        [RelayCommand]
        private async Task BiometricButtonPressed()
        {
            await _alertService.ShowAlertAsync("Biometria", "Funcionalidade não implementada.");
        }
    }
}
