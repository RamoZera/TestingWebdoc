using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using WebDocMobile.Models.DashBoard;

using System.Threading.Tasks;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Models;
using WebDocMobile.Models.DocumentService;
using WebDocMobile.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using WebDocMobile.Helpers;

namespace WebDocMobile.PageModels
{
    public partial class DocumentsPageViewModel: ObservableObject
    {
        public string DepartmentDocumentsNumber { get; set; }
        private readonly IGetDashBoardService _dashBoardService;
        public ObservableCollection<GetCategoricalData> dataDocuments { get; set; }
        public ObservableCollection<Document> DocumentsA { get; set; }
        public bool _navigationToLogin;
        private readonly IAlertService _alertService;
        public string date { get; set; }
        private INavigation _navigationService;
        private GetDocumentItems document { get; set; }

        [ObservableProperty]
        public bool gbLoader;
        public DocumentsPageViewModel(INavigation navigation) : this() => _navigationService = navigation;

        public DocumentsPageViewModel()
        {
            date = DateTime.Now.ToString("dd MMMM yyyy", new CultureInfo("PT-pt"));
            _dashBoardService = new DashBoardService();
            _alertService = new AlertService();
            gbLoader = false;
            var result = _dashBoardService.GetDocuments(null, out _navigationToLogin);
            if(result.Status == ReturnStatus.Success)
            {
                DocumentsA = new ObservableCollection<Document>(result.Result.Select(d =>
                {
                    d.dateString = d.sentData.HasValue ? d.sentData.Value.ToString(APIHelper.dateCustom) : null;
                    return d;
                }));
                dataDocuments = GetCategoricalDataDocument();
            }
            else
            {
                _alertService.ShowAlert("Erro", result.Error);
                if(_navigationToLogin)
                    _navigationService._PushAsyncWithCleanup(new LoginPageMobile(App.UserDetails?.CodEntidade));
            }
        }
        private ObservableCollection<GetCategoricalData> GetCategoricalDataDocument() => new ObservableCollection<GetCategoricalData>
        {
            new GetCategoricalData { Category = "Conhecimento", Value = App.DashBoard.DocumentsCount },
            new GetCategoricalData { Category = "Comigo", Value = App.DashBoard.DocumentsDepartamento},
            new GetCategoricalData { Category = "Departamento", Value = App.DashBoard.DocumentsConhecimento }
        };

        [RelayCommand]
        private async void OpenFilters()
        {
#if ANDROID || IOS
           // await _navigationService.PushAsync(new DocumentFilter());
#endif
        }
    }
}
