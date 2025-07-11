namespace WebDocMobile.Pages.Mobile
{
    public partial class EntitiesExtra : ContentPage
    {
        public EntitiesExtra()
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
