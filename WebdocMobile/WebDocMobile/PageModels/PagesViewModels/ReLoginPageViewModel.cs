using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebDocMobile.Helpers;
using WebDocMobile.Helpers.WsMethods;
using WebDocMobile.Models;
using WebDocMobile.Models.BookService;
using WebDocMobile.Models.DashBoard;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Services;

namespace WebDocMobile.PageModels
{
    public partial class ReLoginPageViewModel : ObservableObject
    {
        private readonly INavigation _navigationService;
        private readonly IGetDocumentService _documentService;
        private readonly IGetDashBoardService _dashBoardService;
        private readonly IAlertService _alertService;
        private readonly ILoginService _loginService;
        private string currentPIN;
        private int currentDigit;
        [ObservableProperty]
        public bool gbLoader;


        public ReLoginPageViewModel(INavigation navigationService)
        {
            _navigationService = navigationService;
            _alertService = new AlertService();
            _documentService = new GetDocumentService();
            _dashBoardService = new DashBoardService();
            _loginService = new LoginService();
            gbLoader = false;
            InitialPINSet();
        }

        public async Task LoadData()
        {
            GenericResponse<LoadDefaultDataDto> data = _documentService.GetDocumentData(out bool _navegateToLogin);
            if (_navegateToLogin)
            {
                _alertService.ShowAlert("Erro", "Token inválido");
                await _navigationService._PushAsyncWithCleanup(new LoginPageMobile(App.UserDetails?.CodEntidade));
            }
            else if (data.Status == ReturnStatus.Success)
            {
                APIHelper.FillFormDocumentData(data.Result);
            }
            else
            {
                _alertService.ShowAlert("Erro", data.Error);
            }
        }

        private async void PINComplete()
        {
            if (currentDigit == 6 && currentPIN.Length == 6)
            {
                GbLoader = true;
                await Task.Delay(100);
                UserBasicInfo user = App.UserDetails;
                if (user.PIN != currentPIN)
                {
                    _alertService.ShowAlert("Erro", "Ping errado");
                    InitialPINSet();
                }
                else
                {
                    var url = UserBasicInfo.GetEntityURL(user.CodEntidade);
                    if (!string.IsNullOrWhiteSpace(url))
                    {
                        App.BaseAddress = url;
                        if (!_loginService.SetPublicToken())
                        {
                            await ErrorToLogin(user, "Erro ao acessar a API");
                        }
                        else
                        {
                            if (!_loginService.LoginUserBasic(user.StrName, user.Password, out GenericResponse<LoginResponse> result, out _))
                            {
                                await ErrorToLogin(user, result.Error);
                            }
                            else
                            {
                                user.Token = result.Result.Token;
                                user.Save();
                            }
                            
                            await LoadData();
                            await _navigationService._PushAsyncWithCleanup(new MainMenuPageMobile());

                        }
                    }
                    else
                    {
                        await ErrorToLogin(user, "Entidade incorreta");
                    }
                }
                GbLoader = false;
            }
            else
            {
                InitialPINSet();
            }
        }

        private Task ErrorToLogin(UserBasicInfo user, string message)
        {
            _alertService.ShowAlert("Erro", message);
            return _navigationService._PushAsyncWithCleanup(new LoginPageMobile(user.CodEntidade));
        }

        [RelayCommand]
        private void Digit1Pressed()
        {
            currentPIN = currentPIN + "1";
            currentDigit++;
            if (currentDigit == 6) { PINComplete(); }
        }
        [RelayCommand]
        private void Digit2Pressed()
        {
            currentPIN = currentPIN + "2";
            currentDigit++;
            if (currentDigit == 6) { PINComplete(); }
        }
        [RelayCommand]
        private void Digit3Pressed()
        {
            currentPIN = currentPIN + "3";
            currentDigit++;
            if (currentDigit == 6) { PINComplete(); }
        }
        [RelayCommand]
        private void Digit4Pressed()
        {
            currentPIN = currentPIN + "4";
            currentDigit++;
            if (currentDigit == 6) { PINComplete(); }
        }
        [RelayCommand]
        private void Digit5Pressed()
        {
            currentPIN = currentPIN + "5";
            currentDigit++;
            if (currentDigit == 6) { PINComplete(); }
        }
        [RelayCommand]
        private void Digit6Pressed()
        {
            currentPIN = currentPIN + "6";
            currentDigit++;
            if (currentDigit == 6) { PINComplete(); }
        }
        [RelayCommand]
        private void Digit7Pressed()
        {
            currentPIN = currentPIN + "7";
            currentDigit++;
            if (currentDigit == 6) { PINComplete(); }
        }
        [RelayCommand]
        private void Digit8Pressed()
        {
            currentPIN = currentPIN + "8";
            currentDigit++;
            if (currentDigit == 6) { PINComplete(); }
        }
        [RelayCommand]
        private void Digit9Pressed()
        {
            currentPIN = currentPIN + "9";
            currentDigit++;
            if (currentDigit == 6) { PINComplete(); }
        }
        [RelayCommand]
        private void Digit0Pressed()
        {
            currentPIN = currentPIN + "0";
            currentDigit++;
            if (currentDigit == 6) { PINComplete(); }
        }
        [RelayCommand]
        private void DeleteDigitPressed()
        {
            if (currentDigit > 0)
            {
                string newPIN = currentPIN.Substring(0, currentPIN.Length - 1);
                currentPIN = newPIN;
                currentDigit--;
            }
        }

        //[RelayCommand]
        //private void BiometricButtonPressed()
        //{
        //    _alertService.ShowAlert("Biometric", "This will be the biometric");
        //}

        [RelayCommand]
        private void ReturnButtonPressed()
        {
            _navigationService._PushAsyncWithCleanup(new LoginPageMobile(App.UserDetails.CodEntidade));
        }
        [RelayCommand]
        private void ReturnButtonLoginPressed()
        {
            _navigationService._PushAsyncWithCleanup(new LoginPageMobile(App.UserDetails.CodEntidade));
        }
        private void InitialPINSet()
        {
            currentDigit = 0;
            currentPIN = string.Empty;
        }
    }
}
