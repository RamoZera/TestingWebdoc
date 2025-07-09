using WebDocMobile.PageModels;

namespace WebDocMobile.Pages.Desktop;

public partial class FirstPageDesktop : ContentPage
{
	public FirstPageDesktop()
	{
		InitializeComponent();

        this.BindingContext = new FirstPageViewModel(this.Navigation);

        NavigationPage.SetHasNavigationBar(this, false);
    }
}