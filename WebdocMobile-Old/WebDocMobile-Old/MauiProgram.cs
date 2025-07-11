﻿﻿﻿using Microsoft.Extensions.Logging;
using WebDocMobile.Pages.Desktop;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Services;
using WebDocMobile.PageModels;
using WebDocMobile.PageModels.StandardViewModels;
using CommunityToolkit.Maui;
using WebDocMobile.PageModels.PagesViewModels;
using Telerik.Maui.Controls.Compatibility;

namespace WebDocMobile;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
            .UseTelerik()
			.UseMauiApp<App>().UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Roboto-Regular.ttf", "RobotoRegular");
                fonts.AddFont("Roboto-Bold.ttf", "RobotoBold");
                fonts.AddFont("Roboto-Light.ttf", "RobotoLight");
                fonts.AddFont("Roboto-Medium.ttf", "RobotoMedium");
			});

        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
        {
#if ANDROID
            handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#endif
        });

#if DEBUG
        builder.Logging.AddDebug();
#endif

		//Services Singletons
		builder.Services.AddSingleton<IAppStateService, AppStateService>();
		builder.Services.AddSingleton<ISettingsService, SettingsService>();
		builder.Services.AddSingleton<IDocumentService, DocumentService>();
        builder.Services.AddSingleton<IInitService, InitService>();
        builder.Services.AddSingleton<ILoginService, LoginService>();
        builder.Services.AddSingleton<IProcessService, ProcessService>();
        builder.Services.AddSingleton<IWorkflowService, WorkflowService>();
        builder.Services.AddSingleton<IAlertService, AlertService>();
        builder.Services.AddSingleton<AppShell>();

#if ANDROID || IOS
        builder.Services.AddTransient<FirstPageMobile>();
        builder.Services.AddTransient<LoginPageMobile>();
        builder.Services.AddTransient<ReLoginPageMobile>();
        builder.Services.AddTransient<MainMenuPageMobile>();
		builder.Services.AddTransient<DocumentsPageMobile>();
		builder.Services.AddTransient<ProcessesPageMobile>();
		builder.Services.AddTransient<SelectEntityCodePageMobile>();
        builder.Services.AddTransient<PINPageMobile>();
#else
        builder.Services.AddTransient<FirstPageDesktop>();
        builder.Services.AddTransient<LoginPageDesktop>();
        builder.Services.AddTransient<ReLoginPageDesktop>();
        builder.Services.AddTransient<MainMenuPageDesktop>();
        builder.Services.AddTransient<DocumentsPageDesktop>();
        builder.Services.AddTransient<ProcessesPageDesktop>();
        builder.Services.AddTransient<SelectEntityCodePageDesktop>();
        builder.Services.AddTransient<PINPageDesktop>();
#endif

        builder.Services.AddTransient<FirstPageViewModel>();
        builder.Services.AddTransient<LoginPageViewModel>();
        builder.Services.AddTransient<ReLoginPageViewModel>();
        builder.Services.AddTransient<MainMenuPageViewModel>();
        builder.Services.AddTransient<DocumentsPageViewModel>();
        builder.Services.AddTransient<ProcessesPageViewModel>();
        builder.Services.AddTransient<SelectEntityCodePageViewModel>();
        builder.Services.AddTransient<PINPageViewModel>();
        builder.Services.AddTransient<AppShellViewModel>();


        return builder.Build();
	}
}
