using WebDocMobile.Helpers.WsMethods;
using WebDocMobile.Models;

namespace WebDocMobile.Services
{
    public class AppStateService : IAppStateService
    {
        public UserBasicInfo UserDetails { get; set; }
        public List<GDDocument> AllDocuments { get; set; }
        public List<GDDocument> MyDocuments { get; set; }
        public List<GDDocument> DepartmentDocuments { get; set; }
        public List<GDDocument> KnownDocuments { get; set; }

        public void ClearAllState()
        {
            UserDetails = null;
            AllDocuments = null;
            MyDocuments = null;
            DepartmentDocuments = null;
            KnownDocuments = null;
        }
    }
}
