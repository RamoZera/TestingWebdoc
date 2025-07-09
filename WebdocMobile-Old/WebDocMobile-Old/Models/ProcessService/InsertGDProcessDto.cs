using WebDocMobile.Helpers.WsMethods;

namespace WebDocMobile.Models.ProcessService
{
    public class InsertGDProcessDto
    {
        public string strHashCode { get; set; }
        public GDProcess wsProcess { get; set; }
        public string fileBuf { get; set; }
        public string strExt { get; set; }
    }
}
