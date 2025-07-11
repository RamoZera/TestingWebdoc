using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDocMobile.Models.NewProcesses
{
    public class ProcessMetadataResponse
    {

        public string classifier { get; set; }
        public string entity { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public string workflowState { get; set; }
        public string image { get; set; }
        public string subject { get; set; }
        public string observations { get; set; }
        public string reference { get; set; }
        public DateTime date { get; set; }
        public DateTime responseDate { get; set; }
        public string number { get; set; }


    }
}
