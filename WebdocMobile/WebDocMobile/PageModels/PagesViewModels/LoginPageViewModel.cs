using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using WebDocMobile.Helpers;
using WebDocMobile.Helpers.WsMethods;
using WebDocMobile.Models;
using WebDocMobile.Models.BookService;
using WebDocMobile.Models.DashBoard;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Services;
using static WebDocMobile.Models.UserBasicInfo;

namespace WebDocMobile.PageModels
{
    public partial class LoginPageViewModel: ObservableObject
    {
        public string strUserName { get; set; }
        public string strPassword { get; set; }
        public string strDomainName { get; set; }
        public string codEntidade { get; set; }
        public ObservableCollection<URLIDName> EntitiesLogin { get; set; }

        private readonly ILoginService _loginService;
        private readonly IInitService _initService;
        private readonly IGetDocumentService _documentService;
        private readonly IAlertService _alertService;
        private INavigation _navigationService;

        [ObservableProperty]
        public bool gbLoader;
        [ObservableProperty]
        public bool gbModalError;
        [ObservableProperty]
        public string gbIconModalError;
        [ObservableProperty]
        public string gbTitleModalError;
        [ObservableProperty]
        public string gbTextModalError;
        [ObservableProperty]
        private bool isPasswordVisible;
        [ObservableProperty]
        public string iconPassword;


        public LoginPageViewModel(INavigation navigation, string codEntidade)
        {
            GetEntitiesLogin();
            _navigationService = navigation;
            _loginService = new LoginService();
            _initService = new InitService();
            _documentService = new GetDocumentService();
            _alertService = new AlertService();
            this.codEntidade = codEntidade;
            gbLoader = false;
            gbModalError = false;
            gbIconModalError = "icon_erroconta";
            gbTitleModalError = string.Empty;
            gbTextModalError = string.Empty;
            isPasswordVisible = true;
            iconPassword = "icon_verpass";
        }


        [RelayCommand]
        public async Task HandleLogIn()
        {
            GbLoader = true;
            await Task.Delay(100);
            if(string.IsNullOrWhiteSpace(App.BaseAddress))
            {
                SetError("Deve selecionar uma entidade");
            }
            else if(!_loginService.SetPublicToken())
            {
                SetError("Erro ao acessar a API");
            }
            else
            {
                if(_loginService.LoginUserBasic(strUserName, strPassword, out GenericResponse<LoginResponse> result, out bool navigateToLogin))
                {
                    UserBasicInfo user = App.UserDetails;
                    user.StrName = strUserName;
                    user.StrDomain = strDomainName;
                    user.CodEntidade = codEntidade;
                    user.Password = strPassword;
                    user.Token = result.Result.Token;
                    user.Save();
#if ANDROID || IOS
                    await LoadData();
                    if(user.PingCorrect())
                        await _navigationService._PushAsyncWithCleanup(new MainMenuPageMobile());
                    else
                        await _navigationService.PushAsync(new PINPageMobile());
#endif
                }
                else
                {
                    SetError(result.Error);
                }
            }
        }

        [RelayCommand]
        public void FecharModalClicked()
        {
            GbModalError = false;
        }

        public async Task LoadData()
        {
            GenericResponse<LoadDefaultDataDto> data = _documentService.GetDocumentData(out bool navegateToLogin);
            if(data.Status == ReturnStatus.Success)
            {
                APIHelper.FillFormDocumentData(data.Result);
            }
            else
            {
                SetError(data.Error);
                if(navegateToLogin)
                    await _navigationService._PushAsyncWithCleanup(new LoginPageMobile(App.UserDetails?.CodEntidade));
            }
        }

        private void SetError(string message)
        {
            GbLoader = false;
            GbTitleModalError = "Erro";
            GbTextModalError = message;
            GbModalError = true;
        }

        private void GetEntitiesLogin()
        {
            EntitiesLogin = new ObservableCollection<UserBasicInfo.URLIDName>(UserBasicInfo.EntityCodes);
        }

        [RelayCommand]
        public void TogglePasswordVisibility()
        {
            IsPasswordVisible = !IsPasswordVisible;

            if (IsPasswordVisible) IconPassword = "icon_verpass"; else IconPassword = "icon_esconderpass";
        }
    }
    public class PasswordIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }
    }

}