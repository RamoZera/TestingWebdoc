using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocMobile.Models.Extras;

namespace WebDocMobile.PageModels.PagesViewModels
{
    public partial class MessageExtraViewModel:ObservableObject
    {
        public string date { get; set; }
        private INavigation _navigationService;

        public ObservableCollection<MessageExtra> Unread { get; set; }
        public ObservableCollection<MessageExtra> Others { get; set; }

        public MessageExtraViewModel(INavigation navigation)
        {
            this._navigationService = navigation;
            date = DateTime.Now.ToString("dd MMMM yyyy", new CultureInfo("PT-pt"));
            Unread = new ObservableCollection<MessageExtra>();
            Others = new ObservableCollection<MessageExtra>();
            foreach (var d in GetMessages().ToList())
            {
                int day = d.Hour.Day;
                int month = d.Hour.Month;
                int year = d.Hour.Year;

                int hour = d.Hour.Hour;
                int minute = d.Hour.Minute;

                // Format day with leading zero if less than 10
                string day_formatted = day < 10 ? $"0{day}" : day.ToString();
                string month_formatted = day < 10 ? $"0{month}" : month.ToString();
                string hour_formatted = hour < 10 ? $"0{hour}" : hour.ToString();
                string minute_formatted = minute < 10 ? $"0{minute}" : minute.ToString();
                // Create a new DateTime object with the formatted date
                //d.dateString = $"{day_formatted}/{month_formatted}/{year} {hour_formatted}:{minute_formatted}";
                d.dateString = $"{hour_formatted}:{minute_formatted}";
                if (d.Status.Equals("Unread"))
                    Unread.Add(d);
              else
                Others.Add(d);
            }
           
          

        }
        public MessageExtraViewModel()
        {
            
            date = DateTime.Now.ToString("dd MMMM yyyy", new CultureInfo("PT-pt"));
            Unread = new ObservableCollection<MessageExtra>();
            Others = new ObservableCollection<MessageExtra>();
            foreach (var d in GetMessages().ToList())
            {
                int day = d.Hour.Day;
                int month = d.Hour.Month;
                int year = d.Hour.Year;

                int hour = d.Hour.Hour;
                int minute = d.Hour.Minute;

                // Format day with leading zero if less than 10
                string day_formatted = day < 10 ? $"0{day}" : day.ToString();
                string month_formatted = day < 10 ? $"0{month}" : month.ToString();
                string hour_formatted = hour < 10 ? $"0{hour}" : hour.ToString();
                string minute_formatted = minute < 10 ? $"0{minute}" : minute.ToString();
                // Create a new DateTime object with the formatted date
                //d.dateString = $"{day_formatted}/{month_formatted}/{year} {hour_formatted}:{minute_formatted}";
                d.dateString = $"{hour_formatted}:{minute_formatted}";
                if (d.Status.Equals("Unread"))
                    Unread.Add(d);
                else
                    Others.Add(d);
            }



        }


        private ObservableCollection<MessageExtra> GetMessages()
        {


            var data = new ObservableCollection<MessageExtra>
        {
            new MessageExtra { Name="Joana Mendes",Description="Remeta o registo para o departamento de jurídico ...",Hour= DateTime.Parse("2024-07-08T17:08:27.917") ,Status ="Unread"},
            new MessageExtra { Name="Joaquim Santos",Description="Não tem o documento físico acessível ao grupo de...",Hour= DateTime.Parse("2024-06-05T17:09:27.917") ,Status="Unread"},
            new MessageExtra { Name="André Ribeiro",Description="Lorem ipsum dolor sit amet, consetetur sadipscing...",Hour= DateTime.Parse("2024-07-27T17:04:27.917") ,Status="Read" },
            new MessageExtra { Name="Miriam Jesus",Description="Lorem ipsum dolor sit amet, consetetur sadipscing...",Hour= DateTime.Parse("2024-07-27T17:04:27.917") ,Status="Read" },


        };
            return data;
        }
    }
}
