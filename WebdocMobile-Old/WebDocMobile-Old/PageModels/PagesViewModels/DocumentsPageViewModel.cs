using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDocMobile.PageModels
{
    public class DocumentsPageViewModel
    {
        private INavigation _navigationService;
        public DocumentsPageViewModel(INavigation navigation)
        {
            this._navigationService = navigation;
        }
    }
}
