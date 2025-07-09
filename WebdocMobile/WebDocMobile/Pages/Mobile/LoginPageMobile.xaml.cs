using Microsoft.Maui.Controls.Internals;
using System.Diagnostics;
using WebDocMobile.PageModels;
using WebDocMobile.Services;
using static System.Net.Mime.MediaTypeNames;

namespace WebDocMobile.Pages.Mobile;

public partial class LoginPageMobile : ContentPage
{

     public LoginPageMobile(LoginPageViewModel viewModel)
    {
        InitializeComponent();

         this.BindingContext = viewModel;

        NavigationPage.SetHasNavigationBar(this, false);

        PasswordEntry.entry.IsPassword = true;

        Debug.WriteLine("Page: ", DomainEntry.Text);
    }

}