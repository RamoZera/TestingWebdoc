using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.ValidationRules;
using Plugin.ValidationRules.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocMobile.Helpers;
using WebDocMobile.Models;
using WebDocMobile.Models.BookService;
using WebDocMobile.Models.NewDocument;
using WebDocMobile.Models.NewProcesses;
using WebDocMobile.Models.ValidationRules;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Services;

namespace WebDocMobile.PageModels.PagesViewModels
{
    public partial class ComprovativoPageViewModel : ExtendedPropertyChanged
    {

        public string date { get; set; }
        private INavigation _navigationService;
        private bool _navigateToLogin;
        private readonly IAlertService _alertService;
        private readonly IGetDocumentService _documentService;
        private readonly IGetProcessService _processService;

        private ObservableCollection<IdName<int, string>> books { get; set; } 
        private ObservableCollection<DocumentTypeDto> types { get; set; } 
        private ObservableCollection<IdName<int, string>> classifiers { get; set; } 

        public ComprovativoPageViewModel(INavigation navigation):this()
        {
            this._navigationService = navigation;
           
        }
        public ComprovativoPageViewModel()
        {
            _alertService = new AlertService();
            _documentService = new GetDocumentService();
            _processService = new GetProcessService();
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

            entity = GetEntities("");
        }
        private ObservableCollection<IdName<int, string>> _entity;
        public ObservableCollection<IdName<int, string>> entity
        {
            get => _entity;
            set
            {
                _entity = value;
            }
        }

        public Validatable<string> Topic { get; set; }
        public Validatable<string> BookR { get; set; }
        public Validatable<string> Type { get; set; }
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

        public bool UpdateStateCheckOut(DocumentMetadataRequest request, bool CheckOut)
        {
            var result = _documentService.UpdateDocumentCheckOut(request, CheckOut, out _navigateToLogin);
            if (result.Result)
                return true;
            else
            {
                _alertService.ShowAlert("Erro", result.Error);
                if (_navigateToLogin)
                    _navigationService._PushAsyncWithCleanup(new LoginPageMobile(App.UserDetails?.CodEntidade));

                return false;
            }

        }

        public async void GetDocumentFile(int Id)
        {

            var result = _documentService.GetDocumentFile(Id, out _navigateToLogin);
            if (result.Status == Models.ReturnStatus.Success)
            {
                var filePath = Path.Combine(FileSystem.Current.CacheDirectory, Guid.NewGuid() + result.Result.Ext);
                await File.WriteAllBytesAsync(filePath, result.Result.Data);
                await OpenFileAsync(filePath);
            }
            else
            {
                _alertService.ShowAlert("Erro", result.Error);
                if (_navigateToLogin)
                    await _navigationService._PushAsyncWithCleanup(new LoginPageMobile(App.UserDetails?.CodEntidade));

            }

        }
        public async void GetProcessFile(int Id)
        {

            var result = _processService.GetProcessFile(Id, out _navigateToLogin);
            if (result.Status == Models.ReturnStatus.Success)
            {
                var filePath = Path.Combine(FileSystem.Current.CacheDirectory, Guid.NewGuid() + result.Result.Ext);
                await File.WriteAllBytesAsync(filePath, result.Result.Data);
                await OpenFileAsync(filePath);
            }
            else
            {
                _alertService.ShowAlert("Erro", result.Error);
                if (_navigateToLogin)
                    await _navigationService._PushAsyncWithCleanup(new LoginPageMobile(App.UserDetails?.CodEntidade));

            }

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
        public async Task OpenFileAsync(string filePath)
        {
            await Launcher.Default.OpenAsync(new OpenFileRequest
            {
                File = new ReadOnlyFile(filePath)
            });
        }

    }
}
