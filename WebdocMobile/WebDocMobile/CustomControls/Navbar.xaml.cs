using Telerik.Maui.Controls;
using WebDocMobile.PageModels;
using WebDocMobile.PageModels.PagesViewModels;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Services;

namespace WebDocMobile.CustomControls;

public partial class Navbar : RadBorder
{

    private RadPopup NewRegisterPopup;
    private RadPopup ConsultRegisterPopup;
    private RadPopup UserInfoPopup;
    private RadPopup LoaderPopup;
    private readonly IAlertService _alertService;
    private string icon_dashboard;

    public Navbar()
    {
        InitializeComponent();

        this.BindingContext = new NavbarViewModel(this.Navigation);
        NavigationPage.SetHasNavigationBar(this, false);
        _alertService = new AlertService();
        InitializeNewRegisterPopup();
        InitializeLoaderPopup();
        InitializeConsultRegisterPopup();
        InitializeUserInfoPopup();
        IconDashboard.Source = "icon_dashboard";
    }

    private void InitializeNewRegisterPopup()
    {
        this.NewRegisterPopup = new RadPopup();
        var content = (View)this.Resources["NewRegisterPopup"];
        if (DeviceInfo.Idiom == DeviceIdiom.Phone)
        {
            content.SetBinding(View.WidthRequestProperty, new Binding("Width", source: Application.Current.MainPage));
        }

        this.NewRegisterPopup.Content = content;
        this.NewRegisterPopup.OutsideBackgroundColor = Color.FromArgb("#CC000000");
    }

    private void InitializeConsultRegisterPopup()
    {
        this.ConsultRegisterPopup = new RadPopup();
        var content = (View)this.Resources["ConsultRegisterPopup"];
        if (DeviceInfo.Idiom == DeviceIdiom.Phone)
        {
            content.SetBinding(View.WidthRequestProperty, new Binding("Width", source: Application.Current.MainPage));
        }

        this.ConsultRegisterPopup.Content = content;
        this.ConsultRegisterPopup.OutsideBackgroundColor = Color.FromArgb("#CC000000");

    }

    private void InitializeLoaderPopup()
    {
        this.LoaderPopup = new RadPopup();
        var content = (View)this.Resources["LoaderPopup"];
        if (DeviceInfo.Idiom == DeviceIdiom.Phone)
        {
            content.SetBinding(View.WidthRequestProperty, new Binding("Width", source: Application.Current.MainPage));
        }

        this.LoaderPopup.Content = content;
        this.LoaderPopup.OutsideBackgroundColor = Color.FromArgb("#CC000000");
    }

    private void InitializeUserInfoPopup()
    {
        this.UserInfoPopup = new RadPopup();
        var content = (View)this.Resources["UserInfoPopup"];
        if (DeviceInfo.Idiom == DeviceIdiom.Phone)
        {
            content.SetBinding(View.WidthRequestProperty, new Binding("Width", source: Application.Current.MainPage));
        }

        this.UserInfoPopup.Content = content;
        this.UserInfoPopup.OutsideBackgroundColor = Color.FromArgb("#CC000000");
    }

    private void Open_NewRegisterPopup(object sender, System.EventArgs e)
    {
        this.NewRegisterPopup.IsOpen = true;

        if (DeviceInfo.Idiom == DeviceIdiom.Desktop)
        {
            this.NewRegisterPopup.Placement = PlacementMode.Center;
        }
    }
    private void Close_NewRegisterPopup(object sender, System.EventArgs e)
    {
        this.NewRegisterPopup.IsOpen = false;
        this.LoaderPopup.IsOpen = true;
    }

    private void Open_ConsultRegisterPopup(object sender, System.EventArgs e)
    {
        this.ConsultRegisterPopup.IsOpen = true;

        if (DeviceInfo.Idiom == DeviceIdiom.Desktop)
        {
            this.ConsultRegisterPopup.Placement = PlacementMode.Center;
        }
    }
    private void Close_ConsultRegisterPopup(object sender, System.EventArgs e)
    {
        this.ConsultRegisterPopup.IsOpen = false;
    }

