using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebDocMobile.Models.WorkflowService;

namespace WebDocMobile.Services
{
    public class WorkflowService : IWorkflowService
    {
        private readonly HttpClient _httpClient;
        private readonly ISettingsService _settingsService;

        public WorkflowService(ISettingsService settingsService)
        {
            _httpClient = new HttpClient();
            _settingsService = settingsService;
        }

        public async Task<bool> ForwardWkf(string strHashCode, string strDocumentIDEncrypted, int intIDWorkflowStateTo, int intIDTeamTo, int intIDUserTo, string strRemarks)
        {
            var url = $"{_settingsService.BaseAddress}/Workflow/ForwardWkf";
            var payload = new ForwardWkfDto
            {
                strHashCode = strHashCode,
                strDocumentIDEncrypted = strDocumentIDEncrypted,
                intIDWorkflowStateTo = intIDWorkflowStateTo,
                intIDTeamTo = intIDTeamTo,
                intIDUserTo = intIDUserTo,
                strRemarks = strRemarks
            };
            var jsonPayload = JsonConvert.SerializeObject(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);

            return response.IsSuccessStatusCode;
        }

        // Add other method implementations from your full service here if they exist
    }
}