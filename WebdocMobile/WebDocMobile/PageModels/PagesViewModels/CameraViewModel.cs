using CommunityToolkit.Mvvm.Input;
using Plugin.ValidationRules;
using Plugin.ValidationRules.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocMobile.Models.ValidationRules;

namespace WebDocMobile.PageModels.PagesViewModels
{
    public partial class CameraViewModel : ExtendedPropertyChanged
    {

        public string date { get; set; }
        private INavigation _navigationService;


        public CameraViewModel(INavigation navigation)
        {
            this._navigationService = navigation;
            date = DateTime.Now.ToString("dd MMMM yyyy", new CultureInfo("PT-pt"));

            Topic = Validator.Build<string>()
                            .WithRule(new IsNotNullOrEmptyRule<string>(), "A topic is required.");



        }
        public CameraViewModel()
        {
            Topic = Validator.Build<string>()
                           .WithRule(new IsNotNullOrEmptyRule<string>(), "O Assunto é obrigatório.");
            Book = Validator.Build<string>()
                           .WithRule(new IsNotNullOrEmptyRule<string>(), "O Livro é obrigatório.");
        }


        public Validatable<string> Topic { get; set; }
        public Validatable<string> Book { get; set; }

        [RelayCommand]
        public async Task GoBack()
        {
            await _navigationService.PopAsync();
        }


    }
}