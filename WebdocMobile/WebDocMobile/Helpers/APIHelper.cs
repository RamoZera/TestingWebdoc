//-----------------------------------------------------------------------
// <copyright file="APIHelper.cs" company="Home">
//     Author: Horus Ra
//     Copyright (c) Home. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using WebDocMobile.Models.DashBoard;
using System.Text.Json;
using WebDocMobile.Helpers.WsMethods;
using WebDocMobile.Models;
using WebDocMobile.Models.BookService;

namespace WebDocMobile.Helpers
{
    internal static class APIHelper
    {
        internal const string jsonMime = "application/json";
        internal const string message_invalidRequest = "Solicitação inválida";
        internal const string message_invalidResponse = "Resposta inválida";
        internal const string dateCustom = "dd/MM/yyyy hh:mm";

        internal static bool CheckForInternetConnection(bool throwException = true)
        {
            if(Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                if(throwException)
                    throw new ApplicationException("Sem conexão com a internet");
                return false;
            }
            return true;
        }

        internal static GenericResponse<T> GetInvalidRequest<T>() => GetErrorResponse<T>(message_invalidRequest);

        internal static GenericResponse<T> GetInvalidResponse<T>() => GetErrorResponse<T>(message_invalidResponse);

        internal static GenericResponse<T> GetErrorResponse<T>(string error) => new GenericResponse<T>
        {
            Error = error,
            Status = ReturnStatus.Error
        };

        internal static void FillFormDocumentData(LoadDefaultDataDto data)
        {
            AppDefaultData.Books = new System.Collections.ObjectModel.ObservableCollection<IdName<int, string>>(data.Books);
            AppDefaultData.ProcessType = new System.Collections.ObjectModel.ObservableCollection<IdName<int, string>>(data.ProcessType);
            AppDefaultData.DocumentType = new System.Collections.ObjectModel.ObservableCollection<DocumentTypeDto>(data.DocumentType);
            AppDefaultData.Classifiers = new System.Collections.ObjectModel.ObservableCollection<IdName<int, string>>(data.Classifiers);
            AppDefaultData.SendReceive = new System.Collections.ObjectModel.ObservableCollection<IdName<int, string>>(data.SendReceive);
            AppDefaultData.Users = new System.Collections.ObjectModel.ObservableCollection<Models.BookService.User>(data.Users);
            AppDefaultData.Group = new System.Collections.ObjectModel.ObservableCollection<GroupDto>(data.Groups);


        }
    }
}