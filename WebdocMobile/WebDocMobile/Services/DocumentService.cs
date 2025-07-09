﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebDocMobile.Helpers.WsMethods;
using WebDocMobile.Models.DocumentService;

namespace WebDocMobile.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly HttpClient _httpClient;
        private readonly ISettingsService _settingsService;

        public DocumentService(ISettingsService settingsService)
        {
            _httpClient = new HttpClient();
            _settingsService = settingsService;
        }

        private async Task<T> PostAsync<T>(string endpoint, object payload)
        {
            var url = $"{_settingsService.BaseAddress}/{endpoint}";
            var jsonPayload = JsonConvert.SerializeObject(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(jsonResponse);
        }

        public Task<List<GDDocument>> ListMyDocuments(string strHashCode, int intPageRowSize, int intCurrentPage)
        {
            var payload = new { strHashCode, intPageRowSize, intCurrentPage };
            // This endpoint is an assumption based on typical API design. You may need to adjust it.
            return PostAsync<List<GDDocument>>("Document/ListMyDocuments", payload);
        }

        public Task<List<GDDocument>> ListAllMyDocuments(string strHashCode, int intPageRowSize, int intCurrentPage, string strSearch)
        {
            var payload = new { strHashCode, intPageRowSize, intCurrentPage, strSearch };
            return PostAsync<List<GDDocument>>("Document/ListAllMyDocuments", payload);
        }

        public Task<List<GDDocument>> ListDocuments(string strHashCode, int intPageRowSize, int intCurrentPage)
        {
            var payload = new { strHashCode, intPageRowSize, intCurrentPage };
            return PostAsync<List<GDDocument>>("Document/ListDocuments", payload);
        }

        public Task<List<GDDocument>> ListALLDocument(string strHashCode, int intPageRowSize, int intCurrentPage, string strAssunto, int intDocumentType, int intGDBook, string strDataRegistoInicio, string strDataRegistoFim)
        {
            var payload = new ListALLDocumentDto
            {
                strHashCode = strHashCode,
                intPageRowSize = intPageRowSize,
                intCurrentPage = intCurrentPage,
                strAssunto = strAssunto,
                intDocumentType = intDocumentType,
                intGDBook = intGDBook,
                strDataRegistoInicio = strDataRegistoInicio,
                strDataRegistoFim = strDataRegistoFim
            };
            return PostAsync<List<GDDocument>>("Document/ListALLDocument", payload);
        }
    }
}