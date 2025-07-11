using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebDocMobile.Helpers;
using WebDocMobile.Models;
using WebDocMobile.Models.NewDocument;

namespace WebDocMobile.Services
{
   public partial class GetProcessService
    {
        public GenericResponse<List<Knowledge>> GetKnowledgeProcess(int pDocumentID, out bool navigateToLogin)
        {

            APIHelper.CheckForInternetConnection();
            _client._StandarSetup(App.UserDetails.Token);

            return _client._CallAPI<List<Knowledge>>($"{App.BaseAddress}process/get_process_knowledge", new StringContent(JsonSerializer.Serialize(new RelatedDocumentRequest()
            {
                id = pDocumentID

            }), System.Text.Encoding.UTF8, APIHelper.jsonMime), out navigateToLogin);

        }


        public GenericResponse<AddRelatedResponse> AddknowledgeProcess(KnowledgeDocumentRequest request, out bool navigateToLogin)
        {

            APIHelper.CheckForInternetConnection();
            _client._StandarSetup(App.UserDetails.Token);

            return _client._CallAPI<AddRelatedResponse>($"{App.BaseAddress}process/add_process_knowledge", new StringContent(JsonSerializer.Serialize(new KnowledgeDocumentRequest()
            {
                id = request.id,
                comments = request.comments,
                observations = request.observations,
                usersIds = request.usersIds,
                groupsIds = request.groupsIds

            }), System.Text.Encoding.UTF8, APIHelper.jsonMime), out navigateToLogin);

        }

        public GenericResponse<AddRelatedResponse> DeleteKnowledgeProcess(KnowledgeDocumentRequest request, out bool navigateToLogin)
        {

            APIHelper.CheckForInternetConnection();
            _client._StandarSetup(App.UserDetails.Token);

            return _client._CallAPI<AddRelatedResponse>($"{App.BaseAddress}process/delete_process_knowledge", new StringContent(JsonSerializer.Serialize(new KnowledgeDocumentRequest()
            {
                id = request.id,
                comments = request.comments,
                observations = request.observations,
                usersIds = request.usersIds,
                groupsIds = request.groupsIds

            }), System.Text.Encoding.UTF8, APIHelper.jsonMime), out navigateToLogin);

        }
    }
}
