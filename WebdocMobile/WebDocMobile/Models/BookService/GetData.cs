using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WebDocMobile.Models.BookService
{


    public class LoadDefaultDataDto
    {
        public List<IdName<int, string>> Books { get; set; }

        public List<DocumentTypeDto> DocumentType { get; set; }

        public List<IdName<int, string>> ProcessType { get; set; }

        public List<IdName<int, string>> Classifiers { get; set; }

        public List<IdName<int, string>> SendReceive { get; set; }
        public List<User> Users { get; set; }
        public List<GroupDto> Groups { get; set; }

    }

    public class User :INotifyPropertyChanged
    {
        public int Id  { get; set; }
        public string Name { get; set; }
        private bool _Selected = false;
        public bool Selected
        {
            get { return _Selected; }
            set
            {
                if (Selected != value)
                {
                    _Selected = value;
                    this.OnPropertyChanged(nameof(Selected));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class DocumentTypeDto : IdName<int, string>
    {
        public int BookId { get; set; }
    }
    public class GroupDto : IdName<int, string>
    {
        public List<User> Users { get; set; }

    }

}
