using Newtonsoft.Json;
using WebDocMobile.Models;

namespace WebDocMobile.Services
{
    public class SettingsService : ISettingsService
    {
        private const string BaseAddressKey = "baseAddress";
        public string BaseAddress
        {
            get => Preferences.Get(BaseAddressKey, string.Empty);
            set => Preferences.Set(BaseAddressKey, value);
        }

        private const string CodigoEntidadeKey = "codigoEntidade";
        public string CodigoEntidade
        {
            get => Preferences.Get(CodigoEntidadeKey, string.Empty);
            set => Preferences.Set(CodigoEntidadeKey, value);
        }

        private const string UserInfoKey = "UserDetails";
        public UserBasicInfo UserInfo
        {
            get
            {
                var json = Preferences.Get(UserInfoKey, string.Empty);
                return string.IsNullOrEmpty(json) ? null : JsonConvert.DeserializeObject<UserBasicInfo>(json);
            }
            set => Preferences.Set(UserInfoKey, JsonConvert.SerializeObject(value));
        }
    }
}
