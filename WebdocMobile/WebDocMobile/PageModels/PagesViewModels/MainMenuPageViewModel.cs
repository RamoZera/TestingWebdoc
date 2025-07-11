using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Globalization;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;
using WebDocMobile.Helpers;
using WebDocMobile.Models.DashBoard;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Services;

namespace WebDocMobile.PageModels
{
    public partial class MainMenuPageViewModel : ObservableObject
    {

        private string text = " registos";
        public string username { get; set; }
        public string date { get; set; }
        [ObservableProperty]
        public string allDocumentsNumber;
        [ObservableProperty]
        public string allProcessesNumber;
        [ObservableProperty]
        private string myDocumentsNumber;
        [ObservableProperty]
        private string departmentDocumentsNumber;
        [ObservableProperty]
        private string knownDocumentsNumber;
        [ObservableProperty]
        private bool isNewRegisterPopupOpen = false;
        [ObservableProperty]
        private bool biometricsConfigured = true;
        [ObservableProperty]
        public Document document;
        [ObservableProperty]
        private Document procese;

        [ObservableProperty]
        public bool gbLoader;
        [ObservableProperty]
        public bool gbModalError;
        [ObservableProperty]
        public string gbIconModalError;
        [ObservableProperty]
        public string gbTitleModalError;
        [ObservableProperty]
        public string gbTextModalError;

        private readonly IGetDashBoardService _dashBoardService;

        [ObservableProperty]
        public ObservableCollection<GetCategoricalData> data;
        [ObservableProperty]
        public ObservableCollection<GetCategoricalData> dataProceses;
        public ObservableCollection<Brush> colorGraph { get; set; }
        private INavigation _navigationService;
        public bool _navigationToLogin;
        private readonly IAlertService _alertService;
        internal Color[] Palette { get; }
        public List<Document> Documents { get; private set; }
        public List<Processes> Proceses { get; private set; }

        public MainMenuPageViewModel(INavigation navigation, bool biometricsConfigured = false) : this()
        {
            _navigationService = navigation;
            this.biometricsConfigured = biometricsConfigured;
        }

        public MainMenuPageViewModel()
        {
            _alertService = new AlertService();
            _dashBoardService = new DashBoardService();
            gbLoader = false;
            gbModalError = false;
            gbIconModalError = "icon_erroconta";
            gbTitleModalError = "";
            gbTextModalError = "";
            username = App.UserDetails.StrName;
            date = DateTime.Now.ToString("dd MMMM yyyy", new CultureInfo("PT-pt"));
            Palette = PaletteLoader.LoadPalette("#072B8E", "#1161FC", "#63B6E3");
            try
            {
                SetPDNumbers(true, true);
                AddDocsPreview();
                AddPrcsPreview();
            }
            catch (Exception ex)
            {
                GbTitleModalError = "Erro";
                GbTextModalError = ex._GetExceptionMessage();
                GbModalError = true;
                //_alertService.ShowAlert("Erro", ex._GetExceptionMessage());
                _navigationService._PushAsyncWithCleanup(new LoginPageMobile(App.UserDetails?.CodEntidade));
            }
        }

        private void AddDocsPreview()
        {
            var dr = _dashBoardService.GetDocuments(5, out _navigationToLogin);
            if (dr.Status == Models.ReturnStatus.Success)
            {
                Documents = dr.Result.Select(d =>
                {
                    d.dateString = d.sentData.HasValue ? d.sentData.Value.ToString(APIHelper.dateCustom) : null;
                    return d;
                }).ToList();
            }
            else
            {
                //_alertService.ShowAlert("Erro", dr.Error);
                GbTitleModalError = "Erro";
                GbTextModalError = dr.Error;
                GbModalError = true;
                if (_navigationToLogin)
                    _navigationService._PushAsyncWithCleanup(new LoginPageMobile(App.UserDetails?.CodEntidade));
            }
        }

