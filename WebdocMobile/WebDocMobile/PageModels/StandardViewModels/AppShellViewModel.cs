﻿﻿﻿using CommunityToolkit.Mvvm.Input;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Services;
using WebDocMobile.PageModels.StandardViewModels;

namespace WebDocMobile.PageModels.StandardViewModels
{
    public partial class AppShellViewModel : BaseViewModel
    {
        private readonly ISettingsService _settingsService;
        private readonly IAppStateService _appStateService;

        public AppShellViewModel(ISettingsService settingsService, IAppStateService appStateService)
        {
            _settingsService = settingsService;
            _appStateService = appStateService;
        }

        [RelayCommand]
        async Task SignOut()
        {
            _settingsService.ClearAllData();
            _appStateService.ClearState();

            await Shell.Current.GoToAsync($"//{nameof(SelectEntityCodePageMobile)}");
        }
    }
}
