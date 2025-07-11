using WebDocMobile.PageModels.PagesViewModels;

namespace WebDocMobile.Pages.Mobile
{
    public partial class MessagesExtra : ContentPage
    {
        public MessagesExtra()
        {
            InitializeComponent();
            
            this.BindingContext = new MessageExtraViewModel(this.Navigation);

            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void HandleClickBackPage(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
