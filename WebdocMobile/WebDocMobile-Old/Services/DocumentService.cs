using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Telerik.Windows.Documents.Flow.Model;
using WebDocMobile.Helpers.WsMethods;
using WebDocMobile.Models.DocumentService;

namespace WebDocMobile.Services
{
    public interface IDocumentService
    {
        Task InsertGDDocument(string hashCode, GDDocument document, string fileBuf, string ext);
        Task AddDocumentAttachment(string hashCode, string docProcNumber, string docRef, string file, string fileName, string extension);
        Task<List<GDDocument>> listALLDocument(string hashCode, int pageRowSize, int currentPage, string assunto, int documentType, int gdBook, string dataRegistoInicio, string dataRegistoFim);
        Task InsertDocumentInProcess(string hashCode, string documentIDEncrypted, string processIDEncrypted, int idProcessTab);
        Task InsertGDDocumentGetGUID(string hashCode, GDDocument document, string fileBuf, string ext);
        Task InsertGDDocumentGetID(string hashCode, GDDocument document, string fileBuf, string ext);
        Task InsertGDDocumentGetIDAndNumber(string hashCode, GDDocument document, string fileBuf, string ext);
        Task InsertGDDocumentNoFile(string hashCode, GDDocument document);
        Task AddDocumentoRelationship(string hashCode, string documentIDEncryted, string documentOtherIDEncrypted);
        Task<GDDocument> GetDocumentDataResumedByNumber(string hashCode, string documentNumber);
        Task<GDDocument> GetDocumentDataResumed(string hashCode, string documentoIDEncrypted);
        Task<List<GDDocument>> ListMyDocuments(string hashCode, int pageRowSize, int currentPage);
        Task<int> TotalMyDocuments(string hashCode);
        Task<List<GDDocument>> ListDocuments(string hashCode, int pageRowSize, int currentPage);
        Task<List<GDDocument>> ListAllMyDocuments(string hashCode, int pageRowSize, int currentPage, string search);
    }

    public class DocumentService : IDocumentService
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public DocumentService()
        {
#if DEBUG
            HttpClientHandler insecureHandler = GetInsecureHandler();
            _httpClient = new HttpClient(insecureHandler);
#else
            _httpClient = new HttpClient();
#endif
            if (Preferences.ContainsKey(nameof(App.codigoEntidade)) && Preferences.ContainsKey(nameof(App.baseAddress)))
            {
                string codigoEntidade = Preferences.Get(nameof(App.codigoEntidade), "");
                string baseAddress = Preferences.Get(nameof(App.baseAddress), "");
                if (codigoEntidade == "\"1994\"")
                {
                    _url = $"{baseAddress.Substring(1, baseAddress.Length - 2)}/api/v1";
                }
                else if (codigoEntidade == "\"1995\"")
                {
                    _url = $"{baseAddress.Substring(1, baseAddress.Length - 2)}/wsservices/wsgetinfo.asmx";
                }
            }

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
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

        public async Task AddDocumentAttachment(string hashCode, string docProcNumber, string docRef, string file, string fileName, string extension)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No Internet Connection");
                return;
            }

