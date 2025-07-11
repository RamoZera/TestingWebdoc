//-----------------------------------------------------------------------
// <copyright file="UserBasicInfo.cs" company="Home">
//     Author: Horus Ra
//     Copyright (c) Home. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Text.Json;
using WebDocMobile.Helpers;

namespace WebDocMobile.Models
{
    public class UserBasicInfo
    {
        internal static readonly HashSet<URLIDName> EntityCodes = new HashSet<URLIDName>() { new URLIDName()
        {
            Id = "1994",
            Name = "Ambisig",
            URL = "https://wsdemo.ambisig.com/api/"
        },new URLIDName()
        {
            Id = "1995",
            Name = "INTERNAL",
            URL = "https://wsdemo.ambisig.com/api/"
        } };

        public string StrName { get; set; }

        public string StrDomain { get; set; }

        public string CodEntidade { get; set; }

        public string PIN { get; set; }

        public bool HasBiometricsActive { get; set; }

        public string Token { get; set; }

        public string Password { get; set; }

        internal static UserBasicInfo New()
        {
            UserBasicInfo result = App.UserDetails = new UserBasicInfo();
            return result;
        }

        internal static UserBasicInfo Load() => App.UserDetails = JsonSerializer.Deserialize<UserBasicInfo>(Preferences.Get(nameof(App.UserDetails), string.Empty), JSONSupport.jsonSerializerOptions_Read);

        internal void Save()
        {
            if(App.UserDetails != this)
                App.UserDetails = this;
            Preferences.Remove(nameof(App.UserDetails));
            Preferences.Set(nameof(App.UserDetails), JsonSerializer.Serialize(this, JSONSupport.jsonSerializerOptions_Write));
        }

        internal static void Clear()
        {
            App.UserDetails = null;
            Preferences.Remove(nameof(App.UserDetails));
        }

        internal bool IsCorrect(out string baseAddress)
        {
            if(PingCorrect() && !string.IsNullOrWhiteSpace(StrName) && !string.IsNullOrWhiteSpace(Password))
                return !string.IsNullOrWhiteSpace(baseAddress = GetEntityURL(CodEntidade));
            baseAddress = null;
            return false;
        }

        internal bool PingCorrect() => PIN?.Length == 6;

        internal static string GetEntityURL(string entityCode) => EntityCodes.FirstOrDefault(ec => ec.Id == entityCode)?.URL;

        public class URLIDName : IdName<string, string>
        {
            public string URL {  get; set; }
        }
    }
}