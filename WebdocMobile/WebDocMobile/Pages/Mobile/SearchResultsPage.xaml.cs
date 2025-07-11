using CommunityToolkit.Maui.Views;
using WebDocMobile.PageModels.PagesViewModels;

namespace WebDocMobile.Pages.Mobile;

public partial class SearchResultsPage : ContentPage
{
	public SearchResultsPage()
	{
		InitializeComponent();
		BindingContext = new SearchResultsViewModel(this.Navigation);
        NavigationPage.SetHasNavigationBar(this, false);
    }
}