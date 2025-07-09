// These 'using' statements import necessary libraries.
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using WebDocMobile.Helpers.WsMethods;
using WebDocMobile.Models;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Services;

namespace WebDocMobile.PageModels
{
    // [QueryProperty] is the modern way to receive data when a page is navigated to.
    // It links the "codEntidade" parameter from the navigation URI to the CodigoEntidade property below.
    [QueryProperty(nameof(CodigoEntidade), "codEntidade")]
    public partial class LoginPageViewModel : ObservableObject // Inheriting from ObservableObject is part of the MVVM Toolkit.
    {
        // --- Properties ---
        // [ObservableProperty] is a source generator from the MVVM toolkit.
        // It automatically creates a public property that notifies the UI when its value changes.
        // This replaces the need for manual get/set with PropertyChanged notifications.
        [ObservableProperty]
        private string strUserName;

        [ObservableProperty]
        private string strPassword;

        [ObservableProperty]
        private string strDomainName;

        // This property will be set automatically by the navigation system via the [QueryProperty] attribute above.
        [ObservableProperty]
        private string codigoEntidade;

        // --- Services ---
        // These are readonly fields to hold the services that will be provided by Dependency Injection.
        private readonly ILoginService _loginService;
        private readonly IInitService _initService;
        private readonly IDocumentService _documentService;
        private readonly IAlertService _alertService;
        private readonly ISettingsService _settingsService;
        private readonly IAppStateService _appStateService;

        // --- Constructor ---
        // This constructor now receives all its dependencies automatically from the DI container
        // that we configured in MauiProgram.cs. This is called "Constructor Injection".
        public LoginPageViewModel(
            ILoginService loginService,
            IInitService initService,
            IDocumentService documentService,
            IAlertService alertService,
            ISettingsService settingsService,
            IAppStateService appStateService)
        {
            // Assign the injected services to our private fields.
            _loginService = loginService;
            _initService = initService;
            _documentService = documentService;
            _alertService = alertService;
            _settingsService = settingsService;
            _appStateService = appStateService;
        }

        // This is a "partial method" that is automatically called by the source generator
        // whenever the CodigoEntidade property's value changes.
        partial void OnCodigoEntidadeChanged(string value)
        {
            // Once we receive the entity code from navigation, we can set the domain name.
            ProcessCodEntidade(value);
        }

        private void ProcessCodEntidade(string codEntidade)
        {
            switch (codEntidade)
            {
                case "1994":
                    StrDomainName = "Ambisig"; // Note: The property name is now PascalCase (StrDomainName)
                    break;
                case "1995":
                    StrDomainName = "INTERNAL";
                    break;
            }
        }

        // [RelayCommand] from the MVVM toolkit creates a bindable ICommand property for the UI.
        // This is cleaner than defining a separate ICommand property for each method.
        [RelayCommand]
        public async Task HandleLogIn()
        {
            Debug.WriteLine("strUserName: " + StrUserName);
            Debug.WriteLine("strPassword: " + StrPassword);
            Debug.WriteLine("strDomainName: " + StrDomainName);

            // 1. Get a session hash code from the Init service.
            var initComponents = await _initService.Init();
            string strHashCode = initComponents.hashCode;

            Debug.WriteLine("Generated HashCode: " + strHashCode);

            // 2. Attempt to log in with the user's credentials.
            bool isLoginSuccessful = await _loginService.LoginUserBasic(strHashCode, StrUserName, StrPassword, StrDomainName);

            if (isLoginSuccessful)
            {
                // 3. If login is successful, create the user's session object.
                var userDetails = new UserBasicInfo
                {
                    strHashCode = strHashCode,
                    strName = StrUserName,
                    strDomain = StrDomainName,
                    codEntidade = CodigoEntidade,
                    PIN = string.Empty,
                    hasBiometricsActive = false
                };

                // 4. Use the services to manage state.
                _settingsService.UserInfo = userDetails; // Persists user info between sessions.
                _appStateService.UserDetails = userDetails; // Sets user info for the current session.

                // 6. Fetch all necessary documents and store them in the global App state.
                await GetDocuments();

                // 7. Navigate to the PIN setup page, replacing the current navigation stack.
                // The "//" prefix tells Shell to create a new root page, so the user can't go "back" to the login screen.
                // This is much simpler than manually manipulating the NavigationStack.
                await Shell.Current.GoToAsync($"//{nameof(PINPageMobile)}");
            }
            else
            {
                // If login fails, show an alert and clear any old user details.
                await _alertService.ShowAlert("Erro", "Dados Incorretos");
                _settingsService.UserInfo = null;
            }
        }

        // This method fetches and categorizes all the documents after a successful login.
        public async Task GetDocuments()
        {
            // Fetch the main lists of documents from the server.
            var myDocuments = await _documentService.ListMyDocuments(_appStateService.UserDetails.strHashCode, 1000, 1);
            var allDocuments = await _documentService.ListDocuments(_appStateService.UserDetails.strHashCode, 1000, 1);
            var allMyDocuments = await _documentService.ListAllMyDocuments(_appStateService.UserDetails.strHashCode, 1000, 1, "");

            // Use HashSets for efficient lookups (O(1) average time complexity).
            // This is much faster than using List.Any() inside a loop (O(N*M)).
            var allMyDocumentsCodes = allMyDocuments.Select(d => d.Code).ToHashSet();
            var myDocumentsCodes = myDocuments.Select(d => d.Code).ToHashSet();

            // Documents for "Conhecimento" are those in the general list but not assigned to the user.
            var knownDocuments = allDocuments.Where(d => !allMyDocumentsCodes.Contains(d.Code)).ToList();
            // Documents for "Departamento" are those assigned to the user's group but not directly to them.
            var departmentDocuments = allMyDocuments.Where(d => !myDocumentsCodes.Contains(d.Code)).ToList();

            // Update the application state service with the fetched and categorized lists.
            _appStateService.AllDocuments = allDocuments;
            _appStateService.MyDocuments = myDocuments;
            _appStateService.DepartmentDocuments = departmentDocuments;
            _appStateService.KnownDocuments = knownDocuments;
        }
    }
}
