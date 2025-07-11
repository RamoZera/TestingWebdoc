using WebDocMobile.PageModels;

namespace WebDocMobile.Pages.Desktop;

public partial class DocumentsPageDesktop : ContentPage
{
	public DocumentsPageDesktop(DocumentsPageViewModel viewModel)
	{
		InitializeComponent();
        this.BindingContext = viewModel;
    }
}