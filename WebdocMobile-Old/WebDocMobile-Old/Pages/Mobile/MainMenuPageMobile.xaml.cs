using WebDocMobile.PageModels;

namespace WebDocMobile.Pages.Mobile;

public partial class MainMenuPageMobile : ContentPage
{
	public MainMenuPageMobile()
	{
		InitializeComponent();

        this.BindingContext = new MainMenuPageViewModel(this.Navigation);

        NavigationPage.SetHasNavigationBar(this, false);
    }

    public async void DocumentsButtonClicked(Object sender, EventArgs e)
    {
        await Task.WhenAny<bool>
        (
            documentsSelected.FadeTo(1, 300),
            processesSelected.FadeTo(0, 300)
        );
    }

    public async void ProcessesButtonClicked(Object sender, EventArgs e)
    {
        await Task.WhenAny<bool>
        (
            documentsSelected.FadeTo(0, 300),
            processesSelected.FadeTo(1, 300)
        );
    }
}