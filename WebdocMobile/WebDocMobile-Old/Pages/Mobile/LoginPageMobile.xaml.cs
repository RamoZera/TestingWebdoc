using Microsoft.Maui.Controls.Internals;
using System.Diagnostics;
using System.Formats.Tar;
using WebDocMobile.PageModels;
using WebDocMobile.Services;
using static System.Net.Mime.MediaTypeNames;

namespace WebDocMobile.Pages.Mobile;

public partial class LoginPageMobile : ContentPage
{

    public LoginPageMobile(string codEntidade)
    {
        InitializeComponent();

        this.BindingContext = new LoginPageViewModel(this.Navigation, codEntidade);

        NavigationPage.SetHasNavigationBar(this, false);

        PasswordEntry.entry.IsPassword = true;

        Debug.WriteLine("Page: ", DomainEntry.Text);
    }

}