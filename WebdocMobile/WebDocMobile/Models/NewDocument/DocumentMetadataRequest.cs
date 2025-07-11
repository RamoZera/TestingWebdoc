using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDocMobile.Models.NewDocument
{
    public class DocumentMetadataRequest
    {
        public int id { get; set; }
        public Sizerequest sizeRequest { get; set; }
    }

    public class Sizerequest
    {
        public int width { get; set; }
        public int height { get; set; }
        public bool keepAspectRatio { get; set; }
    }

    public class DocumentMetadataResponse
    {
        public string book { get; set; }
        public string sendReceive { get; set; }
        public string classifier { get; set; }
        public string type { get; set; }
        public string image { get; set; }
        public string subject { get; set; }
        public string? observations { get; set; }
        public string? reference { get; set; }
        public DateTime? date { get; set; }
        public DateTime? carimboDate { get; set; }
        public DateTime? responseDate { get; set; }
        public string number { get; set; }
        public string entity { get; set; }
        public bool urgent { get; set; }
        public bool waitsResponse { get; set; }
        public bool physicalFile { get; set; }
        public bool Confidential { get; set; }
        public bool InTreatment { get; set; }
        public bool DocumentNoResponse { get; set; }
        public bool CheckOut { get; set; }
        public string WorkflowState { get; set; }
    }

    

    public class MovementText
    {
        public string text { get; set; }
    }

    public class DocumentUpdateCheckOut
    {
        public int id { get; set; }
        public Sizerequest sizeRequest { get; set; }
        public bool CheckOut { get; set; }
    }

}

