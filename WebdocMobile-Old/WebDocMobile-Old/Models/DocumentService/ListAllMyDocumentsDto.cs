using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDocMobile.Models.DocumentService
{
    public class ListAllMyDocumentsDto
    {
        public string strHashCode {  get; set; }
        public int intPageRowSize { get; set; }
        public int intCurrentPage { get; set; }
        public string strSearch { get; set; }
    }
}
