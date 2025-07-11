using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Services;
using System.Text.Json.Serialization;
using WebDocMobile.Models;
using System.Text.Json;
using WebDocMobile.Helpers;

namespace WebDocMobile.PageModels
{
    public partial class FirstPageViewModel
    {
        private AlertService _alertService;
        private INavigation _navigationService;
        public FirstPageViewModel(INavigation navigation)
        {
            this._navigationService = navigation;
            this._alertService = new AlertService();
        }
    }
}
