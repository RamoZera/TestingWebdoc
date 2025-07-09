using System.Collections.Generic;
using System.Threading.Tasks;
using WebDocMobile.Helpers.WsMethods;

namespace WebDocMobile.Services
{
    public interface IDocumentService
    {
        Task<List<GDDocument>> ListMyDocuments(string strHashCode, int intPageRowSize, int intCurrentPage);
        Task<List<GDDocument>> ListAllMyDocuments(string strHashCode, int intPageRowSize, int intCurrentPage, string strSearch);
        Task<List<GDDocument>> ListDocuments(string strHashCode, int intPageRowSize, int intCurrentPage);
        Task<List<GDDocument>> ListALLDocument(string strHashCode, int intPageRowSize, int intCurrentPage, string strAssunto, int intDocumentType, int intGDBook, string strDataRegistoInicio, string strDataRegistoFim);
        // Add other method signatures from your full service here if they exist
    }
}