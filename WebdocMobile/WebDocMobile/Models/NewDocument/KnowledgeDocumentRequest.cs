using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDocMobile.Models.NewDocument
{
    public class KnowledgeDocumentRequest
    {

        public string observations { get; set; }
        public string comments { get; set; }
        public int id { get; set; }
        public int[] usersIds { get; set; }
        public int[] groupsIds { get; set; }
    }


    public class Knowledge
    {
        public int id { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public DateTime date { get; set; }
        public object knowledgeDate { get; set; }
        public string observations { get; set; }
        public string comments { get; set; }
    }

}

