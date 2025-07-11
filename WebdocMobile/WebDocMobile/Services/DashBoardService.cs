//-----------------------------------------------------------------------
// <copyright file="DashNoardService.cs" company="Home">
//     Author: Horus Ra
//     Copyright (c) Home. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Networking;
using Microsoft.Maui.Storage;
using WebDocMobile.Helpers;
using WebDocMobile.Models;
using WebDocMobile.Models.DashBoard;

namespace WebDocMobile.Services
{
    public interface IGetDashBoardService
    {
        GenericResponse<GetDashBoard> GetDashBoardData(out bool navigateToLogin);
        GenericResponse<List<Document>> GetDocuments(int? count, out bool navigateToLogin);
        GenericResponse<List<Processes>> GetProcesses(int? count, out bool navigateToLogin);
    }

    public class DashBoardService : IGetDashBoardService
    {
        private readonly HttpClient _client;

        public DashBoardService()
        {
            _client = new HttpClient();
        }

        public HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if(cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == SslPolicyErrors.None;
            };
            return handler;
        }

        public GenericResponse<GetDashBoard> GetDashBoardData( out bool navigateToLogin)
        {
            APIHelper.CheckForInternetConnection();
            _client._StandarSetup(App.UserDetails.Token);
            return _client._CallAPI<GetDashBoard>($"{App.BaseAddress}init/get_mobile_dashboard_data", null, out navigateToLogin);
        }
       
        public GenericResponse<List<Document>> GetDocuments(int? count, out bool navigateToLogin)
        {
            APIHelper.CheckForInternetConnection();
            _client._StandarSetup(App.UserDetails.Token);
            return _client._CallAPI<List<Document>>($"{App.BaseAddress}document/get_documents", new StringContent(JsonSerializer.Serialize(new { Count = count }, JSONSupport.jsonSerializerOptions_Write), Encoding.UTF8, APIHelper.jsonMime), out navigateToLogin);
        }
        public GenericResponse<List<Processes>> GetProcesses(int? count, out bool navigateToLogin)
        {
            APIHelper.CheckForInternetConnection();
            _client._StandarSetup(App.UserDetails.Token);
            return _client._CallAPI<List<Processes>>($"{App.BaseAddress}process/get_processes", new StringContent(JsonSerializer.Serialize(new { Count = count }, JSONSupport.jsonSerializerOptions_Write), Encoding.UTF8, APIHelper.jsonMime), out navigateToLogin);
        }
    }
}
