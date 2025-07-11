using System.Diagnostics;
using Telerik.Maui.Controls;
using WebDocMobile.Helpers;
using WebDocMobile.Models;
using WebDocMobile.Models.BookService;
using WebDocMobile.Models.NewDocument;
using WebDocMobile.Models.NewProcesses;
using WebDocMobile.PageModels.PagesViewModels;
using WebDocMobile.Services;

namespace WebDocMobile.Pages.Mobile
{
    public partial class FilepPage : ContentPage
    {
        private RadPopup saveDocumentPopup;
        NewDocumentPageViewModel _context;
        NewProcessPageViewModel _context2;
        private readonly byte[] barr;
        private DocumentResponse response;
        private bool _isDocument;
        private int responseId;
        IAlertService _alertService;
        public FilepPage(byte[] barr, bool isDocument)
        {
            _isDocument = isDocument;
            InitializeComponent();
            _alertService = new AlertService();
            _context = new NewDocumentPageViewModel(Navigation);
            BindingContext = _context;
            NavigationPage.SetHasNavigationBar(this, false);
            this.barr = barr;
            imgFoto.Source = ImageSource.FromStream(() => new MemoryStream(barr));
            if (_isDocument == true)
            {
                BookCar.IsVisible = true;


            }
            else
            {
                BookCar.IsVisible = false;
                TitleText.Text = "Process";
                BookCar.IsVisible = false;
                BookR.IsVisible = false;
                BookR.IsEnabled = false;
                BookError.IsEnabled = false;
                TypeDCar.IsEnabled = false;
                TypeDCar.IsVisible = false;
                Type.IsVisible = false;
                Type.IsEnabled = false;
                TypePCar.IsVisible = true;
                TypeProcess.IsVisible = true;
                TypeProcess.IsEnabled = true;
                TypeprocessError.IsVisible = true;
                _context2 = new NewProcessPageViewModel(Navigation);
                //BindingContext = _context2;
            }

            InitializeSaveDocumentPopup();
        }
        private async void ReturnToBack(object sender, EventArgs e) => await Navigation.PopModalAsync();

        private void InitializeSaveDocumentPopup()
        {
            saveDocumentPopup = new RadPopup();
            View content = (View)Resources["SaveDocumentPopup"];
            if (DeviceInfo.Idiom == DeviceIdiom.Phone)
                content.SetBinding(View.WidthRequestProperty, new Binding("Width", source: Application.Current.MainPage));
            saveDocumentPopup.Content = content;
            saveDocumentPopup.OutsideBackgroundColor = Color.FromArgb("#CC000000");
        }

        private void ShowSavePopup(object sender, EventArgs e)
        {
            try
            {
                if (_isDocument)
                {
                    NewDocumentRequest objDoc = new NewDocumentRequest
                    {
                        bookId = ((IdName<int, string>)BookR.SelectedItem).Id,
                        subject = Topic.Text,
                        TypeId = ((DocumentTypeDto)Type.SelectedItem).Id,
                        sendReceiveId = ((IdName<int, string>)SendR.SelectedItem).Id,
                        carimboDate = DataCarimbo.Date.Value,
                        reference = Reference.Text,
                        classifierId = ((IdName<int, string>)Classify.SelectedItem).Id,
                        Date = DataDocumento.Date.Value,
                        entityId = ((IdName<int, string>)Entity.SelectedItem).Id,
                        observations = Observations.Text,
                        physicalFile = true,
                        documentFile = Imagen.GetDataString(barr, null, false),
                        urgent = true,
                        responseDate = DataDocumento.Date.Value,
                        waitsResponse = false,
                        documentExtension = "png",
                        convertToPDF = true

                    };

                    if (objDoc != null)
                    {
                        response = _context.Insert(objDoc);
                        if (response != null)
                            saveDocumentPopup.IsOpen = true;

                    }
                }
                else
                {
                    NewProcessRequest objProcess = new NewProcessRequest
                    {
                        subject = Topic.Text,
                        typeId = ((DocumentTypeDto)TypeProcess.SelectedItem).Id,
                        reference = Reference.Text,
                        classifierId = ((IdName<int, string>)Classify.SelectedItem).Id,
                        date = DataDocumento.Date.Value,
                        entityId = ((IdName<int, string>)Entity.SelectedItem).Id,
                        observations = Observations.Text,
                        physicalFile = true,
                        documentFile = Imagen.GetDataString(barr, null, false),
                        urgent = true,
                        responseDate = DataDocumento.Date.Value,
                        waitsResponse = false,
                        documentExtension = "png",
                        convertToPDF = true
                    };
                    if (objProcess != null)
                    {
                        var response = _context2.Insert(objProcess);
                        if (response != null)
                        {
                            responseId = response.id;
                            saveDocumentPopup.IsOpen = true;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _alertService.ShowAlert("Erro", ex._GetExceptionMessage());
            }

        }

        private void CloseSavePopup(object sender, EventArgs e) => saveDocumentPopup.IsOpen = false;

        private async void PageConsultRegistration(object sender, EventArgs e)
        {
            try
            {
                var obj = new DocumentMetadataRequest
                {
                    id = response.id,
                    sizeRequest = new Sizerequest
                    {
                        height = 250,
                        width = 250,
                        keepAspectRatio = true
                    }

                };
                await Navigation.PushAsync(new ComprovativoPageMobile(obj, true));
                CloseSavePopup(sender, e);
            }
            catch (Exception ex)
            {
                //this.memoryStream.Dispose();
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e) => PageConsultRegistration(sender, e);

        //validation
        private void TopicEntry_Unfocused(object sender, FocusEventArgs e) => _context.Topic.Validate();

        private void BookR_Unfocused(object sender, FocusEventArgs e) => _context.BookR.Validate();

        private void Type_Unfocused(object sender, FocusEventArgs e) => _context.Type.Validate();

        private void SendR_Unfocused(object sender, FocusEventArgs e) => _context.SendR.Validate();

        private void Classify_Unfocused(object sender, FocusEventArgs e) => _context.Classifier.Validate();

        private void TypeProcess_Unfocused(object sender, FocusEventArgs e) => _context.ProcessesType.Validate();


        private async void TapGestureRecognizer_Tapped_1(object sender, TappedEventArgs e)
        {
            if (_isDocument)
                await Navigation.PushAsync(new NewDocumentPageMobile());
            else
                await Navigation.PushAsync(new NewProcessPageMobile());

        }
        //private void Button_Clicked(object sender, EventArgs e)
        //{
        //    search.IsVisible = true;
            
        //}

        //private void Button_Clicked_1(object sender, EventArgs e)
        //{
        //    Entity.ItemsSource = _context.GetEntities(search.Text);
        //    Entity.DisplayMemberPath = "Name";
        //}
    }
}


