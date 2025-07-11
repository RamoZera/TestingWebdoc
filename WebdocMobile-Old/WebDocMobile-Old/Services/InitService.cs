using Newtonsoft.Json;
using System.Diagnostics;
using WebDocMobile.Models.InitService;
using WebDocMobile.Services;

namespace WebDocMobile.Services
{
    public interface IInitService
    {
        Task<InitDto> Init();
        Task<string> GetWsSoapAddress();
        Task<string> GetWsHttpAddress();
    }

    public class InitService : IInitService
    {
        private readonly ISettingsService _settingsService;
        private readonly HttpClient _httpClient;

        public InitService(ISettingsService settingsService)
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

        public async Task<InitDto> Init()
        {
            try
            {
                var baseAddress = _settingsService.BaseAddress;
                if (string.IsNullOrEmpty(baseAddress))
                {
                    Debug.WriteLine("Error: BaseAddress is not set in SettingsService.");
                    return null;
                }

                _httpClient.BaseAddress = new Uri(baseAddress);
                var response = await _httpClient.GetAsync("/Init/Init");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<InitDto>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in InitService.Init: {ex.Message}");
            }
            return null;
        }

        public async Task<string> GetWsSoapAddress()
        {
            var baseAddress = _settingsService.BaseAddress;
            if (string.IsNullOrEmpty(baseAddress)) return string.Empty;

            _httpClient.BaseAddress = new Uri(baseAddress);
            var response = await _httpClient.GetAsync("/Init/GetWsSoapAddress");
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetWsHttpAddress()
        {
            var baseAddress = _settingsService.BaseAddress;
            if (string.IsNullOrEmpty(baseAddress)) return string.Empty;

            _httpClient.BaseAddress = new Uri(baseAddress);
            var response = await _httpClient.GetAsync("/Init/GetWsHttpAddress");
            return await response.Content.ReadAsStringAsync();
        }
    }
}
