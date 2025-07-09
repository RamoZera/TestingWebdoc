﻿using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebDocMobile.Helpers.WsMethods;
using WebDocMobile.Models.ProcessService;

namespace WebDocMobile.Services
{
    public class ProcessService : IProcessService
    {
        private readonly HttpClient _httpClient;
        private readonly ISettingsService _settingsService;

        public ProcessService(ISettingsService settingsService)
        {
            _httpClient = new HttpClient();
            _settingsService = settingsService;
        }

        public async Task<GDProcess> GetProcessDataResumedByNumber(string strHashCode, string strProcessNumber)
        {
            var url = $"{_settingsService.BaseAddress}/Process/GetProcessDataResumedByNumber";
            var payload = new GetProcessDataResumedByNumberDto
            {
                strHashCode = strHashCode,
                strProcessNumber = strProcessNumber
            };
            var jsonPayload = JsonConvert.SerializeObject(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GDProcess>(jsonResponse);
        }
    }
}