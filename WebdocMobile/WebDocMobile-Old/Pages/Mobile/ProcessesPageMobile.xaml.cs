using WebDocMobile.PageModels;

namespace WebDocMobile.Pages.Mobile;

public partial class ProcessesPageMobile : ContentPage
{
	public ProcessesPageMobile()
	{
		InitializeComponent();

		this.BindingContext = new ProcessesPageViewModel(this.Navigation);
    }
}