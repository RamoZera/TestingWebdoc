using WebDocMobile.PageModels;

namespace WebDocMobile.Pages.Mobile;

public partial class ReLoginPageMobile : ContentPage
{
    public ReLoginPageMobile(ReLoginPageViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
        NavigationPage.SetHasNavigationBar(this, false);
    }
}