    private async void CreateDocumentButton(object sender, EventArgs e)
    {
        this.NewRegisterPopup.IsOpen = false;
        this.LoaderPopup.IsOpen = true;
        await Task.Delay(1000);
        await Navigation.PushAsync(new NewDocumentPageMobile());
        this.LoaderPopup.IsOpen = false;
    }

    private async void CreateProcessButton(object sender, EventArgs e)
    {
        this.NewRegisterPopup.IsOpen = false;
        this.LoaderPopup.IsOpen = true;
        await Task.Delay(1000);
        await Navigation.PushAsync(new NewProcessPageMobile());
        this.LoaderPopup.IsOpen = false;
    }



    private async void Open_UserInfoPopup(object sender, System.EventArgs e)
    {
        this.LoaderPopup.IsOpen = true;
        await Task.Delay(1000);
        await Navigation.PushAsync(new AplicationPageMobile());
        this.LoaderPopup.IsOpen = false;
    }
    private void Close_UserInfoPopup(object sender, System.EventArgs e)
    {
        this.UserInfoPopup.IsOpen = false;
    }

    private async void Open_PageAtividade(object sender, EventArgs e)
    {
        this.LoaderPopup.IsOpen = true;
        await Task.Delay(1000);
        await Navigation.PushAsync(new ActivitiesPage());
        this.LoaderPopup.IsOpen = false;
    }

    private async void ConsultRegisterClickDocument(object sender, EventArgs e)
    {
        this.ConsultRegisterPopup.IsOpen = false;
        this.LoaderPopup.IsOpen = true;
        await Task.Delay(1000);
        await Navigation.PushAsync(new NewDocumentPageMobile());
        this.LoaderPopup.IsOpen = false;
    }

    private async void ConsultRegisterClickProceses(object sender, EventArgs e)
    {
        this.ConsultRegisterPopup.IsOpen = false;
        this.LoaderPopup.IsOpen = true;
        await Task.Delay(1000);
        await Navigation.PushAsync(new ListProceses());
        this.LoaderPopup.IsOpen = false;
    }

    public void active()
    {
        ConsulRegisterText.IsVisible = true;
        DashboardText.IsVisible = false;
        UserText.IsVisible = false;
        ButtonRegister.BackgroundColor = Color.FromArgb("#F1F1F1");
        ButtonDashboar.BackgroundColor = Colors.Transparent;
        ButtonUser.BackgroundColor = Colors.Transparent;
        IconDashboard.Source = "icon_dashboard_white";
        IconUser.Source = "icon_user";
        IconRegistos.Source = "icon_registos_blue";
    }
    public void activeUser()
    {
        UserText.IsVisible = true;
        ConsulRegisterText.IsVisible = false;
        DashboardText.IsVisible = false;
        ButtonUser.BackgroundColor = Color.FromArgb("#F1F1F1");
        ButtonRegister.BackgroundColor = Colors.Transparent;
        ButtonDashboar.BackgroundColor = Colors.Transparent;
        IconDashboard.Source = "icon_dashboard_white";
        IconRegistos.Source = "icon_registos";
        IconUser.Source = "icon_user_blue";
    }


    private async void ButtonDashboar_Clicked(object sender, EventArgs e)
    {
        this.LoaderPopup.IsOpen = true;
        await Task.Delay(1000);
        await Navigation.PushAsync(new MainMenuPageMobile());
        ConsulRegisterText.IsVisible = false;
        DashboardText.IsVisible = true;
        IconDashboard.Source = "icon_dashboard";
        ButtonRegister.BackgroundColor = Colors.Transparent;
        ButtonUser.BackgroundColor = Colors.Transparent;
        ButtonDashboar.BackgroundColor = Color.FromArgb("#F1F1F1");
        IconRegistos.Source = "icon_registos";
        IconUser.Source = "icon_user";
        this.LoaderPopup.IsOpen = false;
    }


}