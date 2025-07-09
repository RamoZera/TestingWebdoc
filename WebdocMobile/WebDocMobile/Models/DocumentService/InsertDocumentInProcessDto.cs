using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDocMobile.Models.DocumentService
{
    public class InsertDocumentInProcessDto
    {
        public string strHashCode { get; set; }
        public string strDocumentIDEncrypted { get; set; }
        public string strProcessIDEncrypted { get; set; }
        public int intIDProcessTab { get; set; }
    }
}
