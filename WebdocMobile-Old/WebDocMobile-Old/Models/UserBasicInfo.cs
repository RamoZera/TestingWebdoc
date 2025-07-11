﻿﻿#nullable enable

namespace WebDocMobile.Models
{
    public class UserBasicInfo
    {
        public string? strHashCode {  get; set; }
        public string? strName { get; set; }
        public string? strDomain { get; set; }
        public string? codEntidade { get; set; }
        public string? PIN { get; set; }
        public bool hasBiometricsActive { get; set; }
    }
}
