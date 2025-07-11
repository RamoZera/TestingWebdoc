using Microsoft.Maui.Controls;
using System.Diagnostics;
using WebDocMobile.PageModels;

namespace WebDocMobile.Pages.Mobile;

public partial class FirstPageMobile : ContentPage
{
	public FirstPageMobile()
	{
        InitializeComponent();

        this.BindingContext = new FirstPageViewModel(this.Navigation);

        NavigationPage.SetHasNavigationBar(this, false);
    }

    // temporary button to open Onboarding view
    private async void GoTo_Onboarding(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new OnboardingPage());
    }
}