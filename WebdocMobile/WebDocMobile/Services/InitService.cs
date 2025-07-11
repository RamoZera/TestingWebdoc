using System.Diagnostics;
using System.Text.Json;
using WebDocMobile.Models.InitService;

namespace WebDocMobile.Services
{
    public interface IInitService { }

    public class InitService : IInitService
    {
        private readonly HttpClient _httpClient;

        public InitService()
        {
            _httpClient = new HttpClient();
        }

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
    }
}
