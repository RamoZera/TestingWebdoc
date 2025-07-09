using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDocMobile.Models.DocumentService
{
    public class ListALLDocumentDto
    {
        public string strHashCode { get; set; }
        public int intPageRowSize { get; set; }
        public int intCurrentPage { get; set; }
        public string strAssunto { get; set; }
        public int intDocumentType { get; set; }
        public int intGDBook { get; set; }
        public string strDataRegistoInicio { get; set; }
        public string strDataRegistoFim { get; set; }
    }
}
