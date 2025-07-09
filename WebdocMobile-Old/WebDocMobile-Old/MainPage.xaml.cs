using System.Diagnostics;
using WebDocMobile.Services;
using WebDocMobile.Helpers.WsMethods;
using WebDocMobile.Pages;

namespace WebDocMobile;

public partial class MainPage : ContentPage
{

    public MainPage()
	{
        InitializeComponent();
	}

	public async void GoToFirstPage(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("FirstPage");
	}

	

}

