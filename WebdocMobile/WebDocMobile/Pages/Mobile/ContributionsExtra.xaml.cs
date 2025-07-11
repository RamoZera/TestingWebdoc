namespace WebDocMobile.Pages.Mobile;

public partial class ContributionsExtra : ContentPage
{
	public ContributionsExtra()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
    }

    private async void HandleClickBackPage(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void HandleClickRemoverContributo(object sender, EventArgs e)
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
        ModalAddContribution.IsVisible = false;
    }

    private async void HandleClickAddContribution(object sender, EventArgs e)
    {
        await Task.WhenAny<bool>
        (
            MainContent.FadeTo(0.09, 200),
            ModalAddContribution.FadeTo(1, 200)
        );
        ModalAddContribution.IsVisible = true;
    }
}