using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDocMobile.Models.NewProcesses
{
    public class NewProcessRequest
    {
        public string subject { get; set; }
        public string observations { get; set; }
        public string reference { get; set; }
        public DateTime date { get; set; }
        public DateTime responseDate { get; set; }
        public int typeId { get; set; }
        public int classifierId { get; set; }
        public int entityId { get; set; }
        public bool urgent { get; set; }
        public bool physicalFile { get; set; }
        public bool waitsResponse { get; set; }
        public string documentFile { get; set; }
        public string documentExtension { get; set; }
        public bool convertToPDF { get; set; }
    }

}

