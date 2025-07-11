using Plugin.ValidationRules.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WebDocMobile.Helpers;
using WebDocMobile.Models;
using WebDocMobile.Models.BookService;
using WebDocMobile.Models.NewDocument;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Services;

namespace WebDocMobile.PageModels.PagesViewModels
{
    public class KnowledgeExtraViewModel : ExtendedPropertyChanged
    {
        public string date { get; set; }
        private INavigation _navigationService;
        private readonly IAlertService _alertService;
        private readonly IGetDocumentService _documentService;
        private readonly IGetProcessService _processService;
        public ObservableCollection<Knowledge> knoledgebyDocuments { get; set; }
        public ObservableCollection<Knowledge> knoledgebyProcess { get; set; }
        public KnowledgeExtraViewModel(INavigation navigation) : this()
        {
            this._navigationService = navigation;


        }
        public KnowledgeExtraViewModel()
        {
            _alertService = new AlertService();
            _documentService = new GetDocumentService();
            _processService = new GetProcessService();
            date = DateTime.Now.ToString("dd MMMM yyyy", new CultureInfo("PT-pt"));
        }
       
        public List<Knowledge> GetKnowLedgeDocuments(int documentId)
        {
            var response = _documentService.GetKnowledgeDocument(documentId, out bool _navigateToLogin);
            if (response.Status == Models.ReturnStatus.Success)
            {
               
                    knoledgebyDocuments = new ObservableCollection<Knowledge>(
                        response.Result.Select(d => new Knowledge
                        {
                            id = d.id,
                            comments = d.comments,
                            date = d.date,
                            from = d.from,
                            to = d.to,
                            observations = d.observations,
                            knowledgeDate = d.knowledgeDate
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

        public List<Knowledge> GetKnowLedgeProcess(int documentId)
        {
            var response = _processService.GetKnowledgeProcess(documentId, out bool _navigateToLogin);
            if (response.Status == Models.ReturnStatus.Success)
            {
               
                    knoledgebyProcess = new ObservableCollection<Knowledge>(
                        response.Result.Select(d => new Knowledge
                        {
                            id = d.id,
                            comments = d.comments,
                            date = d.date,
                            from = d.from,
                            to = d.to,
                            observations = d.observations,
                            knowledgeDate = d.knowledgeDate
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

        public ObservableCollection<Models.BookService.User> Users
        {
            get
            {

                return AppDefaultData.Users;
            }
        }

        public ObservableCollection<GroupDto> Group
        {
            get
            {

                return AppDefaultData.Group;
            }
        }

        public AddRelatedResponse SaveKnowLedge(KnowledgeDocumentRequest request)
        {
            AddRelatedResponse response = null;
            var result = _documentService.AddknowledgeDocument(request, out bool _navigateToLogin);
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
        public AddRelatedResponse DeleteKnowLedge(KnowledgeDocumentRequest request)
        {
            AddRelatedResponse response = null;
            var result = _documentService.DeleteKnowledgeDocument(request, out bool _navigateToLogin);
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


        public AddRelatedResponse SaveKnowLedgeProcess(KnowledgeDocumentRequest request)
        {
            AddRelatedResponse response = null;
            var result = _processService.AddknowledgeProcess(request, out bool _navigateToLogin);
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
        public AddRelatedResponse DeleteKnowLedgeProcess(KnowledgeDocumentRequest request)
        {
            AddRelatedResponse response = null;
            var result = _processService.DeleteKnowledgeProcess(request, out bool _navigateToLogin);
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

    }
}
