namespace WebDocMobile.Models.WorkflowService
{
    public class ForwardWkfFromIDUserDto
    {
        public string strHashCode { get; set; }
        public string strDocumentIDEncrypted { get; set; }
        public int intIDWorkflowStateTo { get; set; }
        public int intIDTeamTo { get; set; }
        public int intIDUserTo { get; set; }
        public string strRemarks { get; set; }
        public int intIDUserFrom { get; set; }
    }
}
