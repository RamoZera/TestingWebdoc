using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocMobile.Helpers.WsMethods;

namespace WebDocMobile.Models.DocumentService
{
    public class InsertGDDocumentNoFileDto
    {
        public string strHashCode { get; set; }
        public GDDocument wsDocument { get; set; }
    }
}
