using System.Threading.Tasks;

namespace WebDocMobile.Services
{
    public interface IWorkflowService
    {
        Task<bool> ForwardWkf(string strHashCode, string strDocumentIDEncrypted, int intIDWorkflowStateTo, int intIDTeamTo, int intIDUserTo, string strRemarks);
        // Add other method signatures from your full service here if they exist
    }
}