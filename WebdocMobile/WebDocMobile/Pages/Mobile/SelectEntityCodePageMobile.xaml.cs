using System.Diagnostics;
using WebDocMobile.PageModels;
using WebDocMobile.Services;

namespace WebDocMobile.Pages.Mobile;

public partial class SelectEntityCodePageMobile : ContentPage
{
	public SelectEntityCodePageMobile(SelectEntityCodePageViewModel viewModel)
	{
		InitializeComponent();

		this.BindingContext = viewModel;

        NavigationPage.SetHasNavigationBar(this, false);

    }
}