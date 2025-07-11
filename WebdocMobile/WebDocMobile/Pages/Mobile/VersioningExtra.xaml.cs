namespace WebDocMobile.Pages.Mobile;

public partial class VersioningExtra : ContentPage
{
	public VersioningExtra()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
    }

    private async void HandleClickBackPage(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void HandleClickApagarMovimento(object sender, EventArgs e)
    {
        await Task.WhenAny<bool>
        (
            MainContent.FadeTo(0.09, 200),
            ModalAtentionAlert.FadeTo(1, 200)
        );
        ModalAtentionAlert.IsVisible = true;
    }

    private async void HandleClickVoltaRegisto(object sender, EventArgs e)
    {
        await Task.WhenAny<bool>
        (
            MainContent.FadeTo(1, 200),
            ModalAtentionAlert.FadeTo(0, 200)
        );
        ModalAtentionAlert.IsVisible = false;
    }
}