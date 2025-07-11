using WebDocMobile.Pages.Mobile;

namespace WebDocMobile.PageModels.StandardViewModels
{
    public partial class AppShellViewModel : BaseViewModel
    {
        //[ICommand]
        async void SignOut()
        {
            if (Preferences.ContainsKey(nameof(App.UserDetails)))
            {
                Preferences.Remove(nameof(App.UserDetails));
            }
            await Shell.Current.GoToAsync($"//{nameof(LoginPageMobile)}");
        }
    }
}
