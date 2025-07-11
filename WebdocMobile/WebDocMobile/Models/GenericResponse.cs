//-----------------------------------------------------------------------
// <copyright file="GenericResponse.cs" company="Home">
//     Author: Horus Ra
//     Copyright (c) Home. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace WebDocMobile.Models
{
    public enum ReturnStatus
    {
        Success,
        Error,
        Expired,
        Invalid,
        Relog
    }

    public class GenericResponse<T>
    {
        public T Result { get; set; }

        public ReturnStatus Status { get; set; }

        public string Error { get; set; }
    }
}
