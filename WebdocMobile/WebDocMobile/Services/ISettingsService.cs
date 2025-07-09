﻿using WebDocMobile.Models;

namespace WebDocMobile.Services
{
    public interface ISettingsService
    {
        string BaseAddress { get; set; }
        string CodigoEntidade { get; set; }
        UserBasicInfo UserInfo { get; set; }
        void ClearAllData();
    }
}