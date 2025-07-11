using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Windows.Input;
using WebDocMobile.Pages.Mobile;

namespace WebDocMobile.PageModels.PagesViewModels;

public partial class SearchResultsViewModel : ObservableObject
{
    private readonly INavigation _navigationService;

    [ObservableProperty]
    private int resultsNumber;

    public SearchResultsViewModel(INavigation navigation)
    {
        this._navigationService = navigation;
        resultsNumber = 480;
    }

    [RelayCommand]
    public async Task GoBack()
    {
        await _navigationService.PopAsync(true);
    }

    public ICommand GoFilterRegisters => new Command(async () =>
    {
        App.Current.MainPage.Opacity = 0.5;
        var result = await App.Current.MainPage.ShowPopupAsync(new SearchFilterOptionsPage());
        App.Current.MainPage.Opacity = 1;
    });
}
