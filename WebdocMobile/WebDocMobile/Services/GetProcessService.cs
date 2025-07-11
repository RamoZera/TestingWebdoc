using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebDocMobile.Helpers;
using WebDocMobile.Models;
using WebDocMobile.Models.NewDocument;
using WebDocMobile.Models.NewProcesses;
using WebDocMobile.Models.WorkflowService;

namespace WebDocMobile.Services
{
    public interface IGetProcessService
    {

        GenericResponse<DocumentResponse> InsertTest(NewProcessRequest documentRequest, out bool navigateToLogin, out GenericResponse<DocumentResponse> result);

        GenericResponse<ProcessMetadataResponse> GetProcessMetadata(DocumentMetadataRequest request, out bool _navigateToLogin);
        GenericResponse<AddRelatedResponse> AddRelatedProcess(AddRelatedDocumentRequest request, out bool navigateToLogin);
        GenericResponse<List<DocumentSelectedResponse>> GetProcessSelected(out bool navigateToLogin,string search);
        GenericResponse<List<Movement>> GetMovementProcess(int pProcessesId, out bool navigateToLogin);
        GenericResponse<List<RelatedDocumentResponse>> GetRelatedProcess(int pProcessID, out bool navigateToLogin);
        GenericResponse<AddRelatedResponse> AddRelatedWithProcess(AddRelatedDocumentRequest request, out bool navigateToLogin);
        GenericResponse<AddRelatedResponse> DeleteRelatedProcess(AddRelatedDocumentRequest request, out bool navigateToLogin);

        GenericResponse<GetFileDTO> GetProcessFile(int id, out bool navigateToLogin);
        GenericResponse<WorflowkNavigationResponse> GetProcessWorkflowNavigation(int pDocumentID, out bool navigateToLogin);
        GenericResponse<AddRelatedResponse> AddProcessWorkflowNavigation(WorkflowRequest request, out bool navigateToLogin);
        GenericResponse<List<WorkflowResponse>> GetProcessWorkFlowHistory(int pDocumentId, out bool navigateToLogin);
        GenericResponse<List<Knowledge>> GetKnowledgeProcess(int pDocumentID, out bool navigateToLogin);
        GenericResponse<AddRelatedResponse> AddknowledgeProcess(KnowledgeDocumentRequest request, out bool navigateToLogin);
        GenericResponse<AddRelatedResponse> DeleteKnowledgeProcess(KnowledgeDocumentRequest request, out bool navigateToLogin);


    }
    public partial class GetProcessService : IGetProcessService
    {
        private readonly HttpClient _client;
        public string running;
        private string _token;

        public static string BaseAddress =
    DeviceInfo.Platform == DevicePlatform.Android ? "https://wsdemo.ambisig.com/api/" : "https://wsdemo.ambisig.com/api/";


        public GetProcessService()
        {
            _client = new HttpClient();
        }

        public HttpMessageHandler GetInsecureHttpsHandler()
        {
#if ANDROID
            var handler = new Xamarin.Android.Net.AndroidMessageHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert != null && cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
#elif IOS
            var handler = new NSUrlSessionHandler
            {
                TrustOverrideForUrl = IsHttpsLocalhost
            };
            return handler;
#else
            throw new PlatformNotSupportedException("Only Android and iOS supported.");
#endif
        }
#if IOS
        public bool IsHttpsLocalhost(NSUrlSessionHandler sender, string url, Security.SecTrust trust)
        {
            return url.StartsWith("https://localhost");
        }
#endif
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


