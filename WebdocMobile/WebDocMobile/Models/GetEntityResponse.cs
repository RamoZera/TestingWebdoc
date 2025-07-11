using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDocMobile.Models
{
    public class GetEntityResponse
    {


        public Entity[] result { get; set; }
        public int status { get; set; }
        public object error { get; set; }
    }

    public class Entity
    {
        public int id { get; set; }
        public string Name { get; set; }
    }


    public class EntityRequest
    {
        public string search { get; set; }
    }
    public class RelatedDocumentRequest
    {
        public int id { get; set; }
    }


    public class AddRelatedDocumentRequest
    {

            public int id { get; set; }
            public int[] targetIds { get; set; }
        

    }

}




