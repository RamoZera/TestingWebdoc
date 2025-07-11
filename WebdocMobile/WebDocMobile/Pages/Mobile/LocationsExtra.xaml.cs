namespace WebDocMobile.Pages.Mobile;

public partial class LocationsExtra : ContentPage
{
	public LocationsExtra()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
    }

    private async void HandleClickBackPage(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}