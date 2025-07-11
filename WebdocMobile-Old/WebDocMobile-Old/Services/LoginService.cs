﻿using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using WebDocMobile.Models.LoginService;

namespace WebDocMobile.Services
{
    public interface ILoginService
    {
        Task<bool> LoginUserBasic(string strHashCode, string strUserName, string strPassword, string strDomainName);
        Task<string> GetWsSoapAddress();
        Task<string> GetWsHttpAddress();
    }

    public class LoginService : ILoginService
    {
        private readonly ISettingsService _settingsService;
        private readonly HttpClient _httpClient;

        public LoginService(ISettingsService settingsService)
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

        public async Task<bool> LoginUserBasic(string strHashCode, string strUserName, string strPassword, string strDomainName)
        {
            try
            {
                Debug.WriteLine($"[LoginService] ==> Attempting LoginUserBasic for user '{strUserName}' with hash '{strHashCode}'.");
                var baseAddress = _settingsService.BaseAddress;
                if (string.IsNullOrEmpty(baseAddress))
                {
                    Debug.WriteLine("[LoginService] ==> ERROR: BaseAddress is not set in SettingsService.");
                    return false;
                }
                Debug.WriteLine($"[LoginService] ==> Using BaseAddress: '{baseAddress}'");
                _httpClient.BaseAddress = new Uri(baseAddress);

                var loginRequest = new LoginUserBasicDto
                {
                    strHashCode = strHashCode,
                    strUserName = strUserName,
                    strPassword = strPassword,
                    strDomainName = strDomainName
                };

                var json = JsonConvert.SerializeObject(loginRequest);
                Debug.WriteLine($"[LoginService] ==> Sending POST to /Login/LoginUserBasic with JSON body: {json}");
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/Login/LoginUserBasic", content);
                Debug.WriteLine($"[LoginService] ==> Received response with status code: {response.StatusCode}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[LoginService] ==> EXCEPTION in LoginUserBasic: {ex.Message}");
                return false;
            }
        }

        public async Task<string> GetWsSoapAddress()
        {
            var baseAddress = _settingsService.BaseAddress;
            if (string.IsNullOrEmpty(baseAddress)) return string.Empty;

            _httpClient.BaseAddress = new Uri(baseAddress);
            var response = await _httpClient.GetAsync("/Login/GetWsSoapAddress");
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetWsHttpAddress()
        {
            var baseAddress = _settingsService.BaseAddress;
            if (string.IsNullOrEmpty(baseAddress)) return string.Empty;

            _httpClient.BaseAddress = new Uri(baseAddress);
            var response = await _httpClient.GetAsync("/Login/GetWsHttpAddress");
            return await response.Content.ReadAsStringAsync();
        }
    }
}
