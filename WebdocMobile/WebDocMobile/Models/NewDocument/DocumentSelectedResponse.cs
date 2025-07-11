using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDocMobile.Models.NewDocument
{
    public class DocumentSelectedResponse
    {

            public int id { get; set; }
            public string number { get; set; }
            public string subject { get; set; }
       
    }

    public class DocumentSelectedRequest
    {
        public string search { get; set; }
    }
}
