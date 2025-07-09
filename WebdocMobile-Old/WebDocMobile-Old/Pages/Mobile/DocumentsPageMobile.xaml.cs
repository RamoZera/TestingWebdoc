using WebDocMobile.PageModels;

namespace WebDocMobile.Pages.Mobile;

public partial class DocumentsPageMobile : ContentPage
{
	public DocumentsPageMobile()
	{
		InitializeComponent();

		this.BindingContext = new DocumentsPageViewModel(this.Navigation);
	}
}