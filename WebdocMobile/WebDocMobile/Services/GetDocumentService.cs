using Microsoft.Maui.Networking;
using Microsoft.Maui.Storage;
using System.Text;
using System.Text.Json;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.Intrinsics.Arm;
using System.Security.AccessControl;
using Telerik.Maui.Controls.Compatibility.Barcode;
using WebDocMobile.Helpers;
using WebDocMobile.Models;
using WebDocMobile.Models.BookService;
using WebDocMobile.Models.NewDocument;
using WebDocMobile.Models.DashBoard;
using WebDocMobile.Models.WorkflowService;

namespace WebDocMobile.Services
{
    public interface IGetDocumentService
    {
        GenericResponse<LoadDefaultDataDto> GetDocumentData(out bool _navigateToLogin);
        GenericResponse<List<IdName<int, string>>> GetEntity(string search, out bool _navigateToLogin);
        DocumentResponse InsertTest(NewDocumentRequest documentRequest, out bool navigateToLogin, out GenericResponse<DocumentResponse> result);

        GenericResponse<List<RelatedDocumentResponse>> GetRelatedDocument(int id, out bool navigateToLogin);
        GenericResponse<List<Document>> GetAttachedDocument(int id, out bool navigateToLogin);
        GenericResponse<List<Movement>> GetMovementDocument(int id, out bool navigateToLogin);
        GenericResponse<DocumentMetadataResponse> GetDocumentMetadata(DocumentMetadataRequest request, out bool _navigateToLogin);
        GenericResponse<AddRelatedResponse> AddRelatedDocument(AddRelatedDocumentRequest request, out bool navigateToLogin);
        GenericResponse<AddRelatedResponse> DeleteRelatedDocument(AddRelatedDocumentRequest request, out bool navigateToLogin);

        GenericResponse<List<DocumentSelectedResponse>> GetDocumentSelected(out bool navigateToLogin,string search);
        GenericResponse<List<Knowledge>> GetKnowledgeDocument(int pDocumentID, out bool navigateToLogin);
        GenericResponse<List<WorkflowResponse>> GetWorkFlowHistory(int pDocumentId, out bool navigateToLogin);
        GenericResponse<AddRelatedResponse> AddWorkflowNavigation(WorkflowRequest request, out bool navigateToLogin);
        GenericResponse<WorflowkNavigationResponse> GetWorkflowNavigation(int pDocumentID, out bool navigateToLogin);
        GenericResponse<List<RelatedDocumentResponse>> GetRelatedProcessDocument(int id, out bool navigateToLogin);
        GenericResponse<GetFileDTO> GetDocumentFile(int id, out bool navigateToLogin);
        GenericResponse<AddRelatedResponse> AddknowledgeDocument(KnowledgeDocumentRequest request, out bool navigateToLogin);
        GenericResponse<AddRelatedResponse> DeleteKnowledgeDocument(KnowledgeDocumentRequest request, out bool navigateToLogin);

        GenericResponse<bool> UpdateDocumentCheckOut(DocumentMetadataRequest request, bool CheckOut , out bool _navigateToLogin);

    }
    public partial class GetDocumentService : IGetDocumentService
    {
        private readonly HttpClient _client;
        public string running;




        public GetDocumentService()
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

        public GenericResponse<LoadDefaultDataDto> GetDocumentData(out bool navigateToLogin)
        {

            APIHelper.CheckForInternetConnection();
            _client._StandarSetup(App.UserDetails.Token);

            return _client._CallAPI<LoadDefaultDataDto>($"{App.BaseAddress}init/load_default_data", null
            , out navigateToLogin);
        }

        public GenericResponse<List<IdName<int, string>>> GetEntity(string search, out bool navigateToLogin)
        {

            APIHelper.CheckForInternetConnection();
            _client._StandarSetup(App.UserDetails.Token);

            return _client._CallAPI<List<IdName<int, string>>>($"{App.BaseAddress}entity/get_entities_select", new StringContent(JsonSerializer.Serialize(new EntityRequest()
            {
                search = search
            }), System.Text.Encoding.UTF8, APIHelper.jsonMime), out navigateToLogin);

        }

        public GenericResponse<List<RelatedDocumentResponse>> GetRelatedDocument(int pDocumentID, out bool navigateToLogin)
        {

            APIHelper.CheckForInternetConnection();
            _client._StandarSetup(App.UserDetails.Token);

            return _client._CallAPI<List<RelatedDocumentResponse>>($"{App.BaseAddress}document/get_related_documents", new StringContent(JsonSerializer.Serialize(new RelatedDocumentRequest()
            {
                id = pDocumentID
            }), System.Text.Encoding.UTF8, APIHelper.jsonMime), out navigateToLogin);

        }

