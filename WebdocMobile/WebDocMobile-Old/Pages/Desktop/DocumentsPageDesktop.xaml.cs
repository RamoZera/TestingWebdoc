using WebDocMobile.PageModels;

namespace WebDocMobile.Pages.Desktop;

public partial class DocumentsPageDesktop : ContentPage
{
	public DocumentsPageDesktop()
	{
		InitializeComponent();

        this.BindingContext = new DocumentsPageViewModel(this.Navigation);
    }
}