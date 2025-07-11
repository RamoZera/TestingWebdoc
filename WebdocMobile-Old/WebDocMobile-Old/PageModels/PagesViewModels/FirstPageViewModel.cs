﻿using CommunityToolkit.Mvvm.Input;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Pages.Desktop;
using WebDocMobile.Services;

namespace WebDocMobile.PageModels
{
    public partial class FirstPageViewModel
    {
        private readonly IAlertService _alertService;
        private readonly ISettingsService _settingsService;

        public FirstPageViewModel(IAlertService alertService, ISettingsService settingsService)
        {
            _alertService = alertService;
            _settingsService = settingsService;
        }

        [RelayCommand]
        private async Task HandleGoToLogInPageClicked()
        {
            // This logic is now handled by AppShell.xaml.cs on startup.
            // This command might be for a button to manually trigger login.
            if (_settingsService.UserInfo != null)
            {
#if ANDROID || IOS
                await Shell.Current.GoToAsync($"//{nameof(ReLoginPageMobile)}");
#else
                await Shell.Current.GoToAsync($"//{nameof(ReLoginPageDesktop)}");
#endif
            }
            else
            {
#if ANDROID || IOS
                await Shell.Current.GoToAsync($"//{nameof(SelectEntityCodePageMobile)}");
#else
                await Shell.Current.GoToAsync($"//{nameof(SelectEntityCodePageDesktop)}");
#endif
            }
        }
    }
}
