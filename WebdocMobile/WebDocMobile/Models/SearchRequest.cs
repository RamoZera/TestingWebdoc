using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDocMobile.Models
{
    public class SearchRequest

    {
        public string search { get; set; }
        public Start start { get; set; }
        public End end { get; set; }
        public string number { get; set; }
        public string subject { get; set; }
        public string workflowState { get; set; }
        public int count { get; set; }
    }

    public class Start
    {
        public DateTime dateTime { get; set; }
        public bool useTime { get; set; }
    }

    public class End
    {
        public DateTime dateTime { get; set; }
        public bool useTime { get; set; }
    }


}
