using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDocMobile.Models.DocumentService
{
    public class AddDocumentRelationshipDto
    {
        public string strHashCode {  get; set; }
        public string strDocumentIDEncrypted { get; set; }
        public string strDocumentOtherIDEncrypted { get; set; }
    }
}
