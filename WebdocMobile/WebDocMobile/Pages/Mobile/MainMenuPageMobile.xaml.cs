using WebDocMobile.PageModels;

namespace WebDocMobile.Pages.Mobile;

public partial class MainMenuPageMobile : ContentPage
{
	public MainMenuPageMobile(MainMenuPageViewModel viewModel)
	{
		InitializeComponent();

        this.BindingContext = viewModel;

        NavigationPage.SetHasNavigationBar(this, false);
    }
}