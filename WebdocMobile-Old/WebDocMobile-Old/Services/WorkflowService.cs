using Newtonsoft.Json;
using System.Diagnostics;

namespace WebDocMobile.Services
{
    public interface IWorkflowService
    {
        Task<string> GetWsSoapAddress();
    }

    public class WorkflowService : IWorkflowService
    {
        private readonly ISettingsService _settingsService;
        private readonly HttpClient _httpClient;

        public WorkflowService(ISettingsService settingsService)
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
                var response = await _httpClient.GetAsync("/Workflow/GetWsSoapAddress");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in WorkflowService.GetWsSoapAddress: {ex.Message}");
            }
            return string.Empty;
        }
    }
}
