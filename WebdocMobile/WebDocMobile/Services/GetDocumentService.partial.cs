using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDocMobile.Models;
using WebDocMobile.Models.DashBoard;
using WebDocMobile.Helpers;
using System.Text;
using System.Text.Json;
using WebDocMobile.Models.NewDocument;
using WebDocMobile.Models.WorkflowService;

namespace WebDocMobile.Services
{
    public partial class GetDocumentService
    {
        public GenericResponse<AddRelatedResponse> AddRelatedDocument(AddRelatedDocumentRequest request, out bool navigateToLogin)
        {

            APIHelper.CheckForInternetConnection();
            _client._StandarSetup(App.UserDetails.Token);

            return _client._CallAPI<AddRelatedResponse>($"{App.BaseAddress}document/add_related_document", new StringContent(JsonSerializer.Serialize(new AddRelatedDocumentRequest()
            {
                id = request.id,
                targetIds = request.targetIds
            }), System.Text.Encoding.UTF8, APIHelper.jsonMime), out navigateToLogin);

        }

        public GenericResponse<AddRelatedResponse> DeleteRelatedDocument(AddRelatedDocumentRequest request, out bool navigateToLogin)
        {

            APIHelper.CheckForInternetConnection();
            _client._StandarSetup(App.UserDetails.Token);

            return _client._CallAPI<AddRelatedResponse>($"{App.BaseAddress}document/delete_related_document", new StringContent(JsonSerializer.Serialize(new AddRelatedDocumentRequest()
            {
                id = request.id,
                targetIds = request.targetIds
            }), System.Text.Encoding.UTF8, APIHelper.jsonMime), out navigateToLogin);

        }

        public GenericResponse<List<DocumentSelectedResponse>> GetDocumentSelected(out bool navigateToLogin, string search)
        {

            APIHelper.CheckForInternetConnection();
            _client._StandarSetup(App.UserDetails.Token);

            return _client._CallAPI<List<DocumentSelectedResponse>>($"{App.BaseAddress}document/get_documents_select", new StringContent(JsonSerializer.Serialize(new DocumentSelectedRequest
            {
                search = search
            }), System.Text.Encoding.UTF8, APIHelper.jsonMime), out navigateToLogin); 

        }



        public GenericResponse<List<Knowledge>> GetKnowledgeDocument(int pDocumentID, out bool navigateToLogin)
        {

            APIHelper.CheckForInternetConnection();
            _client._StandarSetup(App.UserDetails.Token);

            return _client._CallAPI<List<Knowledge>>($"{App.BaseAddress}document/get_document_knowledge", new StringContent(JsonSerializer.Serialize(new RelatedDocumentRequest()
            {
                id = pDocumentID

            }), System.Text.Encoding.UTF8, APIHelper.jsonMime), out navigateToLogin);

        }

        public GenericResponse<AddRelatedResponse> AddknowledgeDocument(KnowledgeDocumentRequest request, out bool navigateToLogin)
        {

            APIHelper.CheckForInternetConnection();
            _client._StandarSetup(App.UserDetails.Token);

            return _client._CallAPI<AddRelatedResponse>($"{App.BaseAddress}document/add_document_knowledge", new StringContent(JsonSerializer.Serialize(new KnowledgeDocumentRequest()
            {
                id = request.id,
                comments = request.comments,
                observations = request.observations,
                usersIds = request.usersIds,
                groupsIds = request.groupsIds

            }), System.Text.Encoding.UTF8, APIHelper.jsonMime), out navigateToLogin);

        }
        public GenericResponse<AddRelatedResponse> DeleteKnowledgeDocument(KnowledgeDocumentRequest request, out bool navigateToLogin)
        {

            APIHelper.CheckForInternetConnection();
            _client._StandarSetup(App.UserDetails.Token);

            return _client._CallAPI<AddRelatedResponse>($"{App.BaseAddress}document/delete_document_knowledge", new StringContent(JsonSerializer.Serialize(new KnowledgeDocumentRequest()
            {
                id = request.id,
                comments = request.comments,
                observations = request.observations,
                usersIds = request.usersIds,
                groupsIds = request.groupsIds

            }), System.Text.Encoding.UTF8, APIHelper.jsonMime), out navigateToLogin);

        }

        public GenericResponse<List<WorkflowResponse>> GetWorkFlowHistory(int pDocumentId, out bool navigateToLogin)
        {

            APIHelper.CheckForInternetConnection();
            _client._StandarSetup(App.UserDetails.Token);

            return _client._CallAPI<List<WorkflowResponse>>($"{App.BaseAddress}document/get_workflow_history", new StringContent(JsonSerializer.Serialize(new RelatedDocumentRequest()
            {
                id = pDocumentId
            }), System.Text.Encoding.UTF8, APIHelper.jsonMime), out navigateToLogin);

        }

        public GenericResponse<List<RelatedDocumentResponse>> GetRelatedProcessDocument(int pDocumentID, out bool navigateToLogin)
        {

            APIHelper.CheckForInternetConnection();
            _client._StandarSetup(App.UserDetails.Token);

            return _client._CallAPI<List<RelatedDocumentResponse>>($"{App.BaseAddress}document/get_related_processes", new StringContent(JsonSerializer.Serialize(new RelatedDocumentRequest()
            {
                id = pDocumentID
            }), System.Text.Encoding.UTF8, APIHelper.jsonMime), out navigateToLogin);

        }

        public GenericResponse<WorflowkNavigationResponse> GetWorkflowNavigation(int pDocumentID, out bool navigateToLogin)
        {

            APIHelper.CheckForInternetConnection();
            _client._StandarSetup(App.UserDetails.Token);

            return _client._CallAPI<WorflowkNavigationResponse>($"{App.BaseAddress}document/get_workflow_navigation", new StringContent(JsonSerializer.Serialize(new RelatedDocumentRequest()
            {
                id = pDocumentID
            }), System.Text.Encoding.UTF8, APIHelper.jsonMime), out navigateToLogin);

        }

        public GenericResponse<AddRelatedResponse> AddWorkflowNavigation(WorkflowRequest request, out bool navigateToLogin)
        {

            APIHelper.CheckForInternetConnection();
            _client._StandarSetup(App.UserDetails.Token);

            return _client._CallAPI<AddRelatedResponse>($"{App.BaseAddress}document/add_workflow_navigation", new StringContent(JsonSerializer.Serialize(new WorkflowRequest()
            {
                Id = request.Id,
                Comments = request.Comments,
                WorkflowStateId = request.WorkflowStateId
            }), System.Text.Encoding.UTF8, APIHelper.jsonMime), out navigateToLogin);

        }

        public GenericResponse<GetFileDTO> GetDocumentFile(int id, out bool navigateToLogin)
        {
            APIHelper.CheckForInternetConnection();
            _client._StandarSetup(App.UserDetails.Token);

            return _client._CallAPI<GetFileDTO>($"{App.BaseAddress}document/get_document_file", new StringContent(JsonSerializer.Serialize(new RelatedDocumentRequest()
            {
                id = id,

            }), System.Text.Encoding.UTF8, APIHelper.jsonMime), out navigateToLogin);

        }

    }
}
