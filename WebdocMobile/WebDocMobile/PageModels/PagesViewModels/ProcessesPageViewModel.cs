using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocMobile.Models.DocumentService;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using WebDocMobile.Models.DashBoard;
using System.Threading.Tasks;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Models;
using WebDocMobile.Services;
using System.Collections.ObjectModel;
using WebDocMobile.Helpers;

namespace WebDocMobile.PageModels
{
    public partial class ProcessesPageViewModel: ObservableObject
    {
        private INavigation _navigationService;
        public bool _navigationToLogin;
        private readonly IAlertService _alertService;
        private GetDocumentItems document { get; set; }
        private readonly IGetDashBoardService _dashBoardService;
        public ObservableCollection<GetCategoricalData> dataProcesses { get; set; }
        public ObservableCollection<Processes> ProcesesA { get; set; }
        public string date { get; set; }

        [ObservableProperty]
        public bool gbLoader;
        public ProcessesPageViewModel(INavigation navigation) : this() => _navigationService = navigation;

        public ProcessesPageViewModel()
        {
            date = DateTime.Now.ToString("dd MMMM yyyy", new CultureInfo("PT-pt"));
            _dashBoardService = new DashBoardService();
            _alertService = new AlertService();
            gbLoader = false;
            var result = _dashBoardService.GetProcesses(null, out _navigationToLogin);
            if(result.Status == ReturnStatus.Success)
            {
                ProcesesA = new ObservableCollection<Processes>(result.Result.Select(p =>
                {
                    p.dateString = p.sentData.ToString(APIHelper.dateCustom);
                    return p;
                }));
                dataProcesses = GetCategoricalDataProcess();
            }
            else
            {
                _alertService.ShowAlert("Erro", result.Error);
                if(_navigationToLogin)
                    _navigationService._PushAsyncWithCleanup(new LoginPageMobile(App.UserDetails?.CodEntidade));
            }
        }
        private ObservableCollection<GetCategoricalData> GetCategoricalDataProcess() => new ObservableCollection<GetCategoricalData>
        {
            new GetCategoricalData { Category = "Conhecimento", Value =  App.DashBoard.ProcessesCount },
            new GetCategoricalData { Category = "Comigo", Value = App.DashBoard.ProcessesDepartamento},
            new GetCategoricalData { Category = "Departamento", Value = App.DashBoard.ProcessesConhecimento }
        };
    }
}
