using System.Diagnostics;
using WebDocMobile.PageModels;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Services;

namespace WebDocMobile.Pages.Desktop;

public partial class LoginPageDesktop : ContentPage
{
    public LoginPageDesktop(LoginPageViewModel viewModel)
	{
		InitializeComponent();
        // The ViewModel is now provided by the DI container, fully formed with its own dependencies.
        this.BindingContext = viewModel;
        NavigationPage.SetHasNavigationBar(this, false);
    }
}