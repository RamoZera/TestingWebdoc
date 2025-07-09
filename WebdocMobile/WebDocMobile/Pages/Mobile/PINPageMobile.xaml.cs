using WebDocMobile.PageModels;

namespace WebDocMobile.Pages.Mobile;

public partial class PINPageMobile : ContentPage
{
	public PINPageMobile(PINPageViewModel viewModel)
	{
		InitializeComponent();
        this.BindingContext = viewModel;
        NavigationPage.SetHasNavigationBar(this, false);
    }
}