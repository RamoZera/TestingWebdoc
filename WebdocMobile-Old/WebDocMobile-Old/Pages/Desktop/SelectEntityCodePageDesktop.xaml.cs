using WebDocMobile.PageModels;

namespace WebDocMobile.Pages.Desktop;

public partial class SelectEntityCodePageDesktop : ContentPage
{
	public SelectEntityCodePageDesktop(SelectEntityCodePageViewModel viewModel)
	{
		InitializeComponent();

        this.BindingContext = viewModel;

        NavigationPage.SetHasNavigationBar(this, false);
    }
}