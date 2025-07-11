using Microsoft.Maui.Controls.Internals;
using System.Diagnostics;
using System.Formats.Tar;
using WebDocMobile.Models;
using WebDocMobile.PageModels;
using WebDocMobile.Services;
using static System.Net.Mime.MediaTypeNames;
using static WebDocMobile.Models.UserBasicInfo;

namespace WebDocMobile.Pages.Mobile;

public partial class LoginPageMobile: ContentPage
{
    LoginPageViewModel _context;
    private string entity;
    public LoginPageMobile(string codEntidade)
    {
        InitializeComponent();
        _context = new LoginPageViewModel(this.Navigation, codEntidade);
        this.BindingContext = _context;
        NavigationPage.SetHasNavigationBar(this, false);
        URLIDName si = _context.EntitiesLogin.FirstOrDefault(item => item.Id == codEntidade);
        if(si != null)
        {
            EntityEntry.SelectedItem = si;
            SelectEntity(si);
        }
    }

    private void OnEntryFocused(object sender, FocusEventArgs e)
    {
        PasswordVisibilityButton.IsVisible = true;
    }

    private void OnEntryUnfocused(object sender, FocusEventArgs e)
    {
        PasswordVisibilityButton.IsVisible = false;
    }

    private void OnPasswordToggleClicked(object sender, EventArgs e)
    {
        PasswordEntry.Focus();

        #if ANDROID
            // Garantir que o teclado permaneça aberto após restaurar o foco
            var activity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
            var mainActivity = activity as MainActivity;
            mainActivity?.ShowKeyboard(PasswordEntry.Handler.PlatformView as Android.Views.View);
        #endif

    }


    private void EntityEntry_SelectionChanged(object sender, Telerik.Maui.Controls.ComboBoxSelectionChangedEventArgs e) => SelectEntity(EntityEntry.SelectedIndex != -1 ? (URLIDName)EntityEntry.SelectedItem : null);

    private void SelectEntity(URLIDName si)
    {
        if(si == null)
        {
            App.BaseAddress = null;
            _context.codEntidade = null;
        }
        else
        {
            App.BaseAddress = si.URL;
            _context.codEntidade = si.Id;
        }
    }
}