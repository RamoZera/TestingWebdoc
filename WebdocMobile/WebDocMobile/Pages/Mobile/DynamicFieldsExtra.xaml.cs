namespace WebDocMobile.Pages.Mobile;

public partial class DynamicFieldsExtra : ContentPage
{
	public DynamicFieldsExtra()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
    }

    private async void HandleClickBackPage(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}