            try
            {
                AddDocumentAttachmentDto dto = new AddDocumentAttachmentDto()
                {
                    strHashCode = hashCode,
                    strDocProcNumber = docProcNumber,
                    strDocRef = docRef,
                    file = file,
                    fileName = fileName,
                    extension = extension
                };

                StringContent content = new StringContent(JsonSerializer.Serialize(dto, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/Document/AddDocumentAttachment", content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Attachment was added to document");
                }
                else
                {
                    Debug.WriteLine("Unable to add attachment");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public async Task AddDocumentoRelationship(string hashCode, string documentIDEncryted, string documentOtherIDEncrypted)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No Internet Connection");
                return;
            }

            try
            {
                AddDocumentRelationshipDto dto = new AddDocumentRelationshipDto()
                {
                    strHashCode = hashCode,
                    strDocumentIDEncrypted = documentIDEncryted,
                    strDocumentOtherIDEncrypted = documentOtherIDEncrypted
                };

                StringContent content = new StringContent(JsonSerializer.Serialize(dto, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/Document/AddDocumentoRelationship", content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Relationship was created");
                }
                else
                {
                    Debug.WriteLine("Unable to create relationship");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }
        public async Task<GDDocument> GetDocumentDataResumed(string hashCode, string documentoIDEncrypted)
        {
            GDDocument document = new GDDocument();

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No Internet Connection");
                return document;
            }

            try
            {
                GetDocumentDataResumedDto dto = new GetDocumentDataResumedDto()
                {
                    strHashCode = hashCode,
                    strDocumentIDEncrypted = documentoIDEncrypted
                };

                StringContent contentToSend = new StringContent(JsonSerializer.Serialize(dto, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/Document/GetDocumentDataResumed", contentToSend);

                if (response.IsSuccessStatusCode)
                {
                    string contentToReceive = await response.Content.ReadAsStringAsync();

                    document = JsonSerializer.Deserialize<GDDocument>(contentToReceive, _jsonSerializerOptions);
                }
                else
                {
                    Debug.WriteLine("No Http Response");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            return document;
        }
        public async Task<GDDocument> GetDocumentDataResumedByNumber(string hashCode, string documentNumber)
        {
            GDDocument document = new GDDocument();
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No Internet Connection");
                return document;
            }

            try
            {
                GetDocumentDataResumedByNumberDto dto = new GetDocumentDataResumedByNumberDto()
                {
                    strHashCode = hashCode,
                    strDocumentNumber = documentNumber
                };

                StringContent contentToSend = new StringContent(JsonSerializer.Serialize(dto, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/Document/getDocumentDataResumedByNember", contentToSend);

                if (response.IsSuccessStatusCode)
                {
                    string contentToReceive = await response.Content.ReadAsStringAsync();

                    document = JsonSerializer.Deserialize<GDDocument>(contentToReceive, _jsonSerializerOptions);
                }
                else
                {
                    Debug.WriteLine("No Http Response");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            return document;
        }
        public async Task InsertDocumentInProcess(string hashCode, string documentIDEncrypted, string processIDEncrypted, int idProcessTab)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No Internet Connection");
                return;
            }

            try
            {
                InsertDocumentInProcessDto dto = new InsertDocumentInProcessDto()
                {
                    strHashCode = hashCode,
                    strDocumentIDEncrypted = documentIDEncrypted,
                    strProcessIDEncrypted = processIDEncrypted,
                    intIDProcessTab = idProcessTab
                };

                StringContent content = new StringContent(JsonSerializer.Serialize(dto, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/Document/InsertDocumentInProcess", content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Document was created");
                }
                else
                {
                    Debug.WriteLine("Error during the document creation");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public async Task InsertGDDocument(string hashCode, GDDocument document, string fileBuf, string ext)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No Internet Connection");
                return;
            }

            try
            {
                InsertGDDocumentDto dto = new InsertGDDocumentDto()
                {
                    strHashCode = hashCode,
                    wsDocument = document,
                    fileBuf = fileBuf,
                    strExt = ext
                };

                StringContent content = new StringContent(JsonSerializer.Serialize(dto, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/Document/InsertGDDocument", content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Document was created");
                }
                else
                {
                    Debug.WriteLine("Error during the document creation");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public async Task InsertGDDocumentGetGUID(string hashCode, GDDocument document, string fileBuf, string ext)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No Internet Connection");
                return;
            }

            try
            {
                InsertGDDocumentGetGUIDDto dto = new InsertGDDocumentGetGUIDDto()
                {
                    strHashCode = hashCode,
                    wsDocument = document,
                    fileBuf = fileBuf,
                    strExt = ext
                };

                StringContent content = new StringContent(JsonSerializer.Serialize(dto, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/Document/InsertGDDocumentGetGUID", content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Document was created");
                }
                else
                {
                    Debug.WriteLine("Error during the document creation");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public async Task InsertGDDocumentGetID(string hashCode, GDDocument document, string fileBuf, string ext)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No Internet Connection");
                return;
            }
            try
            {
                InsertGDDocumentGetIDDto dto = new InsertGDDocumentGetIDDto()
                {
                    strHashCode = hashCode,
                    wsDocument = document,
                    fileBuf = fileBuf,
                    strExt = ext
                };

                StringContent content = new StringContent(JsonSerializer.Serialize(dto, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/Document/InsertGDDocumentGetID", content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Document was created");
                }
                else
                {
                    Debug.WriteLine("Error during the document creation");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public async Task InsertGDDocumentGetIDAndNumber(string hashCode, GDDocument document, string fileBuf, string ext)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No Internet Connection");
                return;
            }

            try
            {
                InsertGDDocumentGetIDAndNumberDto dto = new InsertGDDocumentGetIDAndNumberDto()
                {
                    strHashCode = hashCode,
                    wsDocument = document,
                    fileBuf = fileBuf,
                    strExt = ext
                };

                StringContent content = new StringContent(JsonSerializer.Serialize(dto, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/Document/InsertGDDocumentGetIDAndNumber", content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Document was created");
                }
                else
                {
                    Debug.WriteLine("Error during the document creation");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public async Task InsertGDDocumentNoFile(string hashCode, GDDocument document)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No Internet Connection");
                return;
            }

            try
            {
                InsertGDDocumentNoFileDto dto = new InsertGDDocumentNoFileDto()
                {
                    strHashCode = hashCode,
                    wsDocument = document
                };

                StringContent content = new StringContent(JsonSerializer.Serialize(dto, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/Document/InsertGDDocumentNoFile", content);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Document was created");
                }
                else
                {
                    Debug.WriteLine("Error during the document creation");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public async Task<List<GDDocument>> listALLDocument(string hashCode, int pageRowSize, int currentPage, string assunto, int documentType, int gdBook, string dataRegistoInicio, string dataRegistoFim)
        {
            List<GDDocument> list = new List<GDDocument>();
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No Internet Connection");
                return list;
            }

            try
            {
                listALLDocumentDto dto = new listALLDocumentDto()
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

                StringContent contentToSend = new StringContent(JsonSerializer.Serialize(dto, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/Document/listALLDocument", contentToSend);

                if (response.IsSuccessStatusCode)
                {
                    string contentToReceive = await response.Content.ReadAsStringAsync();

                    list = JsonSerializer.Deserialize<List<GDDocument>>(contentToReceive, _jsonSerializerOptions);
                }
                else
                {
                    Debug.WriteLine("No Http Response");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            return list;
        }

        public async Task<List<GDDocument>> ListDocuments(string hashCode, int pageRowSize, int currentPage)
        {
            List<GDDocument> list = new List<GDDocument>();
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No Internet Connection");
                return list;
            }

            try
            {
                ListDocumentsDto dto = new ListDocumentsDto
                {
                    strHashCode = hashCode,
                    intPageRowSize = pageRowSize,
                    intCurrentPage = currentPage
                };

                StringContent contentToSend = new StringContent(JsonSerializer.Serialize(dto, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/Document/listDocuments", contentToSend);

                if (response.IsSuccessStatusCode)
                {
                    string contentToReceive = await response.Content.ReadAsStringAsync();

                    list = JsonSerializer.Deserialize<List<GDDocument>>(contentToReceive, _jsonSerializerOptions);
                }
                else
                {
                    Debug.WriteLine("No Http Response");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            return list;
        }

        public async Task<List<GDDocument>> ListMyDocuments(string hashCode, int pageRowSize, int currentPage)
        {
            List<GDDocument> list = new List<GDDocument>();
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No Internet Connection");
                return list;
            }

            try
            {
                ListMyDocumentsDto dto = new ListMyDocumentsDto
                {
                    strHashCode = hashCode,
                    intPageRowSize = pageRowSize,
                    intCurrentPage = currentPage
                };

                StringContent contentToSend = new StringContent(JsonSerializer.Serialize(dto, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/MyDocumentsApp/listMyDocuments", contentToSend);

                if (response.IsSuccessStatusCode)
                {
                    string contentToReceive = await response.Content.ReadAsStringAsync();

                    list = JsonSerializer.Deserialize<List<GDDocument>>(contentToReceive, _jsonSerializerOptions);
                }
                else
                {
                    Debug.WriteLine("No Http Response");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            return list;
        }

        public async Task<List<GDDocument>> ListAllMyDocuments(string hashCode, int pageRowSize, int currentPage, string search)
        {
            List<GDDocument> list = new List<GDDocument>();
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No Internet Connection");
                return list;
            }
            try
            {
                ListAllMyDocumentsDto dto = new ListAllMyDocumentsDto
                {
                    strHashCode = hashCode,
                    intPageRowSize = pageRowSize,
                    intCurrentPage = currentPage,
                    strSearch = search
                };

                StringContent contentToSend = new StringContent(JsonSerializer.Serialize(dto, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/MyDocumentsApp/listAllMyDocuments", contentToSend);

                if (response.IsSuccessStatusCode)
                {
                    string contentToReceive = await response.Content.ReadAsStringAsync();

                    list = JsonSerializer.Deserialize<List<GDDocument>>(contentToReceive, _jsonSerializerOptions);
                }
                else
                {
                    Debug.WriteLine("No Http Response");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            return list;
        }

        public async Task<int> TotalMyDocuments(string hashCode)
        {
            int myDocuments = 0;
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No Internet Connection");
                return myDocuments;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/MyDocumentsApp/listALLDocument/{hashCode}");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    myDocuments = JsonSerializer.Deserialize<int>(content, _jsonSerializerOptions);
                }
                else
                {
                    Debug.WriteLine("No Http Response");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            return myDocuments;
        }

    }
}
