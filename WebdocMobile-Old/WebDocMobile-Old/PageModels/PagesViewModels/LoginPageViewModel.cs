﻿﻿﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using WebDocMobile.Helpers.WsMethods;
using WebDocMobile.Models;
using WebDocMobile.Pages.Desktop;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Services;

namespace WebDocMobile.PageModels
{
    [QueryProperty(nameof(CodigoEntidade), "codEntidade")]
    public partial class LoginPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string strUserName;
        [ObservableProperty]
        private string strPassword;
        [ObservableProperty]
        private string strDomainName;
        [ObservableProperty]
        private string codigoEntidade;

        private readonly ILoginService _loginService;
        private readonly IInitService _initService;
        private readonly IDocumentService _documentService;
        private readonly IAlertService _alertService;
        private readonly IAppStateService _appStateService;
        private readonly ISettingsService _settingsService;

        public LoginPageViewModel(ILoginService loginService, IInitService initService, IDocumentService documentService, IAlertService alertService, IAppStateService appStateService, ISettingsService settingsService)
        {
            _loginService = loginService;
            _initService = initService;
            _documentService = documentService;
            _alertService = alertService;
            _appStateService = appStateService;
            _settingsService = settingsService;
        }

        private void ProcessCodEntidade(string codEntidade)
        {
            switch (codEntidade)
            {
                case "1994":
                    StrDomainName = "Ambisig";
                    break;
                case "1995":
                    StrDomainName = "INTERNAL";
                    break;
                case "DEMO":
                    // You may need to ask your colleague for the correct domain for this environment
                    StrDomainName = "DEMO_DOMAIN"; 
                    break;
            }
        }

        partial void OnCodigoEntidadeChanged(string value)
        {
            ProcessCodEntidade(value);
        }

        [RelayCommand]
        public async Task HandleLogIn()
        {
            var initComponents = await _initService.Init();
            if (initComponents == null)
            {
                await _alertService.ShowAlertAsync("Erro", "Não foi possível inicializar a sessão. Verifique a ligação e o código de entidade.");
                return;
            }
            string strHashCode = initComponents.hashCode;

            bool isLoginSuccessful = await _loginService.LoginUserBasic(strHashCode, StrUserName, StrPassword, StrDomainName);

            if (isLoginSuccessful)
            {
                var userDetails = new UserBasicInfo
                {
                    strHashCode = strHashCode,
                    strName = StrUserName,
                    strDomain = StrDomainName,
                    codEntidade = CodigoEntidade,
                    PIN = string.Empty,
                    hasBiometricsActive = false
                };

                _settingsService.UserInfo = userDetails;
                _appStateService.UserDetails = userDetails;

                await GetDocuments();

#if ANDROID || IOS
                await Shell.Current.GoToAsync($"//{nameof(PINPageMobile)}");
#else
                await Shell.Current.GoToAsync($"//{nameof(PINPageDesktop)}");
#endif
            }
            else
            {
                await _alertService.ShowAlertAsync("Erro", "Dados Incorretos");
            }
        }

        public async Task GetDocuments()
        {
            if (_appStateService.UserDetails == null) return;

            var myDocuments = await _documentService.ListMyDocuments(_appStateService.UserDetails.strHashCode, 1000, 1);
            var allDocuments = await _documentService.ListDocuments(_appStateService.UserDetails.strHashCode, 1000, 1);
            var allMyDocuments = await _documentService.ListAllMyDocuments(_appStateService.UserDetails.strHashCode, 1000, 1, "");

            var departmentDocuments = new List<GDDocument>();
            var knownDocuments = new List<GDDocument>();

            // This logic can be optimized, but we'll keep it for now.
            if (allDocuments != null && allMyDocuments != null)
            {
                knownDocuments = allDocuments.Where(doc => !allMyDocuments.Any(myDoc => myDoc.Code == doc.Code)).ToList();
            }
            if (allMyDocuments != null && myDocuments != null)
            {
                departmentDocuments = allMyDocuments.Where(myDoc => !myDocuments.Any(doc => doc.Code == myDoc.Code)).ToList();
            }

            _appStateService.AllDocuments = allDocuments ?? new List<GDDocument>();
            _appStateService.MyDocuments = myDocuments ?? new List<GDDocument>();
            _appStateService.DepartmentDocuments = departmentDocuments;
            _appStateService.KnownDocuments = knownDocuments;
        }
    }
}
