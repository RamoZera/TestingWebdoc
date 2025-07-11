using WebDocMobile.PageModels;

namespace WebDocMobile.Pages.Desktop;

public partial class LoginPageDesktop : ContentPage
{
    public LoginPageDesktop(LoginPageViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
        NavigationPage.SetHasNavigationBar(this, false);
    }
}
