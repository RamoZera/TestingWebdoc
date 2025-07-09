using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using WebDocMobile.Models.LoginService;

namespace WebDocMobile.Services
{
    public interface ILoginService
    {
        Task<Boolean> LoginUserBasic(string hashCode, string username, string password, string domain);
    }

    public class LoginService : ILoginService
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public LoginService()
        {
#if DEBUG
            HttpClientHandler insecureHandler = GetInsecureHandler();
            _httpClient = new HttpClient(insecureHandler);
#else
            _httpClient = new HttpClient();
#endif
            if (App.codigoEntidade == "1994")
            {
                _url = $"{App.baseAddress}/api/v1";
            }
            else if (App.codigoEntidade == "1995")
            {
                _url = $"{App.baseAddress}/wsservices/wsgetinfo.asmx";
            }

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }

        public async Task<Boolean> LoginUserBasic(string hashCode, string username, string password, string domain)
        {
            bool loginStatus = false;
            if(Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No Internet Connection");
                return loginStatus;
            }

            try
            {
                LoginUserBasicDto dto = new LoginUserBasicDto()
                {
                    strHashCode = hashCode,
                    strUserName = username,
                    strPassword = password,
                    strDomainName = domain
                };
                StringContent content = new StringContent(JsonSerializer.Serialize(dto, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                HttpResponseMessage response;
                if(App.codigoEntidade == "1994")
                {
                    response = await _httpClient.PostAsync($"{_url}/Login/loginUserBasic", content);
                }
                else if(App.codigoEntidade == "1995")
                {
                    response = await _httpClient.PostAsync($"{_url}/loginUserBasic", content);
                }
                else
                {
                    response = new HttpResponseMessage(HttpStatusCode.NotFound);
                }

                if(response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Request was successful");

                    string contentToReceive = await response.Content.ReadAsStringAsync();

                    loginStatus = JsonSerializer.Deserialize<bool>(contentToReceive, _jsonSerializerOptions);

                    if (loginStatus)
                    {
                        Debug.WriteLine("User " + username + " is signed in");
                    }
                    else
                    {
                        Debug.WriteLine("Username or password incorret");
                    }
                    return loginStatus;
                }
                else
                {
                    Debug.WriteLine("Request failed");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);              
            }
            return loginStatus;
        }
    }
}
