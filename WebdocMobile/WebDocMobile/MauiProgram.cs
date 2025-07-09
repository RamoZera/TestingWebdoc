﻿﻿﻿﻿﻿﻿﻿// These are 'using' statements that import necessary libraries and namespaces.
using Microsoft.Extensions.Logging;
using WebDocMobile.Pages.Desktop;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Services;
using WebDocMobile.PageModels;
using WebDocMobile.PageModels.StandardViewModels;
using CommunityToolkit.Maui;
using Telerik.Maui.Controls.Compatibility;

namespace WebDocMobile;

// This is the main class that configures and creates your MAUI application.
public static class MauiProgram
{
    // This is the primary method that the system calls to build your app.
	public static MauiApp CreateMauiApp()
	{
        // The builder pattern is used to configure the app in a step-by-step, readable way.
		var builder = MauiApp.CreateBuilder();
		builder
            .UseTelerik() // Initializes the Telerik UI for MAUI component library.
			.UseMauiApp<App>() // Specifies that 'App.xaml.cs' is the main application class.
            .ConfigureTelerik() // Explicitly configure handlers for Telerik compatibility controls.
            .UseMauiCommunityToolkit() // Initializes the .NET MAUI Community Toolkit for extra helpers and behaviors.
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Roboto-Regular.ttf", "RobotoRegular");
                fonts.AddFont("Roboto-Bold.ttf", "RobotoBold");
                fonts.AddFont("Roboto-Light.ttf", "RobotoLight");
                fonts.AddFont("Roboto-Medium.ttf", "RobotoMedium");
			});

        // This is a platform-specific customization. It modifies the default 'Entry' control.
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
        {
            // The '#if ANDROID' directive means this code will only be compiled and run on Android.
#if ANDROID
            // This line removes the default background/underline from Entry controls on Android for a cleaner look.
            handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#endif
        });

        // This code block only runs when the app is built in 'DEBUG' mode (during development).
#if DEBUG
        builder.Logging.AddDebug();
#endif

		// --- Dependency Injection Setup ---
        // This is where you register all your services, pages, and viewmodels with the app's service container.
        // 'AddSingleton' means the app will create only ONE instance of the class and reuse it everywhere it's needed.

		// Registering all the services. Any class can now ask for an 'IDocumentService' and get the same instance of 'DocumentService'.
		builder.Services.AddSingleton<IDocumentService, DocumentService>();
        builder.Services.AddSingleton<IInitService, InitService>();
        builder.Services.AddSingleton<ILoginService, LoginService>();
        builder.Services.AddSingleton<IProcessService, ProcessService>();
        builder.Services.AddSingleton<IWorkflowService, WorkflowService>();
        builder.Services.AddSingleton<IAlertService, AlertService>();
        // Singleton service to hold the application's runtime state.
        builder.Services.AddSingleton<IAppStateService, AppStateService>();
        // Singleton service to abstract and manage device-persistent settings.
        builder.Services.AddSingleton<ISettingsService, SettingsService>();

        // Register the AppShell as a singleton so it can receive dependencies.
        builder.Services.AddSingleton<AppShell>();

        // This is a clever use of conditional compilation to register the correct pages
        // based on whether the app is being built for mobile or desktop.
#if ANDROID || IOS
        // If the target is Android or iOS, register the mobile-specific pages.
        // Pages should be registered as Transient to ensure a new instance is created on each navigation, preventing state issues.
        builder.Services.AddTransient<FirstPageMobile>();
        builder.Services.AddTransient<LoginPageMobile>();
        builder.Services.AddTransient<ReLoginPageMobile>();
        builder.Services.AddTransient<MainMenuPageMobile>();
		builder.Services.AddTransient<DocumentsPageMobile>();
		builder.Services.AddTransient<ProcessesPageMobile>();
		builder.Services.AddTransient<SelectEntityCodePageMobile>();
        builder.Services.AddTransient<PINPageMobile>();
#else
        // Otherwise (for Windows or macOS), register the desktop-specific pages.
               builder.Services.AddTransient<FirstPageDesktop>();
        builder.Services.AddTransient<LoginPageDesktop>();
        builder.Services.AddTransient<ReLoginPageDesktop>();
        builder.Services.AddTransient<MainMenuPageDesktop>();
        builder.Services.AddTransient<DocumentsPageDesktop>();
        builder.Services.AddTransient<ProcessesPageDesktop>();
        builder.Services.AddTransient<SelectEntityCodePageDesktop>();
        builder.Services.AddTransient<PINPageDesktop>();
#endif

        // Registering all the ViewModels. These contain the logic for the pages.
         // Use AddTransient for ViewModels so a new instance is created for each page.
        builder.Services.AddTransient<FirstPageViewModel>();
        builder.Services.AddTransient<LoginPageViewModel>();
        builder.Services.AddTransient<ReLoginPageViewModel>();
        builder.Services.AddTransient<MainMenuPageViewModel>();
        builder.Services.AddTransient<DocumentsPageViewModel>();
        builder.Services.AddTransient<ProcessesPageViewModel>();
        builder.Services.AddTransient<PINPageViewModel>();
        builder.Services.AddTransient<SelectEntityCodePageViewModel>();
        builder.Services.AddTransient<AppShellViewModel>();

        // This finalizes the configuration and returns the fully built application object.
        return builder.Build();
	}
}
