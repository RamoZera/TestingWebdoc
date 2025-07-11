using Plugin.ValidationRules.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocMobile.Helpers;
using WebDocMobile.Models.NewDocument;
using WebDocMobile.Models.NewProcesses;
using WebDocMobile.Models.WorkflowService;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Services;

namespace WebDocMobile.PageModels.PagesViewModels
{
    public class MovementExtraViewModel : ExtendedPropertyChanged
    {
        public string date { get; set; }
        private INavigation _navigationService;
        private bool _navigateToLogin;
        private readonly IAlertService _alertService;
        private readonly IGetDocumentService _documentService;
        private readonly IGetProcessService _processService;
        public ObservableCollection<Movement> movements { get; set; } = new();
        public ObservableCollection<Movement> movementsProcess { get; set; } = new();

        public MovementExtraViewModel(INavigation navigation)
        {
            this._navigationService = navigation;
            _alertService = new AlertService();
            _documentService = new GetDocumentService();
            _processService = new GetProcessService();
            date = DateTime.Now.ToString("dd MMMM yyyy", new CultureInfo("PT-pt"));
        }
        public MovementExtraViewModel()
        {

            _alertService = new AlertService();
            _documentService = new GetDocumentService();
            date = DateTime.Now.ToString("dd MMMM yyyy", new CultureInfo("PT-pt"));
        }

        #region Document

        public List<Movement> Movements(DocumentMetadataRequest request)
        {
            var response = _documentService.GetMovementDocument(request.id, out _navigateToLogin);
            if (response.Status == Models.ReturnStatus.Success)
            {

                foreach (var b in response.Result)
                    movements.Add(b);

            }
            else
            {
                _alertService.ShowAlert("Erro", response.Error);
                if (_navigateToLogin)
                    _navigationService._PushAsyncWithCleanup(new LoginPageMobile(App.UserDetails?.CodEntidade));

            }

            return response.Result;
        }

        public DocumentMetadataResponse GetMetadata(DocumentMetadataRequest request)
        {
            DocumentMetadataResponse response = null;
            var result = _documentService.GetDocumentMetadata(request, out _navigateToLogin);
            if (result.Status == Models.ReturnStatus.Success)
                response = result.Result;
            else
            {
                _alertService.ShowAlert("Erro", result.Error);
                if (_navigateToLogin)
                    _navigationService._PushAsyncWithCleanup(new LoginPageMobile(App.UserDetails?.CodEntidade));
            }

            return response;
        }

        #endregion


        #region Process
        public List<Movement> ProcessMovements(DocumentMetadataRequest request)
        {
            var response = _processService.GetMovementProcess(request.id, out _navigateToLogin);
            if (response.Status == Models.ReturnStatus.Success)
            {
                
                    movementsProcess = new ObservableCollection<Movement>(
                        response.Result.Select(d => new Movement
                        {
                            id = d.id,
                            date = d.date,
                            from = d.from,
                            number = d.number,
                            observations = d.observations,
                            to = d.to

                        }));
               
            }
            else
            {
                _alertService.ShowAlert("Erro", response.Error);
                if (_navigateToLogin)
                    _navigationService._PushAsyncWithCleanup(new LoginPageMobile(App.UserDetails?.CodEntidade));

            }

            return response.Result;
        }

        public ProcessMetadataResponse GetProcessMetadata(DocumentMetadataRequest request)
        {
            ProcessMetadataResponse response = null;
            var result = _processService.GetProcessMetadata(request, out _navigateToLogin);
            if (result.Status == Models.ReturnStatus.Success)
                response = result.Result;
            else
            {
                _alertService.ShowAlert("Erro", result.Error);
                if (_navigateToLogin)
                    _navigationService._PushAsyncWithCleanup(new LoginPageMobile(App.UserDetails?.CodEntidade));
            }

            return response;
        }

        #endregion
    }

}
