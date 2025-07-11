

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WebDocMobile.Pages.Mobile;

namespace WebDocMobile.PageModels.PagesViewModels;

internal partial class NavbarViewModel: ObservableObject
{

    private INavigation _navigationService;

    public NavbarViewModel(INavigation navigation)
    {
        this._navigationService = navigation;
    }   

    [RelayCommand]
    public async void CreateDocumentButton()
    {
        await _navigationService.PushAsync(new NewDocumentPageMobile());
    }

    [RelayCommand]
    public async void HandleSignOutButton()
    {
        if (Preferences.ContainsKey(nameof(App.UserDetails)))
        {
            Preferences.Remove(nameof(App.UserDetails));
        }
        #if ANDROID || IOS
                var list = _navigationService.NavigationStack;
                int x = 0;
                while (x < list.Count)
                {
                    Page p = list[x];
                    if (!(list[x] is FirstPageMobile))
                    {
                        if (list[x] is MainMenuPageMobile)
                        {
                            _navigationService.InsertPageBefore(new FirstPageMobile(), list[x]);
                        }
                        _navigationService.RemovePage(p);
                    }
                    else
                    {
                        x++;
                    }
                }
        #endif

    }

    
}
