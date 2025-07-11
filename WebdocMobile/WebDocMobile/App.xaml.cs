using WebDocMobile.Helpers;
using WebDocMobile.Helpers.WsMethods;
using WebDocMobile.Models;
using WebDocMobile.Models.BookService;
using WebDocMobile.Models.DashBoard;
using WebDocMobile.Pages.Mobile;

namespace WebDocMobile;

public partial class App: Application
{
    internal static UserBasicInfo UserDetails { get; set; }
    internal static string BaseAddress { get; set; }
    internal static GetDashBoard DashBoard { get; set; }

    //Global loader and global modal alert
    public static bool SE_Loader = false;
    public static bool SE_ModalError = false;
    public App()
    {
        InitializeComponent();
        //Customize Entry
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(CustomControls.BorderlessEntryC), (handler, view) =>
        {
#if ANDROID
            handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#endif
        });
        UserBasicInfo user;
        try
        {
            user = UserBasicInfo.Load();
        }
        catch
        {
            user = UserBasicInfo.New();
        }
        if(user.IsCorrect(out string baseAddress) == true)
        {
            BaseAddress = baseAddress;
#if ANDROID || IOS
            MainPage = new NavigationPage(new ReLoginPageMobile());
#endif
        }
        else
        {
#if ANDROID || IOS
            MainPage = new NavigationPage(new LoginPageMobile(user.CodEntidade));
#endif
        }
    }
}