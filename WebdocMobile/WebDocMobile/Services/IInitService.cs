using System.Threading.Tasks;
using WebDocMobile.Models.InitService;

namespace WebDocMobile.Services
{
    public interface IInitService
    {
        Task<InitDto> Init();
    }
}