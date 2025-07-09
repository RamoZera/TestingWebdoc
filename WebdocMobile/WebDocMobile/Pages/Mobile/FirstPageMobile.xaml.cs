using System.Diagnostics;
using WebDocMobile.PageModels;

namespace WebDocMobile.Pages.Mobile;

public partial class FirstPageMobile : ContentPage
{
	public FirstPageMobile(FirstPageViewModel viewModel)
	{
        InitializeComponent();
        this.BindingContext = viewModel;
        NavigationPage.SetHasNavigationBar(this, false);
    }
}