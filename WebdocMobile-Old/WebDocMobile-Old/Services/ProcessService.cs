using Newtonsoft.Json;
using System.Diagnostics;
using WebDocMobile.Models.ProcessService;

namespace WebDocMobile.Services
{
    public interface IProcessService
    {
        Task<string> GetWsSoapAddress();
    }

    public class ProcessService : IProcessService
    {
        private readonly ISettingsService _settingsService;
        private readonly HttpClient _httpClient;

        public ProcessService(ISettingsService settingsService)
        {
            _settingsService = settingsService;
#if DEBUG
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };
            _httpClient = new HttpClient(handler);
#else
            _httpClient = new HttpClient();
#endif
        }

        public async Task<string> GetWsSoapAddress()
        {
            try
            {
                var baseAddress = _settingsService.BaseAddress;
                if (string.IsNullOrEmpty(baseAddress))
                {
                    Debug.WriteLine("Error: BaseAddress is not set in SettingsService.");
                    return string.Empty;
                }

                _httpClient.BaseAddress = new Uri(baseAddress);
                var response = await _httpClient.GetAsync("/Process/GetWsSoapAddress");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in ProcessService.GetWsSoapAddress: {ex.Message}");
            }
            return string.Empty;
        }
    }
}
