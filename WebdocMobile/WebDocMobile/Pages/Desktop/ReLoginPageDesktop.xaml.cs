using WebDocMobile.PageModels;

namespace WebDocMobile.Pages.Desktop;

public partial class ReLoginPageDesktop : ContentPage
{
	public ReLoginPageDesktop(ReLoginPageViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}