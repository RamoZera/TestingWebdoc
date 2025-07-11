using CommunityToolkit.Maui.Views;
using WebDocMobile.PageModels.PagesViewModels;

namespace WebDocMobile.Pages.Mobile;

public partial class SearchFilterOptionsPage : Popup
{
	public SearchFilterOptionsPage()
	{
		InitializeComponent();
        BindingContext = new SearchFilterOptionsViewModel();
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        var objTest = new { userID = 1, userText = "Test" };
        this.Close(objTest);
    }
}