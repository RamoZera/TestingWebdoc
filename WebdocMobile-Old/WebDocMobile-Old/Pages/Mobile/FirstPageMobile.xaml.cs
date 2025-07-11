using WebDocMobile.PageModels;

namespace WebDocMobile.Pages.Mobile;

public partial class FirstPageMobile : ContentPage
{
    // The ViewModel is now injected by the DI container
	public FirstPageMobile(FirstPageViewModel viewModel)
	{
        InitializeComponent();
        this.BindingContext = viewModel;
        NavigationPage.SetHasNavigationBar(this, false);
    }
}
