using System.Diagnostics;
using WebDocMobile.PageModels;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Services;

namespace WebDocMobile.Pages.Desktop;

public partial class LoginPageDesktop : ContentPage
{
    public LoginPageDesktop(string codEntidade)
	{
		InitializeComponent();

        this.BindingContext = new LoginPageViewModel(this.Navigation, codEntidade);

        NavigationPage.SetHasNavigationBar(this, false);
    }
}