        private GenericResponse<DocumentResponse> Insert(NewProcessRequest processRequest, out bool navigateToLogin)
        {
            APIHelper.CheckForInternetConnection();
            _client._StandarSetup(App.UserDetails.Token);
            return _client._CallAPI<DocumentResponse>($"{App.BaseAddress}process/insert_process", new StringContent(JsonSerializer.Serialize(new NewProcessRequest()
            {
                subject = processRequest.subject,
                date = processRequest.date,
                typeId = processRequest.typeId,
                reference = processRequest.reference,
                classifierId = processRequest.classifierId,
                entityId = processRequest.entityId,
                observations = processRequest.observations,
                physicalFile = processRequest.physicalFile,
                documentFile = processRequest.documentFile,
                urgent = processRequest.urgent,
                responseDate = processRequest.responseDate,
                waitsResponse = processRequest.waitsResponse,
                documentExtension = processRequest.documentExtension,
                convertToPDF = processRequest.convertToPDF
            }), System.Text.Encoding.UTF8, APIHelper.jsonMime), out navigateToLogin); ;

        }

        public GenericResponse<DocumentResponse> InsertTest(NewProcessRequest processRequest, out bool navigateToLogin, out GenericResponse<DocumentResponse> result)
        {
            result = null;
            APIHelper.CheckForInternetConnection();
            _client._StandarSetup(App.UserDetails.Token);
            GenericResponse<DocumentResponse> wsres = Insert(processRequest, out navigateToLogin);
            if (wsres != null)
            {
                result = wsres;
                return wsres;
            }
            result = APIHelper.GetInvalidResponse<DocumentResponse>();
            return null;
        }
        public GenericResponse<ProcessMetadataResponse> GetProcessMetadata(DocumentMetadataRequest request, out bool _navigateToLogin)
        {
            APIHelper.CheckForInternetConnection();
            _client._StandarSetup(App.UserDetails.Token);
            return _client._CallAPI<ProcessMetadataResponse>($"{App.BaseAddress}process/get_process_metadata", new StringContent(JsonSerializer.Serialize(new DocumentMetadataRequest()
            {
                id = request.id,
                sizeRequest = request.sizeRequest


            }), System.Text.Encoding.UTF8, APIHelper.jsonMime), out _navigateToLogin);

        }

        public GenericResponse<AddRelatedResponse> AddRelatedProcess(AddRelatedDocumentRequest request, out bool navigateToLogin)
        {

            APIHelper.CheckForInternetConnection();
            _client._StandarSetup(App.UserDetails.Token);

            return _client._CallAPI<AddRelatedResponse>($"{App.BaseAddress}document/add_related_process", new StringContent(JsonSerializer.Serialize(new AddRelatedDocumentRequest()
            {
                id = request.id,
                targetIds = request.targetIds
            }), System.Text.Encoding.UTF8, APIHelper.jsonMime), out navigateToLogin);

        }

        public GenericResponse<List<DocumentSelectedResponse>> GetProcessSelected(out bool navigateToLogin,string search)
        {

            APIHelper.CheckForInternetConnection();
            _client._StandarSetup(App.UserDetails.Token);

            return _client._CallAPI<List<DocumentSelectedResponse>>($"{App.BaseAddress}process/get_processes_select", new StringContent(JsonSerializer.Serialize(new DocumentSelectedRequest
            {
                search = search
            }), System.Text.Encoding.UTF8, APIHelper.jsonMime), out navigateToLogin);
        }
        public GenericResponse<List<Movement>> GetMovementProcess(int pProcessesId, out bool navigateToLogin)
        {

            APIHelper.CheckForInternetConnection();
            _client._StandarSetup(App.UserDetails.Token);

            return _client._CallAPI<List<Movement>>($"{App.BaseAddress}process/get_process_movements", new StringContent(JsonSerializer.Serialize(new RelatedDocumentRequest()
            {
                id = pProcessesId
            }), System.Text.Encoding.UTF8, APIHelper.jsonMime), out navigateToLogin);

        }

        public GenericResponse<List<RelatedDocumentResponse>> GetRelatedProcess(int pProcessID, out bool navigateToLogin)
        {

            APIHelper.CheckForInternetConnection();
            _client._StandarSetup(App.UserDetails.Token);

            return _client._CallAPI<List<RelatedDocumentResponse>>($"{App.BaseAddress}process/get_related_processes", new StringContent(JsonSerializer.Serialize(new RelatedDocumentRequest()
            {
                id = pProcessID
            }), System.Text.Encoding.UTF8, APIHelper.jsonMime), out navigateToLogin);

        }

