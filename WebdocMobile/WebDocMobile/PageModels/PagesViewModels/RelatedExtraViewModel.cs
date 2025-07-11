
using Plugin.ValidationRules.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Maui.Controls.CollectionView;
using WebDocMobile.Helpers;
using WebDocMobile.Models;
using WebDocMobile.Models.NewDocument;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Services;

namespace WebDocMobile.PageModels.PagesViewModels
{
    public class RelatedExtraViewModel : ExtendedPropertyChanged
    {
        public string date { get; set; }
        private INavigation _navigationService;
        private bool _navigateToLogin;
        private readonly IAlertService _alertService;
        private readonly IGetDocumentService _documentService;
        private readonly IGetProcessService _processService;
        public ObservableCollection<RelatedDocumentResponse> relatedDocuments { get; set; }
        public ObservableCollection<DocumentSelectedResponse> documentSelected { get; set; }
        public ObservableCollection<DocumentSelectedResponse> processSelected { get; set; }
        public ObservableCollection<RelatedDocumentResponse> relatedDocumentsWithProcess { get; set; }
        public ObservableCollection<RelatedDocumentResponse> relatedProcess { get; set; }

        public RelatedExtraViewModel(INavigation navigation) : this()
        {
            this._navigationService = navigation;



        }
        public RelatedExtraViewModel()
        {
            _alertService = new AlertService();
            _documentService = new GetDocumentService();
            _processService = new GetProcessService();
            date = DateTime.Now.ToString("dd MMMM yyyy", new CultureInfo("PT-pt"));
            try
            {
                GetDocumentsSelected(string.Empty);
                GetProcesesSelected(string.Empty);
            }
            catch (Exception ex)
            {
                _alertService.ShowAlert("Erro", ex._GetExceptionMessage());
            }
        }


        public AddRelatedResponse AddRelatedDocumentRequest(AddRelatedDocumentRequest request)
        {
            AddRelatedResponse response = null;
            var result = _documentService.AddRelatedDocument(request, out bool navegateToLogin);
            if (result.Status == Models.ReturnStatus.Success)
                response = result.Result;

            else
            {
                _alertService.ShowAlert("Erro", result.Error);

                if (navegateToLogin)
                    _navigationService._PushAsyncWithCleanup(new LoginPageMobile(App.UserDetails?.CodEntidade));

            }
            return response;
        }

