//-----------------------------------------------------------------------
// <copyright file="NewDocumentPageMobile.xaml.cs" company="Home">
//     Author: Horus Ra
//     Copyright (c) Home. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.ObjectModel;
using System.IO;
using Telerik.Maui.Controls;
using WebDocMobile.Models;
using WebDocMobile.Models.BookService;
using WebDocMobile.PageModels.PagesViewModels;

namespace WebDocMobile.Pages.Mobile;

public partial class NewDocumentPageMobile : ContentPage
{
    private RadPopup newDocumentPopup;
    private RadPopup saveDocumentPopup;
    private RadPopup cancelDocumentPopup;
    NewDocumentPageViewModel _context;

    public NewDocumentPageMobile()
    {
        //TelerikLocalizationManager.Manager.ResourceManager = MaskResource.ResourceManager;
        InitializeComponent();
        _context = new NewDocumentPageViewModel(Navigation);
        BindingContext = _context;
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeNewDocumentPopup();
        InitializeSaveDocumentPopup();
        InitializeCancelDocumentPopup();
    }

    public async Task<FileResult> PickAndShowButton(PickOptions options, object sender, EventArgs e)
    {
        try
        {
            FileResult result = await FilePicker.Default.PickAsync(options);
            if (result != null)
            {
                using Stream stream = await result.OpenReadAsync();
                ImageSource image = ImageSource.FromStream(() => stream);
                byte[] barr = new byte[stream.Length];
                stream.Read(barr, 0, barr.Length);
                PageFile(sender, e, barr);

                return result;
            }
        }
        catch (Exception ex)
        {
            // The user canceled or something went wrong
        }
        return null;
    }

    public ObservableCollection<string> Items { get; set; }

    public async void uploadButtonClick(object sender, EventArgs e)
    {
        FilePickerFileType customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>> { { DevicePlatform.iOS, new[] { "public.my.comic.extension" } }, // UTType values
                    { DevicePlatform.Android, new[] { "application/comics" } }, // MIME type
                    { DevicePlatform.WinUI, new[] { ".cbr", ".cbz" } }, // file extension
                    { DevicePlatform.Tizen, new[] { "*/*" } }, { DevicePlatform.macOS, new[] { "cbr", "cbz" } }, // UTType values
                });
        PickOptions options = new PickOptions();
        await PickAndShowButton(options, sender, e);
    }

    #region Popup
    private void InitializeNewDocumentPopup()
    {
        newDocumentPopup = new RadPopup();
        View content = (View)Resources["NewDocumentPopup"];
        if (DeviceInfo.Idiom == DeviceIdiom.Phone)
            content.SetBinding(View.WidthRequestProperty, new Binding("Width", source: Application.Current.MainPage));
        newDocumentPopup.Content = content;
        newDocumentPopup.OutsideBackgroundColor = Color.FromArgb("#CC000000");
    }

    private void InitializeSaveDocumentPopup()
    {
        saveDocumentPopup = new RadPopup();
        View content = (View)Resources["SaveDocumentPopup"];
        if (DeviceInfo.Idiom == DeviceIdiom.Phone)
            content.SetBinding(View.WidthRequestProperty, new Binding("Width", source: Application.Current.MainPage));
        saveDocumentPopup.Content = content;
        saveDocumentPopup.OutsideBackgroundColor = Color.FromArgb("#CC000000");
    }

    private void InitializeCancelDocumentPopup()
    {
        cancelDocumentPopup = new RadPopup();
        View content = (View)Resources["CancelDocumentPopup"];
        if (DeviceInfo.Idiom == DeviceIdiom.Phone)
            content.SetBinding(View.WidthRequestProperty, new Binding("Width", source: Application.Current.MainPage));
        cancelDocumentPopup.Content = content;
        cancelDocumentPopup.OutsideBackgroundColor = Color.FromArgb("#CC000000");
    }

    private void ShowPopup(object sender, EventArgs e)
    {
        newDocumentPopup.IsOpen = true;
        if (DeviceInfo.Idiom == DeviceIdiom.Desktop)
            newDocumentPopup.Placement = PlacementMode.Center;
    }

    private void ClosePopup(object sender, EventArgs e) => newDocumentPopup.IsOpen = false;

    private void ShowSavePopup(object sender, EventArgs e)
    {
        saveDocumentPopup.IsOpen = true;
        if (DeviceInfo.Idiom == DeviceIdiom.Desktop)
            saveDocumentPopup.Placement = PlacementMode.Center;
    }

    private void CloseSavePopup(object sender, EventArgs e) => saveDocumentPopup.IsOpen = false;

    private void ShowCancelPopup(object sender, EventArgs e)
    {
        cancelDocumentPopup.IsOpen = true;
        if (DeviceInfo.Idiom == DeviceIdiom.Desktop)
            cancelDocumentPopup.Placement = PlacementMode.Center;
    }

    private void CloseCancelPopup(object sender, EventArgs e) => cancelDocumentPopup.IsOpen = false;

    private void ConfirmCancelPopup(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
    #endregion

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e) => uploadButtonClick(sender, e);

    #region for the Camera
    public async void TakePhoto(object sender, TappedEventArgs e)
    {
        if (MediaPicker.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.CapturePhotoAsync();
            if (photo != null)
                using (Stream memoryStream = await photo.OpenReadAsync())
                {
                    byte[] barr = new byte[memoryStream.Length];
                    memoryStream.Read(barr, 0, barr.Length);
                    PageCamera(sender, e, barr);
                }
        }
    }

    private void TapGestureRecognizerCamera_Tapped(object sender, TappedEventArgs e) => TakePhoto(sender, e);
    #endregion

    private void TapGestureRecognizerCancel_Tapped(object sender, TappedEventArgs e) => CloseCancelPopup(sender, e);

    private async void PageCamera(object sender, EventArgs e, byte[] barr)
    {
        await Navigation.PushModalAsync(new Camera(barr, true));
        ClosePopup(sender, e);
    }
    private async void PageFile(object sender, EventArgs e, byte[] barr)
    {
        await Navigation.PushModalAsync(new FilepPage(barr, true));
        ClosePopup(sender, e);
    }
    private async void ReturnToBack(object sender, EventArgs e) => await Navigation.PopAsync();

    //validation
    private void TopicEntry_Unfocused(object sender, FocusEventArgs e) => _context.Topic.Validate();

    private void BookR_Unfocused(object sender, FocusEventArgs e) => _context.BookR.Validate();

    private void Type_Unfocused(object sender, FocusEventArgs e) => _context.Type.Validate();

    private void SendR_Unfocused(object sender, FocusEventArgs e) => _context.SendR.Validate();

    private void Classify_Unfocused(object sender, FocusEventArgs e) => _context.Classifier.Validate();


}
