using System.Threading.Tasks;
using WebDocMobile.Helpers.WsMethods;

namespace WebDocMobile.Services
{
    public interface IProcessService
    {
        Task<GDProcess> GetProcessDataResumedByNumber(string strHashCode, string strProcessNumber);
        // Add other method signatures from your full service here if they exist
    }
}