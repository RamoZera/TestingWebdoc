using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Maui.Controls;
using WebDocMobile.Models;
using WebDocMobile.Models.BookService;
using WebDocMobile.Services;
using Plugin.ValidationRules;
using WebDocMobile.Models.ValidationRules;
using Plugin.ValidationRules.Extensions;
using WebDocMobile.Models.DocumentService;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Helpers;
using WebDocMobile.Models.NewDocument;
using WebDocMobile.Models.NewProcesses;
using System.Diagnostics;

namespace WebDocMobile.PageModels.PagesViewModels
{
    public partial class NewProcessPageViewModel : ObservableObject
    {
        public string date { get; set; }
        private INavigation _navigationService;
        private readonly IAlertService _alertService;
        //private GetDocumentItems document { get; set; }
        private readonly IGetDocumentService _documentService;
        private readonly IGetProcessService _processService;
        private ObservableCollection<IdName<int, string>> books { get; set; } 
        private ObservableCollection<DocumentTypeDto> types { get; set; } 
        [ObservableProperty]
        public bool gbLoader;
        public NewProcessPageViewModel(INavigation navigation)
        {
            this._navigationService = navigation;
            gbLoader = false;
            date = DateTime.Now.ToString("dd MMMM yyyy", new CultureInfo("PT-pt"));

            Topic = Validator.Build<string>()
                            .WithRule(new IsNotNullOrEmptyRule<string>(), "A topic is required.");
            BookR = Validator.Build<string>()
                          .WithRule(new IsNotNullOrEmptyRule<string>(), "O Livro é obrigatório.");

            SendR = Validator.Build<string>()
                          .WithRule(new IsNotNullOrEmptyRule<string>(), "O Envio /Receção é obrigatório.");
            Classifier = Validator.Build<string>()
                          .WithRule(new IsNotNullOrEmptyRule<string>(), "O Classificador é obrigatório.");
            ProcessType = Validator.Build<string>()
                   .WithRule(new IsNotNullOrEmptyRule<string>(), "O Tipo Proceso é obrigatório.");
            _alertService = new AlertService();
            _processService = new GetProcessService();

        }

        public NewProcessPageViewModel()
        {
            Topic = Validator.Build<string>()
                           .WithRule(new IsNotNullOrEmptyRule<string>(), "O Assunto é obrigatório.");
            BookR = Validator.Build<string>()
                           .WithRule(new IsNotNullOrEmptyRule<string>(), "O Livro é obrigatório.");
            _alertService = new AlertService();
        }


        public Validatable<string> Topic { get; set; }
        public Validatable<string> BookR { get; set; }
        public Validatable<string> Type { get; set; }
        public Validatable<string> ProcessType { get; set; }
        public Validatable<string> SendR { get; set; }
        public Validatable<string> Classifier { get; set; }

        [RelayCommand]
        public async Task GoBack()
        {
            await _navigationService.PopAsync();
        }


        public ObservableCollection<IdName<int, string>> Book1
        {
            get
            {

                return AppDefaultData.Books;
            }
        }

        public ObservableCollection<DocumentTypeDto> DocumentType
        {
            get
            {

                return AppDefaultData.DocumentType;
            }
        }

        public ObservableCollection<IdName<int, string>> SendReceive
        {
            get
            {

                return AppDefaultData.SendReceive;
            }
        }

        public ObservableCollection<IdName<int, string>> Classify
        {
            get
            {

                return AppDefaultData.Classifiers;
            }
        }

        public DocumentResponse Insert(NewProcessRequest doc)
        {
            GbLoader = true;
            DocumentResponse response = null;
            var results = _processService.InsertTest(doc, out bool navigateToLogin, out GenericResponse<DocumentResponse> result);
            if (results.Status == Models.ReturnStatus.Success)
                response = results.Result;
            else
            {
                _alertService.ShowAlert("Erro", results.Error);
                if (navigateToLogin)
                    _navigationService._PushAsyncWithCleanup(new LoginPageMobile(App.UserDetails?.CodEntidade));

            }
            GbLoader = false;

            return response;
        }

    }
}
