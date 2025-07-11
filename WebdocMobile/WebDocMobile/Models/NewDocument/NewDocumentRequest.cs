using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDocMobile.Models.NewDocument
{
    public class NewDocumentRequest
    {

        public string documentFile { get; set; }
        public string documentExtension { get; set; }
        public string subject { get; set; }
        public int bookId { get; set; }
        public int TypeId { get; set; }
        public int sendReceiveId { get; set; }
        public int classifierId { get; set; }
        public DateTime? carimboDate { get; set; }
        public DateTime? responseDate { get; set; }
        public DateTime? Date { get; set; }
        public string? reference { get; set; }
        public string? observations { get; set; }
        public int entityId { get; set; }
        public bool urgent { get; set; }
        public bool waitsResponse { get; set; }
        public bool physicalFile { get; set; }
        public object convertToPDF { get; set; }
        public bool Confidential { get; set; }
        public bool InTreatment { get; set; }
        public bool DocumentNoResponse { get; set; }
    }
}
