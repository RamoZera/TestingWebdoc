using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDocMobile.PageModels
{
    public class ProcessesPageViewModel
    {
        private INavigation _navigationService;
        public ProcessesPageViewModel(INavigation navigation) 
        {
            this._navigationService = navigation;
        }
    }
}
