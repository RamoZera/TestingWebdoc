using WebDocMobile.PageModels;

namespace WebDocMobile.Pages.Mobile;

public partial class OnboardingPage : ContentPage
{
	public OnboardingPage()
	{
		InitializeComponent();
        this.BindingContext = new OnboardingPageModel();

        // Hide the navigation bar for this page
        NavigationPage.SetHasNavigationBar(this, false);
    }
}