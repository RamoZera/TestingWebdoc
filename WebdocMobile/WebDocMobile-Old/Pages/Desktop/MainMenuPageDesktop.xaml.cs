using WebDocMobile.PageModels;

namespace WebDocMobile.Pages.Desktop;

public partial class MainMenuPageDesktop : ContentPage
{
	public MainMenuPageDesktop()
	{
		InitializeComponent();

        this.BindingContext = new MainMenuPageViewModel(this.Navigation);
    }
}