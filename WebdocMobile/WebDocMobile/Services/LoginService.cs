﻿using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebDocMobile.Models.LoginService;

namespace WebDocMobile.Services
{
    public class LoginService : ILoginService
    {
        private readonly HttpClient _httpClient;
        private readonly ISettingsService _settingsService;

        public LoginService(ISettingsService settingsService)
        {
            _httpClient = new HttpClient();
            _settingsService = settingsService;
        }

        public async Task<bool> LoginUserBasic(string strHashCode, string strUserName, string strPassword, string strDomainName)
        {
            var url = $"{_settingsService.BaseAddress}/Login/LoginUserBasic";
            var payload = new LoginUserBasicDto
            {
                strHashCode = strHashCode,
                strUserName = strUserName,
                strPassword = strPassword,
                strDomainName = strDomainName
            };
            var jsonPayload = JsonConvert.SerializeObject(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            
            return response.IsSuccessStatusCode;
        }
    }
}