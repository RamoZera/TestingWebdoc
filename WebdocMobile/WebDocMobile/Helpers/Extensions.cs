//-----------------------------------------------------------------------
// <copyright file="Extensions.cs" company="Home">
//     Author: Horus Ra
//     Copyright (c) Home. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Net.Http.Headers;
using System.Text.Json;
using WebDocMobile.Models;

namespace WebDocMobile.Helpers
{
    internal static class Extensions
    {
        internal static void _SetAcceptMediaType(this HttpClient httpClient, string type)
        {
            if(httpClient != null)
                if(!httpClient.DefaultRequestHeaders.Accept.Any(ah => ah is MediaTypeWithQualityHeaderValue mt && mt.MediaType.Equals(type, StringComparison.OrdinalIgnoreCase)))
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(type));
        }

        internal static void _SetJSONMediaType(this HttpClient httpClient) => httpClient._SetAcceptMediaType(APIHelper.jsonMime);

        internal static void _SetBearerToken(this HttpClient httpClient, string token)
        {
            if(httpClient != null)
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        internal static void _StandarSetup(this HttpClient httpClient, string token)
        {
            httpClient._SetJSONMediaType();
            httpClient._SetBearerToken(token);
        }

        internal static string _GetExceptionMessage(this Exception exc) => exc is AggregateException ag ? ag.Message : exc.InnerException?.Message ?? exc.Message;

        internal static GenericResponse<T> _CallAPI<T>(this HttpClient httpClient, string urlbase, StringContent httpContent, out bool navigateToLogin)
        {
            navigateToLogin = false;
            try
            {
                GenericResponse<T> result = httpClient._ExecuteWithRefresh<T>(urlbase, httpContent);
                if(result.Status == ReturnStatus.Relog)
                    navigateToLogin = true;
                return result;
            }
            catch(Exception e)
            {
                return APIHelper.GetErrorResponse<T>(e._GetExceptionMessage());
            }
        }

        private static GenericResponse<T> _ExecuteWithRefresh<T>(this HttpClient httpClient, string url, StringContent httpContent)
        {
            Uri urlbase = new Uri(url);
            string responseString;
            GenericResponse<T> result = null;
            GenericResponse<string> refreshResult;
            if(httpClient != null)
            {
                bool redo;
                Uri refreshURI = new Uri($"{App.BaseAddress}api/refresh_token");
                do
                {
                    redo = false;
                    using(HttpResponseMessage response = httpContent == null ? httpClient.GetAsync(urlbase).Result : httpClient.PostAsync(urlbase, httpContent).Result)
                        try
                        {
                            if((responseString = response.Content.ReadAsStringAsync().Result)?._DeserializeOrDefault(JSONSupport.jsonSerializerOptions_Read, out result) == true)
                            {
                                if(result.Status == ReturnStatus.Expired)
                                    using(HttpResponseMessage refreshResponse = httpClient.GetAsync(refreshURI).Result)
                                        try
                                        {
                                            if((responseString = refreshResponse.Content.ReadAsStringAsync().Result)?._DeserializeOrDefault(JSONSupport.jsonSerializerOptions_Read, out refreshResult) == true)
                                            {
                                                if(refreshResult.Status == ReturnStatus.Success)
                                                {
                                                    App.UserDetails.Token = refreshResult.Result;
                                                    App.UserDetails.Save();
                                                    httpClient._SetBearerToken(refreshResult.Result);
                                                    redo = true;
                                                }
                                            }
                                            else
                                                return APIHelper.GetErrorResponse<T>(responseString ?? APIHelper.message_invalidResponse);
                                        }
                                        catch(Exception e)
                                        {
                                            return APIHelper.GetErrorResponse<T>(e._GetExceptionMessage());
                                        }
                            }
                            else
                                return APIHelper.GetErrorResponse<T>(responseString ?? APIHelper.message_invalidResponse);
                        }
                        catch(Exception e)
                        {
                            return APIHelper.GetErrorResponse<T>(e._GetExceptionMessage());
                        }
                }
                while(redo);
            }
            return result ?? APIHelper.GetInvalidRequest<T>();
        }

        internal static bool _DeserializeOrDefault<T>(this string json, JsonSerializerOptions options, out T result)
        {
            try
            {
                result = options == null ? JsonSerializer.Deserialize<T>(json) : JsonSerializer.Deserialize<T>(json, options);
                return true;
            }
            catch(JsonException)
            {
                result = default;
                return false;
            }
        }

        internal static Task _PushAsyncWithCleanup(this INavigation navigation, Page page, bool? animated = null)
        {
            Task result;
            if(animated.HasValue)
                result = navigation.PushAsync(page, animated.Value);
            else
                result = navigation.PushAsync(page);
            foreach(Page p in navigation.NavigationStack.Concat(navigation.ModalStack).Distinct().Where(p => p != page).ToList())
                navigation.RemovePage(p);
            return result;
        }

    }
}