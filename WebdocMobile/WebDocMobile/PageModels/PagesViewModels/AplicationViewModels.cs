using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace WebDocMobile.PageModels.PagesViewModels
{
    public class AplicationViewModels
    {
        public string Date { get; set; }
        public string Username { get; set; }
        private INavigation _navigationService;
        public AplicationViewModels(INavigation navigation)
        {
            this._navigationService = navigation;
            Date = DateTime.Now.ToString("dd MMMM yyyy", new CultureInfo("PT-pt"));
            Username = !string.IsNullOrWhiteSpace(App.UserDetails.StrName) ? App.UserDetails.StrName:"userName";
        }
    }
}
