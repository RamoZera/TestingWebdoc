using WebDocMobile.PageModels;

namespace WebDocMobile.Pages.Desktop;

public partial class PINPageDesktop : ContentPage
{
	public PINPageDesktop(PINPageViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}