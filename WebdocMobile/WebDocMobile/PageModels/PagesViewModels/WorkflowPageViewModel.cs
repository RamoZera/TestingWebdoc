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
    public class WorkflowPageViewModel : ExtendedPropertyChanged
    {
        public string date { get; set; }
        private INavigation _navigationService;
        private bool _navigateToLogin;
        private readonly IAlertService _alertService;
        private readonly IGetDocumentService _documentService;
        private readonly IGetProcessService _processService;
        public ObservableCollection<WorkflowResponse> worksflows { get; set; } = new();
        public ObservableCollection<RadioNavigate> radioNavigates { get; set; }
        public ObservableCollection<WorkflowResponse> worksflowsProcess { get; set; } = new();
        public ObservableCollection<RadioNavigate> radioNavigatesProcess { get; set; }

        public WorkflowPageViewModel(INavigation navigation) : this()
        {
            this._navigationService = navigation;
        }
        public WorkflowPageViewModel()
        {
            _alertService = new AlertService();
            _documentService = new GetDocumentService();
            _processService = new GetProcessService();
            date = DateTime.Now.ToString("dd MMMM yyyy", new CultureInfo("PT-pt"));

        }

        #region Documents 

        public List<WorkflowResponse> GetWorkflowHystory(int documentId)
        {
            var response = _documentService.GetWorkFlowHistory(documentId, out _navigateToLogin);
            if (response.Status == Models.ReturnStatus.Success)
            {

                worksflows = new ObservableCollection<WorkflowResponse>(response.Result.Select(p => new WorkflowResponse
                {
                    Id = p.Id,
                    By = p.By,
                    To = p.To,
                    Comments = p.Comments,
                    Date = p.Date,
                    From = p.From
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

        public WorflowkNavigationResponse GetWorflowkNavigation(int documentId)
        {
            WorflowkNavigationResponse response = null;
            var result = _documentService.GetWorkflowNavigation(documentId, out _navigateToLogin);
            if (result.Status == Models.ReturnStatus.Success)
            {
                radioNavigates = new ObservableCollection<RadioNavigate>(
                       result.Result.Navigation.Select(d => new RadioNavigate
                       {
                           Id = d.Id,
                           Name = d.Name,
                           Selected = d.Selected
                       }));
                response = result.Result;
            }

            else
            {
                _alertService.ShowAlert("Erro", result.Error);
                if (_navigateToLogin)
                    _navigationService._PushAsyncWithCleanup(new LoginPageMobile(App.UserDetails?.CodEntidade));
            }
            return response;

        }

        public AddRelatedResponse AddWorkflowNavigation(WorkflowRequest request)
        {
            AddRelatedResponse response = null;
            var result = _documentService.AddWorkflowNavigation(request, out bool _navigateToLogin);
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

        #region processes
        public List<WorkflowResponse> GetProcessWorkflowHystory(int documentId)
        {
            var response = _processService.GetProcessWorkFlowHistory(documentId, out _navigateToLogin);
            if (response.Status == Models.ReturnStatus.Success)
            {
              
                    worksflowsProcess = new ObservableCollection<WorkflowResponse>(response.Result.Select(p => new WorkflowResponse
                    {
                        Id = p.Id,
                        By = p.By,
                        To = p.To,
                        Comments = p.Comments,
                        Date = p.Date,
                        From = p.From
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

        public WorflowkNavigationResponse GetProcessWorflowkNavigation(int documentId)
        {
            WorflowkNavigationResponse response = null;
            var result = _processService.GetProcessWorkflowNavigation(documentId, out _navigateToLogin);
            if (result.Status == Models.ReturnStatus.Success)
            {
                radioNavigatesProcess = new ObservableCollection<RadioNavigate>(
                       result.Result.Navigation.Select(d => new RadioNavigate
                       {
                           Id = d.Id,
                           Name = d.Name,
                           Selected = d.Selected
                       }));
                response = result.Result;
            }

            else
            {
                _alertService.ShowAlert("Erro", result.Error);
                if (_navigateToLogin)
                    _navigationService._PushAsyncWithCleanup(new LoginPageMobile(App.UserDetails?.CodEntidade));
            }
            return response;

        }

        public AddRelatedResponse AddProcessWorkflowNavigation(WorkflowRequest request)
        {
            AddRelatedResponse response = null;
            var result = _processService.AddProcessWorkflowNavigation(request, out bool _navigateToLogin);
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
