using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WebDocMobile.Models.InitService;

namespace WebDocMobile.Services
{
    public class InitService : IInitService
    {
        private readonly HttpClient _httpClient;
        private readonly ISettingsService _settingsService;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        // The service now receives its dependencies via constructor injection.
        public InitService(ISettingsService settingsService)
        {
            _settingsService = settingsService;

#if DEBUG
            // This handler is useful for development when using self-signed certificates (e.g., for localhost).
            HttpClientHandler insecureHandler = GetInsecureHandler();
            _httpClient = new HttpClient(insecureHandler);
#else
            _httpClient = new HttpClient();
#endif

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                // Using CamelCase is a common convention for JSON APIs.
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        // This method should only be used in DEBUG builds to bypass SSL certificate validation.
        // It is not safe for production environments.
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
                // Get the required settings from the service instead of a static class.
                string codigoEntidade = _settingsService.CodigoEntidade;
                string baseAddress = _settingsService.BaseAddress;

                HttpResponseMessage response;
                if (codigoEntidade == "1994")
                {
                    // Construct the full URL at the time of the request.
                    var url = $"{baseAddress}/api/v1/Init/Init";
                    response = await _httpClient.GetAsync(url);
                }
                else if(codigoEntidade == "1995")
                {
                    // This appears to be a SOAP endpoint that requires a POST.
                    var url = $"{baseAddress}/wsservices/wsgetinfo.asmx/Init";
                    response = await _httpClient.PostAsync(url, new StringContent(string.Empty));
                }
                else
                {
                    // If the entity code is not recognized, return a "Not Found" response.
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
                    Debug.WriteLine($"Http Response Error: {response.StatusCode}");
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
