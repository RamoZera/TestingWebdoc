using Newtonsoft.Json;
using WebDocMobile.Models;

namespace WebDocMobile.Services
{
    public class SettingsService : ISettingsService
    {
        public string BaseAddress
        {
            get => Preferences.Get(nameof(BaseAddress), string.Empty);
            set => Preferences.Set(nameof(BaseAddress), value);
        }

        public string CodigoEntidade
        {
            get => Preferences.Get(nameof(CodigoEntidade), string.Empty);
            set => Preferences.Set(nameof(CodigoEntidade), value);
        }

        public UserBasicInfo UserInfo
        {
            get
            {
                var serializedData = Preferences.Get(nameof(UserInfo), string.Empty);
                return string.IsNullOrEmpty(serializedData) ? null : JsonConvert.DeserializeObject<UserBasicInfo>(serializedData);
            }
            set
            {
                var serializedData = JsonConvert.SerializeObject(value);
                Preferences.Set(nameof(UserInfo), serializedData);
            }
        }

        public void ClearAllData()
        {
            Preferences.Clear();
        }
    }
}