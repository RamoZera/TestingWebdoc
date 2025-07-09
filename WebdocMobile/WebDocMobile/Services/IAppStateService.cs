using System.Collections.Generic;
using WebDocMobile.Helpers.WsMethods;
using WebDocMobile.Models;

namespace WebDocMobile.Services
{
    public interface IAppStateService
    {
        UserBasicInfo UserDetails { get; set; }
        List<GDDocument> AllDocuments { get; set; }
        List<GDDocument> MyDocuments { get; set; }
        List<GDDocument> DepartmentDocuments { get; set; }
        List<GDDocument> KnownDocuments { get; set; }
        void ClearState();
    }
}