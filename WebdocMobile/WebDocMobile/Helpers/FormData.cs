using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocMobile.Models;
using WebDocMobile.Models.BookService;

namespace WebDocMobile.Helpers
{
    public static class AppDefaultData
    {
        public static ObservableCollection<IdName<int, string>> Books { get; set; }

        public static ObservableCollection<DocumentTypeDto> DocumentType { get; set; }

        public static ObservableCollection<IdName<int, string>> ProcessType { get; set; }

        public static ObservableCollection<IdName<int, string>> Classifiers { get; set; }

        public static ObservableCollection<IdName<int, string>> SendReceive { get; set; }
        public static ObservableCollection<Models.BookService.User> Users { get; set; }
        public static ObservableCollection<GroupDto> Group { get; set; }

    }
}
