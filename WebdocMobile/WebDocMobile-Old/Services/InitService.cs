using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebDocMobile.Models.InitService;

namespace WebDocMobile.Services
{
    public interface IInitService
    {
        Task<InitDto> Init();
    }

    public class InitService : IInitService
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public InitService()
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

        public async Task<InitDto> Init()
        {
            InitDto dto = new InitDto();
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No Internet Connection");
                return dto;
            }

            try
            {
                HttpResponseMessage response;
                if (App.codigoEntidade == "1994")
                {
                    response = await _httpClient.GetAsync($"{_url}/Init/Init");
                }
                else if(App.codigoEntidade == "1995")
                {
                    response = await _httpClient.PostAsync($"{_url}/Init", new StringContent(String.Empty));
                }
                else
                {
                    response = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
                }


                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    dto = JsonSerializer.Deserialize<InitDto>(content, _jsonSerializerOptions);

                    Debug.WriteLine(dto.ToString());
                }
                else
                {
                    Debug.WriteLine("No Http Response");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return dto;
        }
    }
}
