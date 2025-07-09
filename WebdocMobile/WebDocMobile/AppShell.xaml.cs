﻿﻿﻿﻿﻿using WebDocMobile.PageModels.StandardViewModels;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Pages.Desktop;
using WebDocMobile.Services;

namespace WebDocMobile;

public partial class AppShell : Shell
{
    private readonly ISettingsService _settingsService;
	public AppShell(ISettingsService settingsService, AppShellViewModel viewModel)
	{
        _settingsService = settingsService;
		InitializeComponent();
        // The ViewModel is now provided by the DI container, fully formed with its own dependencies.
        this.BindingContext = viewModel;

        // Register all the routes the app can navigate to.
		Routing.RegisterRoute(nameof(LoginPageMobile), typeof(LoginPageMobile));
        Routing.RegisterRoute(nameof(PINPageMobile), typeof(PINPageMobile));
        Routing.RegisterRoute(nameof(MainMenuPageMobile), typeof(MainMenuPageMobile));
        Routing.RegisterRoute(nameof(DocumentsPageMobile) , typeof(DocumentsPageMobile));
        Routing.RegisterRoute(nameof(ProcessesPageMobile) , typeof(ProcessesPageMobile));
        Routing.RegisterRoute(nameof(FirstPageMobile) , typeof(FirstPageMobile));
        Routing.RegisterRoute(nameof(ReLoginPageMobile), typeof(ReLoginPageMobile));
        Routing.RegisterRoute(nameof(SelectEntityCodePageMobile), typeof(SelectEntityCodePageMobile));

        // Register Desktop routes
        Routing.RegisterRoute(nameof(LoginPageDesktop), typeof(LoginPageDesktop));
        Routing.RegisterRoute(nameof(PINPageDesktop), typeof(PINPageDesktop));
        Routing.RegisterRoute(nameof(MainMenuPageDesktop), typeof(MainMenuPageDesktop));
        Routing.RegisterRoute(nameof(DocumentsPageDesktop), typeof(DocumentsPageDesktop));
        Routing.RegisterRoute(nameof(ProcessesPageDesktop), typeof(ProcessesPageDesktop));
        Routing.RegisterRoute(nameof(FirstPageDesktop), typeof(FirstPageDesktop));
        Routing.RegisterRoute(nameof(ReLoginPageDesktop), typeof(ReLoginPageDesktop));
        Routing.RegisterRoute(nameof(SelectEntityCodePageDesktop), typeof(SelectEntityCodePageDesktop));
    }

    // This event fires after the Shell is properly loaded and ready for navigation.
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Use the injected settings service to check for user data. This is clean and testable.
        if (_settingsService.UserInfo != null)
        {
            // If so, go directly to the Re-Login page. The "//" prefix makes this the new root of the navigation stack.
            await Current.GoToAsync($"//{nameof(ReLoginPageMobile)}");
        }
        else
        {
            // Otherwise, start at the Entity Code selection page.
            await Current.GoToAsync($"//{nameof(SelectEntityCodePageMobile)}");
        }
    }
}
