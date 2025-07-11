using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDocMobile.Models.NewDocument
{
    public class RelatedDocumentResponse
    {
        public string by { get; set; }
        public DateTime date { get; set; }
        public DateTime sentData { get; set; }
        public string workflowState { get; set; }
        public int id { get; set; }
        public string number { get; set; }
        public string subject { get; set; }
    }

    public class AddRelatedResponse
    {
        public object result { get; set; }
        public int status { get; set; }
        public string  error { get; set; }
    }

}

