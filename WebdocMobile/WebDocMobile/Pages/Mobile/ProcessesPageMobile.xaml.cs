using WebDocMobile.PageModels;

namespace WebDocMobile.Pages.Mobile;

public partial class ProcessesPageMobile : ContentPage
{
	public ProcessesPageMobile(ProcessesPageViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
    }
}