﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Pages.Desktop;

namespace WebDocMobile.PageModels
{
    public partial class FirstPageViewModel : ObservableObject
    {
        // This ViewModel is now much simpler. Its only job is to initiate the login flow.
        // The complex startup logic is now correctly centralized in AppShell.xaml.cs.
        public FirstPageViewModel()
        {
        }

        [RelayCommand]
        private async Task GoToLoginFlow()
        {
            // This command simply starts the user on the correct path.
            // The AppShell handles the initial routing automatically, but this can be used for a manual start button.
#if ANDROID || IOS
            await Shell.Current.GoToAsync($"//{nameof(SelectEntityCodePageMobile)}");
#else
            await Shell.Current.GoToAsync($"//{nameof(SelectEntityCodePageDesktop)}");
#endif
        }

    }
}
