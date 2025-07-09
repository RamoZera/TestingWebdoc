using Microsoft.Extensions.Logging;
using WebDocMobile.Pages.Desktop;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Services;
using WebDocMobile.PageModels;
using WebDocMobile.PageModels.StandardViewModels;
using CommunityToolkit.Maui;
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
		builder.Services.AddSingleton<IDocumentService, DocumentService>();
        builder.Services.AddSingleton<IInitService, InitService>();
        builder.Services.AddSingleton<ILoginService, LoginService>();
        builder.Services.AddSingleton<IProcessService, ProcessService>();
        builder.Services.AddSingleton<IWorkflowService, WorkflowService>();
        builder.Services.AddSingleton<IAlertService, AlertService>();

#if ANDROID || IOS
        builder.Services.AddSingleton<FirstPageMobile>();
        builder.Services.AddSingleton<LoginPageMobile>();
        builder.Services.AddSingleton<ReLoginPageMobile>();
        builder.Services.AddSingleton<MainMenuPageMobile>();
		builder.Services.AddSingleton<DocumentsPageMobile>();
		builder.Services.AddSingleton<ProcessesPageMobile>();
		builder.Services.AddSingleton<SelectEntityCodePageMobile>();
#else
        builder.Services.AddSingleton<FirstPageDesktop>();
        builder.Services.AddSingleton<LoginPageDesktop>();
        builder.Services.AddSingleton<ReLoginPageDesktop>();
        builder.Services.AddSingleton<MainMenuPageDesktop>();
        builder.Services.AddSingleton<DocumentsPageDesktop>();
        builder.Services.AddSingleton<ProcessesPageDesktop>();
        builder.Services.AddSingleton<SelectEntityCodePageDesktop>();
#endif

        builder.Services.AddSingleton<FirstPageViewModel>();
        builder.Services.AddSingleton<LoginPageViewModel>();
        builder.Services.AddSingleton<ReLoginPageViewModel>();
        builder.Services.AddSingleton<MainMenuPageViewModel>();
        builder.Services.AddSingleton<DocumentsPageViewModel>();
        builder.Services.AddSingleton<ProcessesPageViewModel>();


        return builder.Build();
	}
}
