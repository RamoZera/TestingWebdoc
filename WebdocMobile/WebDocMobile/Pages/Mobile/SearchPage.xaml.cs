using WebDocMobile.PageModels;
using WebDocMobile.PageModels.PagesViewModels;

namespace WebDocMobile.Pages.Mobile;

public partial class SearchPage : ContentPage
{
    public SearchPage()
    {
        InitializeComponent();

        this.BindingContext = new SearchPageViewModel(this.Navigation);

        NavigationPage.SetHasNavigationBar(this, false);
    }
}