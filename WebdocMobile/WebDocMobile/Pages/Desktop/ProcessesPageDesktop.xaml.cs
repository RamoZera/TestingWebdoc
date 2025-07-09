using WebDocMobile.PageModels;

namespace WebDocMobile.Pages.Desktop;

public partial class ProcessesPageDesktop : ContentPage
{
	public ProcessesPageDesktop(ProcessesPageViewModel viewModel)
	{
		InitializeComponent();
        this.BindingContext = viewModel;
    }
}