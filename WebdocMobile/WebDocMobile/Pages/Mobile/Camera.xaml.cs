//-----------------------------------------------------------------------
// <copyright file="Camera.xaml.cs" company="Home">
//     Author: Horus Ra
//     Copyright (c) Home. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Telerik.Maui.Controls;
using WebDocMobile.Models.BookService;
using WebDocMobile.PageModels.PagesViewModels;
using WebDocMobile.Models.NewDocument;
using WebDocMobile.Models;
using System.Diagnostics;
using WebDocMobile.Helpers;
using WebDocMobile.Models.NewProcesses;
using System.Threading.Tasks;
using WebDocMobile.Services;

namespace WebDocMobile.Pages.Mobile
{
    public partial class Camera : ContentPage
    {
        private readonly IAlertService _alertService;
        private RadPopup saveDocumentPopup;
        NewDocumentPageViewModel _context;
        NewProcessPageViewModel _context2;
        private readonly byte[] barr;
        private int responseId;
        private bool _isDocument;
        public Camera(byte[] barr, bool isDocument)
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
            this.barr = barr;
            _isDocument = isDocument;
            imgFoto.Source = ImageSource.FromStream(() => new MemoryStream(barr));
            _context = new NewDocumentPageViewModel(Navigation);
            BindingContext = _context;
            _alertService = new AlertService();
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
                var validationErrors = new List<string>();

                if (string.IsNullOrWhiteSpace(Topic.Text)) validationErrors.Add("O Assunto é obrigatório.");
                if (BookR.SelectedItem == null) validationErrors.Add("O Livro é obrigatório.");
                if (Type.SelectedItem == null) validationErrors.Add("O Tipo é obrigatório.");
                if (SendR.SelectedItem == null) validationErrors.Add("O Envio/Receção é obrigatório.");
                if (Classify.SelectedItem == null) validationErrors.Add("O Classificador é obrigatório.");
                if (Entity.SelectedItem == null) validationErrors.Add("A Entidade é obrigatória.");

                if (validationErrors.Any())
                {
                    string errorMessage = string.Join("\n", validationErrors);
                    _alertService.ShowAlert("Erro de Validação", errorMessage);
                    return;
                }


                if (_isDocument)
                {
                    NewDocumentRequest objDoc = new NewDocumentRequest
                    {
                        bookId = ((IdName<int, string>)BookR.SelectedItem).Id,
                        subject = Topic.Text,
                        TypeId = ((DocumentTypeDto)Type.SelectedItem).Id,
                        sendReceiveId = ((IdName<int, string>)SendR.SelectedItem).Id,
                        carimboDate = DataCarimbo.Date.HasValue ? DataCarimbo.Date.Value : (DateTime?)null,
                        reference = Reference.Text,
                        classifierId = ((IdName<int, string>)Classify.SelectedItem).Id,
                        Date = DataDocumento.Date.HasValue ? DataDocumento.Date.Value : (DateTime?)null,
                        entityId = ((IdName<int, string>)Entity.SelectedItem).Id,
                        observations = Observations.Text,
                        physicalFile = SuportePapel.IsToggled,
                        documentFile = Imagen.GetDataString(barr, null, false),
                        urgent = Urgente.IsToggled,
                        responseDate = Datalimit.Date.HasValue ? Datalimit.Date.Value : (DateTime?)null,
                        waitsResponse = AguardaResposta.IsToggled,
                        documentExtension = "png",
                        Confidential = Confidencial.IsToggled,
                        InTreatment = EmTratamento.IsToggled,
                        DocumentNoResponse = DocumentoSemResposta.IsToggled,
                        convertToPDF = true

                    };
                    if (objDoc != null)
                    {
                        var response = _context.Insert(objDoc);
                        if (response != null)
                        {
                            responseId = response.id;
                            saveDocumentPopup.IsOpen = true;
                           
                        }


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
                Debug.WriteLine(ex.Message);
            }

        }

        private void CloseSavePopup(object sender, EventArgs e) => saveDocumentPopup.IsOpen = false;

        private async void PageConsultRegistration(object sender, EventArgs e)
        {
            try
            {
                var obj = new DocumentMetadataRequest
                {
                    id = responseId,
                    sizeRequest = new Sizerequest
                    {
                        height = 250,
                        width = 250,
                        keepAspectRatio = true
                    }

                };
                await Navigation.PushModalAsync(new ComprovativoPageMobile(obj, _isDocument));
                CloseSavePopup(sender, e);
            }
            catch (Exception ex)
            {

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

     
        //private void Button_Clicked_1(object sender, EventArgs e)
        //{
        //    Entity.ItemsSource = _context.GetEntities(search.Text);
        //    Entity.DisplayMemberPath = "Name";
        //}
    }
}
