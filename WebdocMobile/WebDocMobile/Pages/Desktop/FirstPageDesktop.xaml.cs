using WebDocMobile.PageModels;

namespace WebDocMobile.Pages.Desktop;

public partial class FirstPageDesktop : ContentPage
{
	public FirstPageDesktop(FirstPageViewModel viewModel)
	{
		InitializeComponent();
        this.BindingContext = viewModel;
        NavigationPage.SetHasNavigationBar(this, false);
    }
}