        public GenericResponse<List<Document>> GetAttachedDocument(int pDocumentId, out bool navigateToLogin)
        {

            APIHelper.CheckForInternetConnection();
            _client._StandarSetup(App.UserDetails.Token);

            return _client._CallAPI<List<Document>>($"{App.BaseAddress}entity/attached_documents", new StringContent(JsonSerializer.Serialize(new RelatedDocumentRequest()
            {
                id = pDocumentId
            }), System.Text.Encoding.UTF8, APIHelper.jsonMime), out navigateToLogin);

        }

        public GenericResponse<List<Movement>> GetMovementDocument(int pDocumentId, out bool navigateToLogin)
        {

            APIHelper.CheckForInternetConnection();
            _client._StandarSetup(App.UserDetails.Token);

            return _client._CallAPI<List<Movement>>($"{App.BaseAddress}document/get_document_movements", new StringContent(JsonSerializer.Serialize(new RelatedDocumentRequest()
            {
                id = pDocumentId
            }), System.Text.Encoding.UTF8, APIHelper.jsonMime), out navigateToLogin);

        }


        private GenericResponse<DocumentResponse> Insert(NewDocumentRequest documentRequest, out bool navigateToLogin)
        {
            APIHelper.CheckForInternetConnection();
            _client._StandarSetup(App.UserDetails.Token);
            return _client._CallAPI<DocumentResponse>($"{App.BaseAddress}document/insert_document", new StringContent(JsonSerializer.Serialize(new NewDocumentRequest()
            {
                subject = documentRequest.subject,
                bookId = documentRequest.bookId,
                TypeId = documentRequest.TypeId,
                sendReceiveId = documentRequest.sendReceiveId,
                carimboDate = documentRequest.carimboDate,
                reference = documentRequest.reference,
                classifierId = documentRequest.classifierId,
                Date = documentRequest.Date,
                entityId = documentRequest.entityId,
                observations = documentRequest.observations,
                physicalFile = documentRequest.physicalFile,
                documentFile = documentRequest.documentFile,
                urgent = documentRequest.urgent,
                responseDate = documentRequest.responseDate,
                waitsResponse = documentRequest.waitsResponse,
                documentExtension = documentRequest.documentExtension,
                Confidential = documentRequest.Confidential,
                DocumentNoResponse = documentRequest.DocumentNoResponse,
                InTreatment = documentRequest.InTreatment,
                convertToPDF = documentRequest.convertToPDF
            }), System.Text.Encoding.UTF8, APIHelper.jsonMime), out navigateToLogin);

        }

        public DocumentResponse InsertTest(NewDocumentRequest documentRequest, out bool navigateToLogin, out GenericResponse<DocumentResponse> result)
        {
            result = null;
            APIHelper.CheckForInternetConnection();
            _client._StandarSetup(App.UserDetails.Token);
            GenericResponse<DocumentResponse> wsres = Insert(documentRequest, out navigateToLogin);
            if (wsres != null)
            {
                result = wsres;
                return wsres.Result;
            }
            result = APIHelper.GetInvalidResponse<DocumentResponse>();
            return null;
        }

        public GenericResponse<DocumentMetadataResponse> GetDocumentMetadata(DocumentMetadataRequest request, out bool _navigateToLogin)
        {
            APIHelper.CheckForInternetConnection();
            _client._StandarSetup(App.UserDetails.Token);
            return _client._CallAPI<DocumentMetadataResponse>($"{App.BaseAddress}document/get_document_metadata", new StringContent(JsonSerializer.Serialize(new DocumentMetadataRequest()
            {
                id = request.id,
                sizeRequest = request.sizeRequest


            }), System.Text.Encoding.UTF8, APIHelper.jsonMime), out _navigateToLogin);

        }

        public GenericResponse<bool> UpdateDocumentCheckOut(DocumentMetadataRequest request, bool checkOut, out bool _navigateToLogin)
        {
            APIHelper.CheckForInternetConnection();
            _client._StandarSetup(App.UserDetails.Token);

            return _client._CallAPI<bool>($"{App.BaseAddress}document/update_document_checkout", new StringContent(JsonSerializer.Serialize(new DocumentUpdateCheckOut()
            {
                id = request.id,
                CheckOut = checkOut

            }), System.Text.Encoding.UTF8, APIHelper.jsonMime), out _navigateToLogin);
        }

    }
}
