using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocMobile.Pages.Desktop;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Services;
using WebDocMobile.Helpers;
using WebDocMobile.Models;
using Newtonsoft.Json;
using WebDocMobile.Helpers.WsMethods;

namespace WebDocMobile.PageModels
{
    public partial class LoginPageViewModel
    {
        public string strUserName { get; set; }
        public string strPassword { get; set; }
        public string strDomainName { get; set; }
        private string codEntidade { get; set; }

        private readonly ILoginService _loginService;
        private readonly IInitService _initService;
        private readonly IDocumentService _documentService;
        private readonly IAlertService _alertService;
        private INavigation _navigationService;

        public LoginPageViewModel(INavigation navigation, string codEntidade)
        {
            this._navigationService = navigation;

            _loginService = new LoginService();
            _initService = new InitService();
            _documentService = new DocumentService();
            _alertService = new AlertService();

            this.codEntidade = codEntidade;
            ProcessCodEntidade(codEntidade);
        }

        private void ProcessCodEntidade(string codEntidade)
        {
            switch(codEntidade)
            {
                case "1994":
                    strDomainName = "Ambisig";
                    break;
                case "1995":
                    strDomainName = "INTERNAL";
                    break;
            }
        }

        [RelayCommand]
        public async void HandleLogIn()
        {
            Debug.WriteLine("strUserName: " + strUserName);
            Debug.WriteLine("strPassword: " + strPassword);
            Debug.WriteLine("strDomainName: " + strDomainName);

            var initComponents = await _initService.Init();
            string strHashCode = initComponents.hashCode;

            Debug.WriteLine("Generated HashCode: " + strHashCode);

            bool IsLoginSuccessful = await _loginService.LoginUserBasic(strHashCode, strUserName, strPassword, strDomainName);

            if (IsLoginSuccessful)
            {
                var userDetails = new UserBasicInfo
                {
                    strHashCode = strHashCode,
                    strName = strUserName,
                    strDomain = strDomainName,
                    codEntidade = codEntidade,
                    PIN = string.Empty,
                    hasBiometricsActive = false
                };

                if(Preferences.ContainsKey(nameof(App.UserDetails)))
                {
                    Preferences.Remove(nameof(App.UserDetails));
                }
                string userDetailsStr = JsonConvert.SerializeObject(userDetails);
                Preferences.Set(nameof(App.UserDetails), userDetailsStr);
                App.UserDetails = userDetails;
                GetDocuments(_documentService);
#if ANDROID || IOS
                var list = _navigationService.NavigationStack;
                int x = 0;
            while(x < list.Count)
                {
                    Page p = list[x];
                    if (!(list[x] is PINPageMobile))
                    {
                        if (list[x] is LoginPageMobile)
                        {
                            _navigationService.InsertPageBefore(new PINPageMobile(), list[x]);
                        }
                        _navigationService.RemovePage(p);
                    }
                    else
                    {
                        x++;
                    }
                }
#else
                var list = _navigationService.NavigationStack;
                int x = 0;
            while(x < list.Count)
                {
                    Page p = list[x];
                    if (!(list[x] is PINPageDesktop))
                    {
                        if (list[x] is LoginPageDesktop)
                        {
                            _navigationService.InsertPageBefore(new PINPageDesktop(), list[x]);
                        }
                        _navigationService.RemovePage(p);
                    }
                    else
                    {
                        x++;
                    }
                }
#endif

            }
            else
            {
                _alertService.ShowAlert("Erro", "Dados Incorretos");
                Preferences.Remove(nameof(App.UserDetails));
            }
        }

        public async void GetDocuments(IDocumentService _documentService)
        {
            var myDocuments = await _documentService.ListMyDocuments(App.UserDetails.strHashCode, 1000, 1);
            var allDocuments = await _documentService.ListDocuments(App.UserDetails.strHashCode, 1000, 1);
            var allMyDocuments = await _documentService.ListAllMyDocuments(App.UserDetails.strHashCode, 1000, 1, "");

            var departmentDocuments = new List<GDDocument>();
            var knownDocuments = new List<GDDocument>();


            foreach (GDDocument document in allDocuments)
            {
                bool isKnownDoc = true;
                foreach (GDDocument myDoc in allMyDocuments)
                {
                    if (document.Code == myDoc.Code)
                    {
                        isKnownDoc = false;
                    }
                }
                if (isKnownDoc)
                {
                    knownDocuments.Add(document);
                }
            }
            foreach (GDDocument document in allMyDocuments)
            {
                bool isDepDoc = true;
                foreach (GDDocument doc in myDocuments)
                {
                    if (document.Code == doc.Code)
                    {
                        isDepDoc = false;
                    }
                }
                if (isDepDoc)
                {
                    departmentDocuments.Add(document);
                }
            }

            if (Preferences.ContainsKey(nameof(App.allDocuments)) || Preferences.ContainsKey(nameof(App.myDocuments)) ||
                Preferences.ContainsKey(nameof(App.departmentDocuments)) || Preferences.ContainsKey(nameof(App.knownDocuments)))
            {
                Preferences.Remove(nameof(App.allDocuments));
                Preferences.Remove(nameof(App.myDocuments));
                Preferences.Remove(nameof(App.departmentDocuments));
                Preferences.Remove(nameof(App.knownDocuments));
            }
            Preferences.Set(nameof(App.allDocuments), JsonConvert.SerializeObject(allDocuments));
            Preferences.Set(nameof(App.myDocuments), JsonConvert.SerializeObject(myDocuments));
            Preferences.Set(nameof(App.departmentDocuments), JsonConvert.SerializeObject(departmentDocuments));
            Preferences.Set(nameof(App.knownDocuments), JsonConvert.SerializeObject(knownDocuments));
            App.allDocuments = allDocuments;
            App.myDocuments = myDocuments;
            App.departmentDocuments = departmentDocuments;
            App.knownDocuments = knownDocuments;
        }
    }
}
