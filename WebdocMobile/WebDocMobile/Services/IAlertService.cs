using System.Threading.Tasks;

namespace WebDocMobile.Services
{
    public interface IAlertService
    {
        Task ShowAlert(string title, string message);
    }
}