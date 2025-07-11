using WebDocMobile.CustomControls;
using WebDocMobile.PageModels.PagesViewModels;

namespace WebDocMobile.Pages.Mobile
{
    public partial class AplicationPageMobile : ContentPage
    {
        public AplicationPageMobile()
        {
            InitializeComponent();
            this.BindingContext = new AplicationViewModels(this.Navigation);

            NavigationPage.SetHasNavigationBar(this, false);

            var pageXAML = new Navbar();


            Label DashboardText = pageXAML.FindByName<Label>("DashboardText");
            DashboardText.IsVisible = false;
            Label UserText = pageXAML.FindByName<Label>("UserText");
            UserText.IsVisible = true;
            bar.Add((View)pageXAML);
            pageXAML.activeUser();
        }

        private void Tema_Toggled(object sender, ToggledEventArgs e)
        {
            if (Tema.IsToggled)
            {
                Application.Current.UserAppTheme = AppTheme.Dark;
                Principal.BackgroundColor = Colors.DarkGray;


            }
            else
            {
                Application.Current.UserAppTheme = AppTheme.Unspecified;
                Principal.BackgroundColor = Colors.LightGray;
            }
        }
    }
}
