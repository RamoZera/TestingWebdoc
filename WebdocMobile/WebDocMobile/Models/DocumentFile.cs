using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDocMobile.Models
{
    public class GetFileDTO
    {
        public string Ext { get; set; }

        public byte[] Data { get; set; }
    }
}
