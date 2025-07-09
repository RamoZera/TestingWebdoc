using CommunityToolkit.Maui;
using WebDocMobile.Models;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Pages.Desktop;
using WebDocMobile.PageModels;
using WebDocMobile.Services;
using WebDocMobile.Helpers.WsMethods;
using Newtonsoft.Json;

namespace WebDocMobile;

public partial class App : Application
{
    public static UserBasicInfo UserDetails;
    public static string baseAddress;
    public static string codigoEntidade;
    public static List<GDDocument> allDocuments;
    public static List<GDDocument> myDocuments;
    public static List<GDDocument> departmentDocuments;
    public static List<GDDocument> knownDocuments;

    //Select Entity
    public static bool SE_LoadingScreen = false;
    public static bool SE_WrongCode = false;
	public App()
	{
		InitializeComponent();
        if(Preferences.ContainsKey(nameof(App.UserDetails)) && 
            Preferences.ContainsKey(nameof(App.codigoEntidade)) && 
            Preferences.ContainsKey(nameof(App.baseAddress)))
        {
#if ANDROID || IOS
            MainPage = new NavigationPage(new ReLoginPageMobile());
#else
            MainPage = new NavigationPage(new ReLoginPageDesktop());
#endif
        }
        else
        {
#if ANDROID || IOS
            MainPage = new NavigationPage(new SelectEntityCodePageMobile());
#else
            MainPage = new NavigationPage(new SelectEntityCodePageDesktop());
#endif
        }
    }
}
