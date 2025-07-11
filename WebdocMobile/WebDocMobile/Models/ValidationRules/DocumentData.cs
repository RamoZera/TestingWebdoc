using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocMobile.Models.BookService;

namespace WebDocMobile.Models.ValidationRules
{
    public class DocumentData
    {
        private ObservableCollection<GetBook> test;

       
        [Required]
        [Display(Name = "Topic", GroupName = "Data")]
        private string topic;
        [Required]
        [Display(Name = "Book", GroupName = "Data")]
        public ObservableCollection<GetBook> Book
        {
            get
            {
                if (this.test == null)
                {
                    this.test = GetAllBook.GetBooks();
                    
                }
                return this.test;
            }
        }

        [Required]
        [Display(Name = "Type", GroupName = "Data")]
        public ObservableCollection<string> Type { get; set; } =
            [
                "INE",
                "AMBISIG",
                "Autoridade da Mobilidade e dos Transportes",
                "Agência Portuguesa do Ambiente, I.P",
                "Autoridade Marítima Nacional",
                "Direção Regional de Políticas Marítimas",
                "Direção-Geral de Estatísticas da Educação e Ciência",
                "Direção-Geral da Autoridade Marítima",
                "Porto dos Açores",
                "Turismo de Portugal, I.P.",
            ];
        [Required]
        [Display(Name = "Shipment", GroupName = "Data")]
        public ObservableCollection<string> Shipment { get; set; }
        [Required]
        [Display(Name = "Sorter", GroupName = "Data")]
        public ObservableCollection<string> Sorter { get; set; }

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
        public ObservableCollection<string> TypeE { get; set; }
        [Required]
        [Display(Name = "Entidade", GroupName = "Entidade")]
        public ObservableCollection<string> Entidade { get; set; }
        //priority atributes
        [Display(Name = "Urgency")]
        public bool? Urgency { get; set; }
        [Display(Name = "WaitResponse")]
        public bool? WaitResponse { get; set; }
        [Display(Name = "PaperSupport")]
        public bool? PaperSupport { get; set; }
        public GetBook BooksReal { get; set; }
        public string Topic { get => topic; set => topic = value; }
    }
}
