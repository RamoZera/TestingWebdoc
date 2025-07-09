using WebDocMobile.PageModels;

namespace WebDocMobile.Pages.Desktop;

public partial class ProcessesPageDesktop : ContentPage
{
	public ProcessesPageDesktop()
	{
		InitializeComponent();

        this.BindingContext = new ProcessesPageViewModel(this.Navigation);
    }
}