        public GenericResponse<AddRelatedResponse> AddRelatedWithProcess(AddRelatedDocumentRequest request, out bool navigateToLogin)
        {

            APIHelper.CheckForInternetConnection();
            _client._StandarSetup(App.UserDetails.Token);

            return _client._CallAPI<AddRelatedResponse>($"{App.BaseAddress}process/add_related_process", new StringContent(JsonSerializer.Serialize(new AddRelatedDocumentRequest()
            {
                id = request.id,
                targetIds = request.targetIds
            }), System.Text.Encoding.UTF8, APIHelper.jsonMime), out navigateToLogin);

        }

        public GenericResponse<AddRelatedResponse> DeleteRelatedProcess(AddRelatedDocumentRequest request, out bool navigateToLogin)
        {

            APIHelper.CheckForInternetConnection();
            _client._StandarSetup(App.UserDetails.Token);

            return _client._CallAPI<AddRelatedResponse>($"{App.BaseAddress}process/delete_related_process", new StringContent(JsonSerializer.Serialize(new AddRelatedDocumentRequest()
            {
                id = request.id,
                targetIds = request.targetIds
            }), System.Text.Encoding.UTF8, APIHelper.jsonMime), out navigateToLogin);

        }

        public GenericResponse<GetFileDTO> GetProcessFile(int id, out bool navigateToLogin)
        {

            APIHelper.CheckForInternetConnection();
            _client._StandarSetup(App.UserDetails.Token);

            return _client._CallAPI<GetFileDTO>($"{App.BaseAddress}process/get_process_file", new StringContent(JsonSerializer.Serialize(new RelatedDocumentRequest()
            {
                id = id,

            }), System.Text.Encoding.UTF8, APIHelper.jsonMime), out navigateToLogin);

        }

       #region processes workflow
        public GenericResponse<WorflowkNavigationResponse> GetProcessWorkflowNavigation(int pDocumentID, out bool navigateToLogin)
        {

            APIHelper.CheckForInternetConnection();
            _client._StandarSetup(App.UserDetails.Token);

            return _client._CallAPI<WorflowkNavigationResponse>($"{App.BaseAddress}process/get_workflow_navigation", new StringContent(JsonSerializer.Serialize(new RelatedDocumentRequest()
            {
                id = pDocumentID
            }), System.Text.Encoding.UTF8, APIHelper.jsonMime), out navigateToLogin);

        }

        public GenericResponse<AddRelatedResponse> AddProcessWorkflowNavigation(WorkflowRequest request, out bool navigateToLogin)
        {

            APIHelper.CheckForInternetConnection();
            _client._StandarSetup(App.UserDetails.Token);

            return _client._CallAPI<AddRelatedResponse>($"{App.BaseAddress}process/add_workflow_navigation", new StringContent(JsonSerializer.Serialize(new WorkflowRequest()
            {
                Id = request.Id,
                Comments = request.Comments,
                WorkflowStateId = request.WorkflowStateId
            }), System.Text.Encoding.UTF8, APIHelper.jsonMime), out navigateToLogin);

        }
        public GenericResponse<List<WorkflowResponse>> GetProcessWorkFlowHistory(int pDocumentId, out bool navigateToLogin)
        {

            APIHelper.CheckForInternetConnection();
            _client._StandarSetup(App.UserDetails.Token);

            return _client._CallAPI<List<WorkflowResponse>>($"{App.BaseAddress}process/get_workflow_history", new StringContent(JsonSerializer.Serialize(new RelatedDocumentRequest()
            {
                id = pDocumentId
            }), System.Text.Encoding.UTF8, APIHelper.jsonMime), out navigateToLogin);

        }

        #endregion

    }
}
