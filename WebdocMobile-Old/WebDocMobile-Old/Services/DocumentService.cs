using System.Diagnostics;
using System.Text;
using System.Text.Json;
using WebDocMobile.Helpers.WsMethods;
using WebDocMobile.Models.DocumentService;

namespace WebDocMobile.Services
{
    public interface IDocumentService
    {
        Task<List<GDDocument>> ListMyDocuments(string hashCode, int pageRowSize, int currentPage);
        Task<List<GDDocument>> ListDocuments(string hashCode, int pageRowSize, int currentPage);
        Task<List<GDDocument>> ListAllMyDocuments(string hashCode, int pageRowSize, int currentPage, string search);
        Task<List<GDDocument>> ListAllDocuments(string hashCode, int pageRowSize, int currentPage, string assunto, int documentType, int gdBook, string dataRegistoInicio, string dataRegistoFim);
    }

    public class DocumentService : IDocumentService
    {
        private readonly ISettingsService _settingsService;
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public DocumentService(ISettingsService settingsService)
        {
            _settingsService = settingsService;
#if DEBUG
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };
            _httpClient = new HttpClient(handler);
#else
            _httpClient = new HttpClient();
#endif
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        private string GetApiUrl()
        {
            var baseAddress = _settingsService.BaseAddress;
            if (string.IsNullOrEmpty(baseAddress))
            {
                Debug.WriteLine("Error: BaseAddress is not set in SettingsService.");
                return string.Empty;
            }
            return baseAddress;
        }

        public async Task<List<GDDocument>> ListAllDocuments(string hashCode, int pageRowSize, int currentPage, string assunto, int documentType, int gdBook, string dataRegistoInicio, string dataRegistoFim)
        {
            var documents = new List<GDDocument>();
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet) return documents;

            try
            {
                var apiUrl = GetApiUrl();
                if (string.IsNullOrEmpty(apiUrl)) return documents;

                var dto = new ListAllDocumentsDto
                {
                    strHashCode = hashCode,
                    intPageRowSize = pageRowSize,
                    intCurrentPage = currentPage,
                    strAssunto = assunto,
                    intDocumentType = documentType,
                    intGDBook = gdBook,
                    strDataRegistoInicio = dataRegistoInicio,
                    strDataRegistoFim = dataRegistoFim
                };

                var json = JsonSerializer.Serialize(dto, _jsonSerializerOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{apiUrl}/Document/listALLDocument", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    documents = JsonSerializer.Deserialize<List<GDDocument>>(responseContent, _jsonSerializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in ListAllDocuments: {ex.Message}");
            }
            return documents;
        }

        public async Task<List<GDDocument>> ListAllMyDocuments(string hashCode, int pageRowSize, int currentPage, string search)
        {
            var documents = new List<GDDocument>();
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet) return documents;

            try
            {
                var apiUrl = GetApiUrl();
                if (string.IsNullOrEmpty(apiUrl)) return documents;

                var dto = new ListAllMyDocumentsDto
                {
                    strHashCode = hashCode,
                    intPageRowSize = pageRowSize,
                    intCurrentPage = currentPage,
                    strSearch = search
                };

                var json = JsonSerializer.Serialize(dto, _jsonSerializerOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{apiUrl}/Document/ListAllMyDocuments", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    documents = JsonSerializer.Deserialize<List<GDDocument>>(responseContent, _jsonSerializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in ListAllMyDocuments: {ex.Message}");
            }
            return documents;
        }

        public async Task<List<GDDocument>> ListDocuments(string hashCode, int pageRowSize, int currentPage)
        {
            var documents = new List<GDDocument>();
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet) return documents;

            try
            {
                var apiUrl = GetApiUrl();
                if (string.IsNullOrEmpty(apiUrl)) return documents;

                var dto = new ListDocumentsDto
                {
                    strHashCode = hashCode,
                    intPageRowSize = pageRowSize,
                    intCurrentPage = currentPage
                };

                var json = JsonSerializer.Serialize(dto, _jsonSerializerOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{apiUrl}/Document/ListDocuments", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    documents = JsonSerializer.Deserialize<List<GDDocument>>(responseContent, _jsonSerializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in ListDocuments: {ex.Message}");
            }
            return documents;
        }

        public async Task<List<GDDocument>> ListMyDocuments(string hashCode, int pageRowSize, int currentPage)
        {
            var documents = new List<GDDocument>();
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet) return documents;

            try
            {
                var apiUrl = GetApiUrl();
                if (string.IsNullOrEmpty(apiUrl)) return documents;

                var dto = new ListMyDocumentsDto
                {
                    strHashCode = hashCode,
                    intPageRowSize = pageRowSize,
                    intCurrentPage = currentPage
                };

                var json = JsonSerializer.Serialize(dto, _jsonSerializerOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{apiUrl}/Document/ListMyDocuments", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    documents = JsonSerializer.Deserialize<List<GDDocument>>(responseContent, _jsonSerializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in ListMyDocuments: {ex.Message}");
            }
            return documents;
        }
    }
}
