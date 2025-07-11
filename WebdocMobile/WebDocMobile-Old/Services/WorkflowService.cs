using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebDocMobile.Models.WorkflowService;

namespace WebDocMobile.Services
{
    public interface IWorkflowService
    {
        Task ForwardWkf(string hashCode, string documentIDEncrypted, int workflowStateToID, int teamToID, int userToID, string remarks);
        Task ProcFowardWkf(string hashCode, string processIDEncrypted, int workflowStateToID, int teamToID, int userToID, string remarks);
        Task ForwardWkfFromIDUser(string hashCode, string documentIDEncrypted, int workflowStateToID, int teamToID, int userToID, string remarks, int userFromID);
        Task ForwardWkfByDocRef(string hashCode, string docNumber, string docRef);
    }

    public class WorkflowService : IWorkflowService
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public WorkflowService()
        {
#if DEBUG
            HttpClientHandler insecureHandler = GetInsecureHandler();
            _httpClient = new HttpClient(insecureHandler);
#else
            _httpClient = new HttpClient();
#endif
            if(App.codigoEntidade == "1994")
            {
                _url = $"{App.baseAddress}/api/v1";
            }
            else if(App.codigoEntidade == "1995")
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

        public async Task ForwardWkf(string hashCode, string documentIDEncrypted, int workflowStateToID, int teamToID, int userToID, string remarks)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No Internet Connection");
                return;
            }

            try
            {
                ForwardWkfDto dto = new ForwardWkfDto()
                {
                    strHashCode = hashCode,
                    strDocumentIDEncrypted = documentIDEncrypted,
                    intIDWorkflowStateTo = workflowStateToID,
                    intIDTeamTo = teamToID,
                    intIDUserTo = userToID,
                    strRemarks = remarks
                };
                StringContent content = new StringContent(JsonSerializer.Serialize(dto, _jsonSerializerOptions));
                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/Workflow/forwardWkf", content);

                if(response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Document's Workflow has been changed");
                }
                else
                {
                    Debug.WriteLine("Error during the change of document's workflow");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public async Task ForwardWkfByDocRef(string hashCode, string docNumber, string docRef)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No Internet Connection");
                return;
            }

            try
            {
                ForwardWKFByDocRefDto dto = new ForwardWKFByDocRefDto()
                {
                    strHashCode = hashCode,
                    strDocNumber = docNumber,
                    strDocRef = docRef
                };
                StringContent content = new StringContent(JsonSerializer.Serialize(dto, _jsonSerializerOptions));
                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/Workflow/forwardWkf", content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Document's Workflow has been changed");
                }
                else
                {
                    Debug.WriteLine("Error during the change of document's workflow");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public async Task ForwardWkfFromIDUser(string hashCode, string documentIDEncrypted, int workflowStateToID, int teamToID, int userToID, string remarks, int userFromID)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No Internet Connection");
                return;
            }

            try
            {
                ForwardWkfFromIDUserDto dto = new ForwardWkfFromIDUserDto()
                {
                    strHashCode = hashCode,
                    strDocumentIDEncrypted = documentIDEncrypted,
                    intIDWorkflowStateTo = workflowStateToID,
                    intIDTeamTo = teamToID,
                    intIDUserTo = userToID,
                    strRemarks = remarks,
                    intIDUserFrom = userFromID
                };
                StringContent content = new StringContent(JsonSerializer.Serialize(dto, _jsonSerializerOptions));
                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/Workflow/forwardWkf", content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Document's Workflow has been changed");
                }
                else
                {
                    Debug.WriteLine("Error during the change of document's workflow");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public async Task ProcFowardWkf(string hashCode, string processIDEncrypted, int workflowStateToID, int teamToID, int userToID, string remarks)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No Internet Connection");
                return;
            }

            try
            {
                ProcForwardWkfDto dto = new ProcForwardWkfDto()
                {
                    strHashCode = hashCode,
                    strProcessIDEncrypted = processIDEncrypted,
                    intIDWorkflowStateTo = workflowStateToID,
                    intIDTeamTo = teamToID,
                    intIDUserTo = userToID,
                    strRemarks = remarks,
                };
                StringContent content = new StringContent(JsonSerializer.Serialize(dto, _jsonSerializerOptions));
                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/Workflow/forwardWkf", content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Document's Workflow has been changed");
                }
                else
                {
                    Debug.WriteLine("Error during the change of document's workflow");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
