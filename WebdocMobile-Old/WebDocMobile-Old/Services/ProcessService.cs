using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebDocMobile.Helpers.WsMethods;
using WebDocMobile.Models.ProcessService;

namespace WebDocMobile.Services
{
    public interface IProcessService
    {
        Task InsertGDProcess(string hashCode, GDProcess process, string fileBuf, string strExt);
        Task InsertGDProcessGetGUID(string hashCode, GDProcess process, string fileBuf, string strExt);
        Task<GDProcess> GetProcessDataResumedByNumber(string hashCode, string processNumber);
    }

    public class ProcessService : IProcessService
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public ProcessService()
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

        public async Task<GDProcess> GetProcessDataResumedByNumber(string hashCode, string processNumber)
        {
            GDProcess p = new GDProcess();
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No Internet Connection");
                return p;
            }

            try
            {
                GetProcessDataResumedByNumberDto dto = new GetProcessDataResumedByNumberDto()
                {
                    strHashCode = hashCode,
                    strProcessNumber = processNumber
                };

                StringContent contentToSend = new StringContent(JsonSerializer.Serialize(dto, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/Process/getProcessDataResumedByNumber", contentToSend);

                if (response.IsSuccessStatusCode)
                {
                    string contentToReceive = await response.Content.ReadAsStringAsync();

                    p = JsonSerializer.Deserialize<GDProcess>(contentToReceive, _jsonSerializerOptions);
                }
                else
                {
                    Debug.WriteLine("No Http Response");
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            return p;
        }

        public async Task InsertGDProcess(string hashCode, GDProcess process, string fileBuf, string strExt)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No Internet Connection");
                return;
            }

            try
            {
                InsertGDProcessDto dto = new InsertGDProcessDto()
                {
                    strHashCode = hashCode,
                    wsProcess = process,
                    fileBuf = fileBuf,
                    strExt = strExt
                };

                StringContent content = new StringContent(JsonSerializer.Serialize(dto, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/Process/InsertGDProcess", content);

                if(response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Process was created");
                }
                else
                {
                    Debug.WriteLine("Error during process creation");
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public async Task InsertGDProcessGetGUID(string hashCode, GDProcess process, string fileBuf, string strExt)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No Internet Connection");
                return;
            }

            try
            {
                InsertGDProcessGetGUIDDto dto = new InsertGDProcessGetGUIDDto()
                {
                    strHashCode = hashCode,
                    wsProcess = process,
                    fileBuf = fileBuf,
                    strExt = strExt
                };

                StringContent content = new StringContent(JsonSerializer.Serialize(dto, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/Process/InsertGDProcessGetGUID", content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Process with GUID = " + response.Content.ToString() + " was inserted");
                }
                else
                {
                    Debug.WriteLine("Error during process creation");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
