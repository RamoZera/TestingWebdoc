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
using WebDocMobile.Models.NewDocument;
using WebDocMobile.Helpers;
using WebDocMobile.Pages.Mobile;
using System.Diagnostics;

namespace WebDocMobile.PageModels.PagesViewModels
{
    public partial class NewDocumentPageViewModel : ObservableObject
    {

        public string date { get; set; }
        private INavigation _navigationService;
        private readonly IAlertService _alertService;
        private readonly IGetDocumentService _documentService;
        private ObservableCollection<IdName<int, string>> books { get; set; }


        [ObservableProperty]
        public bool gbLoader;

        public NewDocumentPageViewModel(INavigation navigation)
        {
            this._navigationService = navigation;
            _alertService = new AlertService();
            gbLoader = false;
            _documentService = new GetDocumentService();
            date = DateTime.Now.ToString("dd MMMM yyyy", new CultureInfo("PT-pt"));

            Topic = Validator.Build<string>()
                            .WithRule(new IsNotNullOrEmptyRule<string>(), "A topic is required.");
            BookR = Validator.Build<string>()
                          .WithRule(new IsNotNullOrEmptyRule<string>(), "O Livro é obrigatório.");
            Type = Validator.Build<string>()
                          .WithRule(new IsNotNullOrEmptyRule<string>(), "O Document Type é obrigatório.");
            SendR = Validator.Build<string>()
                          .WithRule(new IsNotNullOrEmptyRule<string>(), "O Envio /Receção é obrigatório.");
            Classifier = Validator.Build<string>()
                          .WithRule(new IsNotNullOrEmptyRule<string>(), "O Classificador é obrigatório.");
            ProcessesType = Validator.Build<string>()
                              .WithRule(new IsNotNullOrEmptyRule<string>(),
                                 "O Tipo Proceso  é obrigatório.");

            entity = GetEntities("");
        }
        private ObservableCollection<IdName<int, string>> _entity;
        public ObservableCollection<IdName<int, string>> entity
        {
            get => _entity;
            set
            {
                _entity = value;
                OnPropertyChanged(); 
            }
        }
        public Validatable<string> Topic { get; set; }
        public Validatable<string> BookR { get; set; }
        public Validatable<string> Type { get; set; }
        public Validatable<string> ProcessesType { get; set; }
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

        public ObservableCollection<IdName<int, string>> ProcessType
        {
            get
            {

                return AppDefaultData.ProcessType;
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

        public ObservableCollection<IdName<int, string>> GetEntities(string search)
        {
            // Inicializa a coleção, se necessário
            if (_entity == null)
            {
                _entity = new ObservableCollection<IdName<int, string>>();
            }

            var result = _documentService.GetEntity(search, out bool navigateToLogin);
            if (result.Status == Models.ReturnStatus.Success)
            {
                // Atualize a coleção com o resultado
                return new ObservableCollection<IdName<int, string>>(
                    result.Result.Select(d => new IdName<int, string>
                    {
                        Id = d.Id,
                        Name = d.Name
                    }));
            }
            else
            {
                _alertService.ShowAlert("Erro", result.Error);
                if (navigateToLogin)
                {
                    _navigationService._PushAsyncWithCleanup(new LoginPageMobile(App.UserDetails?.CodEntidade));
                }
            }

            return new ObservableCollection<IdName<int, string>>();
        }
        public DocumentResponse Insert(NewDocumentRequest doc)
        {
            GbLoader = true;
            DocumentResponse response = null;
            var results = _documentService.InsertTest(doc, out bool navigateToLogin, out GenericResponse<DocumentResponse> result);
            if (result.Status == Models.ReturnStatus.Success)
                response = result.Result;
            else
            {

                _alertService.ShowAlert("Erro", result.Error);

                if (navigateToLogin)
                    _navigationService._PushAsyncWithCleanup(new LoginPageMobile(App.UserDetails?.CodEntidade));

            }
            GbLoader = false;

            return response;
        }


    }
}

