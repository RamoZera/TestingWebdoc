using WebDocMobile.PageModels.PagesViewModels;

namespace WebDocMobile.Pages.Desktop;

public partial class MainMenuPageDesktop : ContentPage
{
	public MainMenuPageDesktop(MainMenuPageViewModel viewModel)
	{
		InitializeComponent();
        this.BindingContext = viewModel;
    }
}