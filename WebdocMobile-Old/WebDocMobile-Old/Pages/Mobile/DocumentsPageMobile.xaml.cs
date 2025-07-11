using WebDocMobile.PageModels;

namespace WebDocMobile.Pages.Mobile;

public partial class DocumentsPageMobile : ContentPage
{
	public DocumentsPageMobile(DocumentsPageViewModel viewModel)
	{
		InitializeComponent();

		this.BindingContext = viewModel;
	}
}