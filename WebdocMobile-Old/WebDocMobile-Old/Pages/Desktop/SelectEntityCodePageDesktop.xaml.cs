using WebDocMobile.PageModels;

namespace WebDocMobile.Pages.Desktop;

public partial class SelectEntityCodePageDesktop : ContentPage
{
	public SelectEntityCodePageDesktop()
	{
		InitializeComponent();

        this.BindingContext = new SelectEntityCodePageViewModel(this.Navigation);

        NavigationPage.SetHasNavigationBar(this, false);
    }
}