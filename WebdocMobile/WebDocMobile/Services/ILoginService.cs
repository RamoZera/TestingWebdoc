using System.Threading.Tasks;

namespace WebDocMobile.Services
{
    public interface ILoginService
    {
        Task<bool> LoginUserBasic(string strHashCode, string strUserName, string strPassword, string strDomainName);
    }
}