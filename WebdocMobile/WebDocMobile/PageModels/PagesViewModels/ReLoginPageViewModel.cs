﻿﻿﻿﻿﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WebDocMobile.Helpers.WsMethods;
using System.Linq;
using WebDocMobile.Models;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Services;

namespace WebDocMobile.PageModels
{
    // This ViewModel is responsible for the logic of the re-login screen (PIN entry).
    public partial class ReLoginPageViewModel : ObservableObject
    {
        // --- Services ---
        // Services are injected via the constructor for testability and maintainability.
        private readonly IAlertService _alertService;
        private readonly IDocumentService _documentService;
        private readonly ISettingsService _settingsService;
        private readonly IAppStateService _appStateService;

        // --- Properties ---
        // The current PIN entered by the user.
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanDeleteDigit))]
        private string pin;

        public ObservableCollection<PinDigitViewModel> PinDigits { get; }

        // The name of the user, displayed on the screen.
        [ObservableProperty]
        private string username;

        // A computed property to determine if the delete button should be enabled.
        public bool CanDeleteDigit => !string.IsNullOrEmpty(Pin);

        // --- Constructor ---
        public ReLoginPageViewModel(IAlertService alertService, IDocumentService documentService, ISettingsService settingsService, IAppStateService appStateService)
        {
            _alertService = alertService;
            _documentService = documentService;
            _settingsService = settingsService;
            _appStateService = appStateService;

            // Load user info from persistent settings and populate the app state
            _appStateService.UserDetails = _settingsService.UserInfo;
            Username = _appStateService.UserDetails?.strName ?? "User";

            PinDigits = new ObservableCollection<PinDigitViewModel>(
                Enumerable.Range(0, 6).Select(_ => new PinDigitViewModel())
            );
            Pin = string.Empty;
        }

        // --- Commands ---
        [RelayCommand]
        private async Task AddDigit(string digit)
        {
            if (Pin?.Length >= 6) return;
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

        [RelayCommand]
        private async Task CheckPinAsync()
        {
            if (_appStateService.UserDetails?.PIN == Pin)
            {
                await GetDocuments();
                await Shell.Current.GoToAsync($"//{nameof(MainMenuPageMobile)}");
            }
            else
            {
                await _alertService.ShowAlert("Error", "Incorrect Pin");
                Pin = string.Empty;
                UpdatePinCircles();
            }
        }

        private async Task GetDocuments()
        {
            var myDocuments = await _documentService.ListMyDocuments(_appStateService.UserDetails.strHashCode, 1000, 1);
            var allDocuments = await _documentService.ListDocuments(_appStateService.UserDetails.strHashCode, 1000, 1);
            var allMyDocuments = await _documentService.ListAllMyDocuments(_appStateService.UserDetails.strHashCode, 1000, 1, "");

            // Use HashSets for efficient lookups (O(1) average time complexity).
            // This is much faster than using List.Any() inside a loop (O(N*M)).
            var allMyDocumentsCodes = allMyDocuments.Select(d => d.Code).ToHashSet();
            var myDocumentsCodes = myDocuments.Select(d => d.Code).ToHashSet();

            // Documents for "Conhecimento" are those in the general list but not assigned to the user.
            var knownDocuments = allDocuments.Where(d => !allMyDocumentsCodes.Contains(d.Code)).ToList();
            // Documents for "Departamento" are those assigned to the user's group but not directly to them.
            var departmentDocuments = allMyDocuments.Where(d => !myDocumentsCodes.Contains(d.Code)).ToList();

            _appStateService.AllDocuments = allDocuments;
            _appStateService.MyDocuments = myDocuments;
            _appStateService.DepartmentDocuments = departmentDocuments;
            _appStateService.KnownDocuments = knownDocuments;
        }

        private void UpdatePinCircles()
        {
            for (int i = 0; i < PinDigits.Count; i++)
            {
                PinDigits[i].IsFilled = i < Pin.Length;
            }
            // Manually notify that the computed property may have changed.
            OnPropertyChanged(nameof(CanDeleteDigit));
        }
    }
}
