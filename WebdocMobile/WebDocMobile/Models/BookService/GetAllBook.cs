using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDocMobile.Models.BookService
{
    public class GetAllBook
    {
        public static ObservableCollection<GetBook> GetBooks()
        {
            
                var data = new ObservableCollection<GetBook>
            {
                new GetBook { StrHashCode = "Seaming-0002", Value = 22 },
                new GetBook { StrHashCode = "INE-0001", Value = 268 },
                new GetBook { StrHashCode = "010034-Ambisig", Value = 43}
            };
                return data;
           
        }
    }
}