        public List<RelatedDocumentResponse> RelatedDocuments(int documentId)
        {
            var response = _documentService.GetRelatedDocument(documentId, out _navigateToLogin);
            if (response.Status == Models.ReturnStatus.Success)
            {
                if (response.Result.Count != 0)
                    relatedDocuments = new ObservableCollection<RelatedDocumentResponse>(
                        response.Result.Select(d => new RelatedDocumentResponse
                        {
                            by = d.by,
                            id = d.id,
                            date = d.date,
                            number = d.number,
                            sentData = d.sentData,
                            subject = d.subject,
                            workflowState = d.workflowState
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

        public AddRelatedResponse DeleteRelatedDocument(AddRelatedDocumentRequest request)
        {
            AddRelatedResponse response = null;
            var result = _documentService.DeleteRelatedDocument(request, out bool navegateToLogin);
            if (result.Status == Models.ReturnStatus.Success)
                response = result.Result;

            else
            {
                _alertService.ShowAlert("Erro", result.Error);

                if (navegateToLogin)
                    _navigationService._PushAsyncWithCleanup(new LoginPageMobile(App.UserDetails?.CodEntidade));

            }
            return response;
        }

        public List<DocumentSelectedResponse> GetDocumentsSelected(string search)
        {
            var response = _documentService.GetDocumentSelected(out _navigateToLogin,search);
            if (response.Status == Models.ReturnStatus.Success)
            {
                
                    documentSelected = new ObservableCollection<DocumentSelectedResponse>(
                        response.Result.Select(d => new DocumentSelectedResponse
                        {
                            id = d.id,
                            number = d.number,
                            subject = d.subject
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

        public List<DocumentSelectedResponse> GetProcesesSelected(string search)
        {
            var response = _processService.GetProcessSelected(out _navigateToLogin, search);
            if (response.Status == Models.ReturnStatus.Success)
            {
                
                    processSelected = new ObservableCollection<DocumentSelectedResponse>(
                        response.Result.Select(d => new DocumentSelectedResponse
                        {
                            id = d.id,
                            subject = d.subject,
                            number = d.number
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

        public AddRelatedResponse AddRelatedProcessRequest(AddRelatedDocumentRequest request)
        {
            AddRelatedResponse response = null;
            var result = _processService.AddRelatedProcess(request, out bool navegateToLogin);
            if (result.Status == Models.ReturnStatus.Success)
                response = result.Result;
            else
            {
                _alertService.ShowAlert("Erro", result.Error);
                if (navegateToLogin)
                    _navigationService._PushAsyncWithCleanup(new LoginPageMobile(App.UserDetails?.CodEntidade));

            }
            return response;
        }

        public List<RelatedDocumentResponse> GetRelatedDocumentsWithProcess(int documentId)
        {
            var response = _documentService.GetRelatedProcessDocument(documentId, out _navigateToLogin);
            if (response.Status == Models.ReturnStatus.Success)
            {
                
                    relatedDocumentsWithProcess = new ObservableCollection<RelatedDocumentResponse>(
                        response.Result.Select(d => new RelatedDocumentResponse
                        {
                            by = d.by,
                            number = d.number,
                            date = d.date,
                            sentData = d.sentData,
                            workflowState = d.workflowState,
                            id = d.id,
                            subject = d.subject
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
        public List<RelatedDocumentResponse> GetRelatedProcess(int documentId)
        {
            var response = _documentService.GetRelatedProcessDocument(documentId, out _navigateToLogin);
            if (response.Status == Models.ReturnStatus.Success)
            {
               
                    relatedDocumentsWithProcess = new ObservableCollection<RelatedDocumentResponse>(
                        response.Result.Select(d => new RelatedDocumentResponse
                        {
                            by = d.by,
                            id = d.id,
                            date = d.date,
                            number = d.number,
                            sentData = d.sentData,
                            subject = d.subject,
                            workflowState = d.workflowState
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

        public AddRelatedResponse AddRelatedWithProcess(AddRelatedDocumentRequest request)
        {
            AddRelatedResponse response = null;
            var result = _processService.AddRelatedWithProcess(request, out bool navegateToLogin);
            if (result.Status == Models.ReturnStatus.Success)
                response = result.Result;
            else
            {
                _alertService.ShowAlert("Erro", result.Error);
                if (navegateToLogin)
                    _navigationService._PushAsyncWithCleanup(new LoginPageMobile(App.UserDetails?.CodEntidade));

            }
            return response;
        }
        public AddRelatedResponse DeleteRelatedProcess(AddRelatedDocumentRequest request)
        {
            AddRelatedResponse response = null;
            var result = _processService.DeleteRelatedProcess(request, out bool navegateToLogin);
            if (result.Status == Models.ReturnStatus.Success)
                response = result.Result;

            else
            {
                _alertService.ShowAlert("Erro", result.Error);

                if (navegateToLogin)
                    _navigationService._PushAsyncWithCleanup(new LoginPageMobile(App.UserDetails?.CodEntidade));

            }
            return response;
        }

        public List<RelatedDocumentResponse> GetRelatedWithProcess(int processId)
        {
            var response = _processService.GetRelatedProcess(processId, out _navigateToLogin);
            if (response.Status == Models.ReturnStatus.Success)
            {
                if (response.Result.Count != 0)
                    relatedProcess = new ObservableCollection<RelatedDocumentResponse>(
                        response.Result.Select(d => new RelatedDocumentResponse
                        {
                            by = d.by,
                            id = d.id,
                            date = d.date,
                            number = d.number,
                            sentData = d.sentData,
                            subject = d.subject,
                            workflowState = d.workflowState
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



    }
}
