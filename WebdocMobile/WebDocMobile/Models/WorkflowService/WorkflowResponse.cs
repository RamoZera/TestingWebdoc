using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WebDocMobile.Models.WorkflowService
{
    public class WorkflowResponse
    {

        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string By { get; set; }
        public string Comments { get; set; }
        public DateTime Date { get; set; }


    }

    public class WorflowkNavigationResponse
    {
        public RadioNavigate [] Navigation { get; set; }
        public IdName<int, string> IdName { get; set; }
    }
    public class WorkflowRequest
    {
        public int Id { get; set; }
        public string Comments { get; set; }
        public int WorkflowStateId { get; set; }
    }

    public class RadioNavigate: INotifyPropertyChanged
    {
        public int Id { get; set; }
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


}
