namespace WebDocMobile.Pages.Mobile
{
    public partial class SecurityExtra : ContentPage
    {
        public SecurityExtra()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void HandleClickBackPage(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
