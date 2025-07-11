using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
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

    public partial class MoreOperationsViewModel : ExtendedPropertyChanged
    {
        public string date { get; set; }
        private INavigation _navigationService;
        private bool _navigateToLogin;
        private readonly IAlertService _alertService;
        private readonly IGetDocumentService _documentService;
        private readonly IGetProcessService _processService;
        public ObservableCollection<Movement> movements { get; set; }
        public ObservableCollection<Movement> movementsProcess { get; set; }
        public ObservableCollection<RelatedDocumentResponse> relatedDocuments { get; set; }
        public ObservableCollection<DocumentSelectedResponse> documentsSelected { get; set; }
        public ObservableCollection<Knowledge> knowledges { get; set; }


        private int documentId { get; set; }
        public int countMovement { get; set; }
        public int countKnowledge { get; set; }
        public MoreOperationsViewModel(INavigation navigation) : this()
        {
            this._navigationService = navigation;


        }
        public MoreOperationsViewModel()
        {
            _alertService = new AlertService();
            _documentService = new GetDocumentService();
            _processService = new GetProcessService();
            date = DateTime.Now.ToString("dd MMMM yyyy", new CultureInfo("PT-pt"));

        }
        #region documents
        public List<Movement> Movements(DocumentMetadataRequest request)
        {

            var response = _documentService.GetMovementDocument(request.id, out _navigateToLogin);
            if (response.Status == Models.ReturnStatus.Success)
            {
                if (response.Result.Count != 0)
                {
                    movements = new ObservableCollection<Movement>(response.Result.Select(d =>
                    new Movement
                    {
                        id = d.id,
                        date = d.date,
                        from = d.from,
                        number = d.number,
                        observations = d.observations,
                        to = d.to
                    }));
                    //countMovement = movements.Count;
                    documentId = request.id;
                }


            }
            else
            {
                _alertService.ShowAlert("Erro", response.Error);
                if (_navigateToLogin)
                    _navigationService._PushAsyncWithCleanup(new LoginPageMobile(App.UserDetails?.CodEntidade));

            }

            return response.Result;
        }

        public List<RelatedDocumentResponse> RelatedDocuments(DocumentMetadataRequest request)
        {
            var response = _documentService.GetRelatedDocument(request.id, out _navigateToLogin);
            if (response.Status == Models.ReturnStatus.Success)
            {
                if (response.Result.Count != 0)
                {
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
            }
            else
            {
                _alertService.ShowAlert("Erro", response.Error);
                if (_navigateToLogin)
                    _navigationService._PushAsyncWithCleanup(new LoginPageMobile(App.UserDetails?.CodEntidade));

            }

            return response.Result;
        }

        public List<DocumentSelectedResponse> GetDocumentsSelected(string search)
        {
            var response = _documentService.GetDocumentSelected(out _navigateToLogin, search);
            if (response.Status == Models.ReturnStatus.Success)
            {
                if (response.Result.Count != 0)
                {
                    foreach (var b in response.Result)
                    {

                        documentsSelected.Add(b);
                    }

                }
            }
            else
            {
                _alertService.ShowAlert("Erro", response.Error);
                if (_navigateToLogin)
                    _navigationService._PushAsyncWithCleanup(new LoginPageMobile(App.UserDetails?.CodEntidade));

            }
            return response.Result;
        }

        public List<Knowledge> Knowledges(DocumentMetadataRequest request)
        {
            var response = _documentService.GetKnowledgeDocument(request.id, out _navigateToLogin);
            if (response.Status == Models.ReturnStatus.Success)
            {
                
                    knowledges = new ObservableCollection<Knowledge>(
                        response.Result.Select(d => new Knowledge
                        {
                            id = d.id,
                            comments = d.comments,
                            date = d.date,
                            from = d.from,
                            knowledgeDate = d.knowledgeDate,
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
        #region process
        public List<Movement> MovementsProcess(DocumentMetadataRequest request)
        {
            var response = _processService.GetMovementProcess(request.id, out _navigateToLogin);
            if (response.Status == Models.ReturnStatus.Success)
            {
                
                    movementsProcess = new ObservableCollection<Movement>(response.Result.Select(d =>
                     new Movement
                     {
                         id = d.id,
                         date = d.date,
                         from = d.from,
                         number = d.number,
                         observations = d.observations,
                         to = d.to
                     }));
                   
                

                documentId = request.id;

            }
            else
            {
                _alertService.ShowAlert("Erro", response.Error);

                if (_navigateToLogin)
                    _navigationService._PushAsyncWithCleanup(new LoginPageMobile(App.UserDetails?.CodEntidade));
            }

            return response.Result;
        }
        public List<RelatedDocumentResponse> RelatedProcess(DocumentMetadataRequest request)
        {
            var response = _processService.GetRelatedProcess(request.id, out _navigateToLogin);
            if (response.Status == Models.ReturnStatus.Success)
            {
               
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
        public List<Knowledge> KnowledgesProcess(DocumentMetadataRequest request)
        {
            var response = _processService.GetKnowledgeProcess(request.id, out _navigateToLogin);
            if (response.Status == Models.ReturnStatus.Success)
            {
               
                    knowledges = new ObservableCollection<Knowledge>(
                        response.Result.Select(d => new Knowledge
                        {
                            id = d.id,
                            comments = d.comments,
                            date = d.date,
                            from = d.from,
                            knowledgeDate = d.knowledgeDate,
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
