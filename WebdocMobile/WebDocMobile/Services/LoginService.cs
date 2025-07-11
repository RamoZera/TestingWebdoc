using System.Net.Security;
using System.Text;
using System.Text.Json;
using WebDocMobile.Helpers;
using WebDocMobile.Models;

namespace WebDocMobile.Services
{
    public interface ILoginService
    {
        bool LoginUserBasic(string username, string password, out GenericResponse<LoginResponse> result, out bool navigateToLogin);

        bool SetPublicToken();
    }

    public class LoginService: ILoginService
    {
        private readonly HttpClient _httpClient;
        private string _token;

        public LoginService()
        {
            _httpClient = new HttpClient();
            APIHelper.CheckForInternetConnection();
        }

        public HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if(cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == SslPolicyErrors.None;
            };
            return handler;
        }

        private GenericResponse<string> GetPublicToken()
        {
            APIHelper.CheckForInternetConnection();
            _httpClient._SetJSONMediaType();
            return _httpClient._CallAPI<string>($"{App.BaseAddress}api/get_publictoken", null, out _);
        }

        public bool SetPublicToken()
        {
            var result = GetPublicToken();
            if(result.Status == ReturnStatus.Success)
            {
                _token = result.Result;
                return true;
            }
            _token = null;
            return false;
        }

        private GenericResponse<LoginResponse> Authenticate(string pUser, string pPassword, out bool navigateToLogin)
        {
            APIHelper.CheckForInternetConnection();
            _httpClient._StandarSetup(_token);
            return _httpClient._CallAPI<LoginResponse>($"{App.BaseAddress}user/authenticate", new StringContent(JsonSerializer.Serialize(new LoginInfo()
            {
                username = pUser,
                password = pPassword
            }), Encoding.UTF8, APIHelper.jsonMime), out navigateToLogin);
        }

        public bool LoginUserBasic(string username, string password, out GenericResponse<LoginResponse> result, out bool navigateToLogin)
        {
            APIHelper.CheckForInternetConnection();
            _httpClient._StandarSetup(_token);
            result = Authenticate(username, password, out navigateToLogin);
            return result.Status == ReturnStatus.Success;
        }
    }
}
