using System.Diagnostics;
using WebDocMobile.PageModels;

namespace WebDocMobile.Pages.Mobile;

public partial class FirstPageMobile : ContentPage
{
	public FirstPageMobile()
	{
        InitializeComponent();

        this.BindingContext = new FirstPageViewModel(this.Navigation);

        NavigationPage.SetHasNavigationBar(this, false);
    }
}