        private void AddPrcsPreview()
        {
            var pr = _dashBoardService.GetProcesses(5, out _navigationToLogin);
            if (pr.Status == Models.ReturnStatus.Success)
            {
                Proceses = pr.Result.Select(p =>
                {
                    p.dateString = p.sentData.ToString(APIHelper.dateCustom);
                    return p;
                }).ToList();
            }
            else
            {
                //_alertService.ShowAlert("Erro", pr.Error);
                GbTitleModalError = "Erro";
                GbTextModalError = pr.Error;
                GbModalError = true;
                if (_navigationToLogin)
                    _navigationService._PushAsyncWithCleanup(new LoginPageMobile(App.UserDetails?.CodEntidade));
            }
        }


        private ObservableCollection<GetCategoricalData> GetElementData(int myElementsNumber, int departmentElementsNumber, int knownElementsNumber) => new ObservableCollection<GetCategoricalData>
        {
            new GetCategoricalData { Category = "Conhecimento", Value = myElementsNumber },
            new GetCategoricalData { Category = "Comigo", Value = knownElementsNumber },
            new GetCategoricalData { Category = "Departamento", Value = departmentElementsNumber }
        };

        private void SetPDNumbers(bool reload, bool loadDocuments)
        {
            if (reload)
            {
                Models.GenericResponse<GetDashBoard> result = _dashBoardService.GetDashBoardData(out _navigationToLogin);
                if (result.Status == Models.ReturnStatus.Success)
                    App.DashBoard = result.Result;
                else
                {
                    App.DashBoard = null;
                    //_alertService.ShowAlert("Erro", result.Error);
                    GbTitleModalError = "Erro";
                    GbTextModalError = result.Error;
                    GbModalError = true;
                    if (_navigationToLogin)
                        _navigationService._PushAsyncWithCleanup(new LoginPageMobile(App.UserDetails?.CodEntidade));
                }
            }
            if (App.DashBoard != null)
            {
                if (loadDocuments)
                {
                    MyDocumentsNumber = App.DashBoard.DocumentsCount.ToString();
                    DepartmentDocumentsNumber = App.DashBoard.DocumentsDepartamento.ToString();
                    KnownDocumentsNumber = App.DashBoard.DocumentsConhecimento.ToString();
                    AllDocumentsNumber = $"{App.DashBoard.DocumentsCount + App.DashBoard.DocumentsDepartamento + App.DashBoard.DocumentsConhecimento} {text}";
                    Data = GetElementData(App.DashBoard.DocumentsCount, App.DashBoard.DocumentsDepartamento, App.DashBoard.DocumentsConhecimento);
                }
                else
                {
                    MyDocumentsNumber = App.DashBoard.ProcessesCount.ToString();
                    DepartmentDocumentsNumber = App.DashBoard.ProcessesDepartamento.ToString();
                    KnownDocumentsNumber = App.DashBoard.ProcessesConhecimento.ToString();
                    AllProcessesNumber = $"{App.DashBoard.ProcessesCount + App.DashBoard.ProcessesDepartamento + App.DashBoard.ProcessesConhecimento} {text}";
                    DataProceses = GetElementData(App.DashBoard.ProcessesCount, App.DashBoard.ProcessesDepartamento, App.DashBoard.ProcessesConhecimento);
                }
            }
        }

        static class PaletteLoader
        {
            public static Color[] LoadPalette(params string[] values)
            {
                Color[] colors = new Color[values.Length];
                for (int i = 0; i < values.Length; i++)
                    colors[i] = Color.FromArgb(values[i]);
                return colors;
            }
        }


        [RelayCommand]
        public async Task HandleSeeDocumentsButton()
        {
            GbLoader = true;
            await Task.Delay(1000);
            await _navigationService.PushAsync(new DocumentsPageMobile());
            GbLoader = false;
        }
        [RelayCommand]
        public async Task HandleSeeProcessesButton()
        {
            GbLoader = true;
            await Task.Delay(1000);
            await _navigationService.PushAsync(new ListProceses());
            GbLoader = false;
        }
        [RelayCommand]
        public void ShowDocumentDetails()
        {
            SetPDNumbers(false, true);
        }
        [RelayCommand]
        public void ShowProccessDetails()
        {
            SetPDNumbers(false, false);
        }
        [RelayCommand]
        public async Task NavToSearchPage()
        {
            await _navigationService.PushAsync(new SearchPage());
        }
        [RelayCommand]
        public void FecharModalClicked()
        {
            GbModalError = false;
        }
    }
}