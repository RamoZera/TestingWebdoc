using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDocMobile.Models.NewDocument
{
    
  public class NewDocumentResponse
    {
        public DocumentResponse result { get; set; }
        public int status { get; set; }
        public object error { get; set; }
    }

    public class DocumentResponse
    {
        public int id { get; set; }
        public string guid { get; set; }
        public string link { get; set; }
    }

}
