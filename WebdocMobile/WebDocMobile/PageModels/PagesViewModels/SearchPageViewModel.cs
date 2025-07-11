

using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WebDocMobile.Pages.Mobile;

namespace WebDocMobile.PageModels.PagesViewModels;

public partial class SearchPageViewModel(INavigation navigation)
{
    private readonly INavigation _navigationService = navigation;

    public ObservableCollection<string> Entities { get; set; } =
            [
                "INE",
                "AMBISIG",
                "Autoridade da Mobilidade e dos Transportes",
                "Agência Portuguesa do Ambiente, I.P",
                "Autoridade Marítima Nacional",
                "Direção Regional de Políticas Marítimas",
                "Direção-Geral de Estatísticas da Educação e Ciência",
                "Direção-Geral da Autoridade Marítima",
                "Porto dos Açores",
                "Turismo de Portugal, I.P.",
            ];

    [RelayCommand]
    public async Task GoBack()
    {
        await _navigationService.PopAsync();
    }

    [RelayCommand]
    public async Task Search()
    {
        await _navigationService.PushAsync(new SearchResultsPage());
    }
}
