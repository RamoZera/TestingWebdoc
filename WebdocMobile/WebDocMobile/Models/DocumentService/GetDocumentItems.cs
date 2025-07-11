using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.Common.DataAnnotations;


namespace WebDocMobile.Models.DocumentService
{
    public class GetDocumentItems : NotifyPropertyChangedBase
    {
        private ObservableCollection<GetBook> book;

        [Required]
        [Display(Name = "Topic", GroupName = "Data")]
        [DataType(DataType.Text)]
        public string Topic { get; set; }
        [Required]
        [Display(Name = "Book", GroupName = "Data")]
        public ObservableCollection<GetBook> Book
        {
            get
            {
                return this.book;
            }
            set
            {
                if (this.book != value)
                {
                    this.book = value;
                    this.OnPropertyChanged();
                }
            }
        }

        [Required]
        [Display(Name = "Type", GroupName = "Data")]
        public string Type { get; set; }
        [Required]
        [Display(Name = "Shipment", GroupName = "Data")]
        public string Shipment { get; set; }
        [Required]
        [Display(Name = "Sorter", GroupName = "Data")]
        public string Sorter { get; set; }

        [Display(Name = "DateDocument", GroupName = "Data")]
        [DataType(DataType.DateTime)]
        public DateTime? DateDocument { get; set; }
        [Display(Name = "EndDate", GroupName = "Data")]
        [DataType(DataType.DateTime)]
        public DateTime? EndDate { get; set; }
        [Display(Name = "DateResponseLimit", GroupName = "Data")]
        [DataType(DataType.DateTime)]
        public DateTime? DateResponseLimit { get; set; }
        [Display(Name = "Reference", GroupName = "Data")]
        public string Reference { get; set; }
        [Display(Name = "Observations", GroupName = "Data")]
        [DataType(DataType.MultilineText)]
        public string Observations { get; set; }
        //atributtes for Entidade        
        [Display(Name = "TypeE", GroupName = "Entidade")]
        public string TypeE { get; set; }
        [Required]
        [Display(Name = "Entidade", GroupName = "Entidade")]
        public string Entidade { get; set; }
        //priority atributes
        [Display(Name = "Urgency")]
        public bool? Urgency { get; set; }
        [Display(Name = "WaitResponse")]
        public bool? WaitResponse { get; set; }
        [Display(Name = "PaperSupport")]
        public bool? PaperSupport { get; set; }

       

    }
}
