//-----------------------------------------------------------------------
// <copyright file="Support.cs" company="Home">
//     Author: Horus Ra
//     Copyright (c) Home. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Text.Encodings.Web;
using System.Text.Json;

namespace WebDocMobile.Helpers
{
    public static class JSONSupport
    {
        public static readonly JsonSerializerOptions jsonSerializerOptions_Write = new JsonSerializerOptions()
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
        public static readonly JsonSerializerOptions jsonSerializerOptions_Read = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
    }
}