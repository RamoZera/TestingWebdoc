using CommunityToolkit.Mvvm.Input;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Services;

namespace WebDocMobile.PageModels.StandardViewModels
{
    public partial class AppShellViewModel : BaseViewModel
    {
        private readonly IAppStateService _appStateService;
        private readonly ISettingsService _settingsService;

        // The DI container will not inject services into a parameterless constructor.
        // This constructor is kept for the XAML previewer, but the real work
        // should be done in a constructor that accepts services.
        // For this ViewModel, we will assume it's instantiated by DI.
        public AppShellViewModel(IAppStateService appStateService, ISettingsService settingsService)
        {
            _appStateService = appStateService;
            _settingsService = settingsService;
        }

        [RelayCommand]
        async Task SignOut()
        {
            _settingsService.UserInfo = null;
            _appStateService.ClearAllState();
            await Shell.Current.GoToAsync($"//{nameof(SelectEntityCodePageMobile)}");
        }
    }
}
