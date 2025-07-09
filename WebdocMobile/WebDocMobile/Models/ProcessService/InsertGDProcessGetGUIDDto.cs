using WebDocMobile.Helpers;
using WebDocMobile.Helpers.WsMethods;

namespace WebDocMobile.Models.ProcessService
{
    public class InsertGDProcessGetGUIDDto
    {
        public string strHashCode { get; set; }
        public GDProcess wsProcess { get; set; }
        public string fileBuf { get; set; }
        public string strExt { get; set; }
    }
}
