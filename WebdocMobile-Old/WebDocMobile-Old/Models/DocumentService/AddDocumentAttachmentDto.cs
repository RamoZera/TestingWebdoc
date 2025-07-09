using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDocMobile.Models.DocumentService
{
    public class AddDocumentAttachmentDto
    {
        public string strHashCode { get; set; }
        public string strDocProcNumber { get; set; }
        public string strDocRef { get; set; }
        public string file { get; set; }
        public string fileName { get; set; }
        public string extension { get; set; }
    }
}
