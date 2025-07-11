﻿﻿﻿using WebDocMobile.Models;
using WebDocMobile.PageModels.StandardViewModels;
using WebDocMobile.Services;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Pages.Desktop;

namespace WebDocMobile;

public partial class AppShell : Shell
{
    private readonly ISettingsService _settingsService;
	public AppShell(ISettingsService settingsService, AppShellViewModel viewModel)
	{
		InitializeComponent();
        _settingsService = settingsService;
        this.BindingContext = viewModel;

        //Mobile Pages Routing

		Routing.RegisterRoute(nameof(LoginPageMobile), typeof(LoginPageMobile));
        Routing.RegisterRoute(nameof(PINPageMobile), typeof(PINPageMobile));
        Routing.RegisterRoute(nameof(MainMenuPageMobile), typeof(MainMenuPageMobile));
        Routing.RegisterRoute(nameof(DocumentsPageMobile) , typeof(DocumentsPageMobile));
        Routing.RegisterRoute(nameof(ProcessesPageMobile) , typeof(ProcessesPageMobile));
        Routing.RegisterRoute(nameof(FirstPageMobile) , typeof(FirstPageMobile));

        //Desktop Pages Routing

        Routing.RegisterRoute(nameof(LoginPageDesktop), typeof(LoginPageDesktop));
        Routing.RegisterRoute(nameof(PINPageDesktop), typeof(PINPageDesktop));
        Routing.RegisterRoute(nameof(MainMenuPageDesktop), typeof(MainMenuPageDesktop));
        Routing.RegisterRoute(nameof(DocumentsPageDesktop), typeof(DocumentsPageDesktop));
        Routing.RegisterRoute(nameof(ProcessesPageDesktop), typeof(ProcessesPageDesktop));
        Routing.RegisterRoute(nameof(FirstPageDesktop), typeof(FirstPageDesktop));
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

#if ANDROID || IOS
        var targetPage = _settingsService.UserInfo != null ? nameof(ReLoginPageMobile) : nameof(SelectEntityCodePageMobile);
#else
        var targetPage = _settingsService.UserInfo != null ? nameof(ReLoginPageDesktop) : nameof(SelectEntityCodePageDesktop);
#endif
        await Current.GoToAsync($"//{targetPage}");
    }
}
