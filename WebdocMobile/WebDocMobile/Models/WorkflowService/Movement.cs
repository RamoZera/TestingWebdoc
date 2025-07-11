using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDocMobile.Models.WorkflowService
{
    public class Movement
    {
        public string observations { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public DateTime date { get; set; }
        public int id { get; set; }
        public int number { get; set; }


    }
}
