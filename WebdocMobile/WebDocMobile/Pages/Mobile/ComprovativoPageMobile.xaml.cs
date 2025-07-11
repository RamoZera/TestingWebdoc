using CommunityToolkit.Mvvm.Input;
using Telerik.Maui.Controls;
using WebDocMobile.Models.NewDocument;
using WebDocMobile.Models.NewProcesses;
using WebDocMobile.PageModels.PagesViewModels;
using WebDocMobile.Services;
namespace WebDocMobile.Pages.Mobile;


public partial class ComprovativoPageMobile : ContentPage
{
    ComprovativoPageViewModel _context;
    private readonly byte[] barr;
    public DocumentMetadataResponse obj { get; set; }
    public ProcessMetadataResponse objP { get; set; }
    public DocumentUpdateCheckOut objUpdateCheckOut { get; set; }
    private DocumentMetadataRequest _metadata;
    private bool _isDocument;

    private RadPopup OptionsDocumentPopup;

    public ComprovativoPageMobile(DocumentMetadataRequest metadata, bool isDocument)
    {
        InitializeComponent();
        InitializeOptionsDocumentPopup();
        _context = new ComprovativoPageViewModel(Navigation);
        BindingContext = _context;
        NavigationPage.SetHasNavigationBar(this, false);
        this._metadata = metadata;
        _isDocument = isDocument;
        if (isDocument == true)
        {
            obj = _context.GetMetadata(metadata);
            NumberText.Text = obj.number;
            ToolTipProperties.SetText(NumberText, obj.number);
            Assunto.Text = obj.subject;
            this.barr = Convert.FromBase64String(obj.image);
            if (barr.Any())
                imgPhotoC.Source = ImageSource.FromStream(() => new MemoryStream(barr));
            BookR.Text = obj.book;
            Type.Text = obj.type;
            SendR.Text = obj.sendReceive;
            Classify.Text = obj.classifier;
            Reference.Text = obj.reference;
            Observations.Text = obj.observations;
            DataCarimbo.Date = obj.carimboDate;
            DataDocumento.Date = obj.date;
            DataReference.Date = obj.responseDate;
            Entity.Text = obj.entity;
            AguardaResposta.IsToggled = obj.waitsResponse;
            Urgente.IsToggled = obj.urgent;
            Confidencial.IsToggled = obj.Confidential;
            EmTratamento.IsToggled = obj.InTreatment;
            DocumentoSemResposta.IsToggled = obj.DocumentNoResponse;
            SuportePapel.IsToggled = obj.physicalFile;
            EnableControls(obj.CheckOut);
        }
        else
        {
            objP = _context.GetProcessMetadata(metadata);
            BookR.IsEnabled = false;
            BookR.IsVisible = false;
            BookCard.IsVisible = false;
            this.barr = Convert.FromBase64String(objP.image);
            if (barr.Any())
                imgPhotoC.Source = ImageSource.FromStream(() => new MemoryStream(barr));
            NumberText.Text = objP.number;
            Reference.Text = objP.reference;
            Observations.Text = objP.observations;
            Classify.Text = objP.classifier;
            SendRCard.IsVisible = false;
            SendR.IsEnabled = false;
            SendR.IsVisible = false;
            DataCarimbo.Date = objP.responseDate;
            DataDocumento.Date = objP.date;
            DataReference.Date = objP.responseDate;
            Type.Text = objP.type;
        }

    }

    private void TopicEntry_Unfocused(object sender, FocusEventArgs e) => _context.Topic.Validate();

    private void BookR_Unfocused(object sender, FocusEventArgs e) => _context.BookR.Validate();

    private void Type_Unfocused(object sender, FocusEventArgs e) => _context.Type.Validate();

    private void SendR_Unfocused(object sender, FocusEventArgs e) => _context.SendR.Validate();

    private void Classify_Unfocused(object sender, FocusEventArgs e) => _context.Classifier.Validate();

    private async void Button_Clicked(object sender, EventArgs e)
    {
        ModalLoader.IsVisible = true;
        await Task.Delay(100);
        await Navigation.PushAsync(new MoreOperationsPageMobile(_metadata, _isDocument));
        ModalLoader.IsVisible = false;
    }

    private async void ReturnButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
    private void ButtonDownload_Clicked(object sender, EventArgs e)
    {
        if (_isDocument)
            _context.GetDocumentFile(_metadata.id);
        else
            _context.GetProcessFile(_metadata.id);

    }

    public void EnableControls(bool CheckOut)
    {
        BookR.IsEnabled = CheckOut;
        Type.IsEnabled = CheckOut;
        SendR.IsEnabled = CheckOut;
        Classify.IsEnabled = CheckOut;
        Reference.IsEnabled = CheckOut;
        Observations.IsEnabled = CheckOut;
        DataCarimbo.IsEnabled  = CheckOut;
        DataDocumento.IsEnabled = CheckOut;
        DataReference.IsEnabled = CheckOut;
        Entity.IsEnabled = CheckOut;
        AguardaResposta.IsEnabled = CheckOut;
        Urgente.IsEnabled = CheckOut;
        Confidencial.IsEnabled = CheckOut;
        EmTratamento.IsEnabled = CheckOut;
        DocumentoSemResposta.IsEnabled = CheckOut;
        SuportePapel.IsEnabled = CheckOut;
        CheckOutIn.IsToggled = CheckOut;
        OpcoesImage.IsEnabled = CheckOut;

        if (CheckOutIn.IsToggled)
        {

            SwitchFrame.BackgroundColor = Color.FromHex("#C8003D");

            SwitchThumb.Margin = new Thickness(0, 0, 5, 0);
            SwitchThumb.HorizontalOptions = LayoutOptions.End;

            FirstImage.IsVisible = false;
            SecondImage.IsVisible = true;

            SecondImage.Opacity = 1; 
        }
        else
        {
            SwitchFrame.BackgroundColor = Color.FromHex("#0D7655");

            SwitchThumb.Margin = new Thickness(5, 0, 0, 0);
            SwitchThumb.HorizontalOptions = LayoutOptions.Start;

            FirstImage.IsVisible = true;
            SecondImage.IsVisible = false;

            FirstImage.Opacity = 1; 
        }

    }

    private async void OnFrameTapped(object sender, EventArgs e)
    {
        bool CheckUpdate;
        if (!CheckOutIn.IsToggled)
        {
            CheckOutIn.IsToggled = true;
            CheckUpdate = CheckOutCheckInAction();
            if (!CheckUpdate)
            {
                return;
            }
            SwitchFrame.BackgroundColor = Color.FromHex("#C8003D");

            SwitchThumb.Margin = new Thickness(0, 0, 5, 0);
            SwitchThumb.HorizontalOptions = LayoutOptions.End;

            FirstImage.IsVisible = false;
            SecondImage.IsVisible = true;

            SecondImage.Opacity = 0; 
            await SecondImage.FadeTo(1, 50); 


            EnableControls(CheckOutIn.IsToggled);
            OpcoesImage.Source = "icon_opcoes_2";
            OpcoesImage.IsEnabled = CheckOutIn.IsToggled;
        }
        else
        {
            CheckOutIn.IsToggled = false;
            CheckUpdate = CheckOutCheckInAction();
            if (!CheckUpdate)
            {
                return;
            }
            SwitchFrame.BackgroundColor = Color.FromHex("#0D7655");

            SwitchThumb.Margin = new Thickness(5, 0, 0, 0);
            SwitchThumb.HorizontalOptions = LayoutOptions.Start;

            FirstImage.IsVisible = true;
            SecondImage.IsVisible = false;

            FirstImage.Opacity = 0; 
            await FirstImage.FadeTo(1, 50); 


            EnableControls(CheckOutIn.IsToggled);
            OpcoesImage.Source = "icon_opcoes";
            OpcoesImage.IsEnabled = CheckOutIn.IsToggled;
        }
    }

    public bool CheckOutCheckInAction ()
    {
        bool Update = _context.UpdateStateCheckOut(this._metadata, CheckOutIn.IsToggled);

        return Update;

    }

    #region Pop Up 

    private void InitializeOptionsDocumentPopup()
    {
        this.OptionsDocumentPopup = new RadPopup();
        var content = (View)this.Resources["OptionsDocumentPopup"];
        if (DeviceInfo.Idiom == DeviceIdiom.Phone)
        {
            content.SetBinding(View.WidthRequestProperty, new Binding("Width", source: Application.Current.MainPage));
        }

        this.OptionsDocumentPopup.Content = content;
        this.OptionsDocumentPopup.OutsideBackgroundColor = Color.FromArgb("#CC000000");

    }

    private void Open_OptionsDocumentPopup(object sender, System.EventArgs e)
    {
        this.OptionsDocumentPopup.IsOpen = true;

        if (DeviceInfo.Idiom == DeviceIdiom.Desktop)
        {
            this.OptionsDocumentPopup.Placement = PlacementMode.Center;
        }
    }
    private void Close_OptionsDocumentPopup(object sender, System.EventArgs e)
    {
        this.OptionsDocumentPopup.IsOpen = false;
    }

    private async void ChengeDocumentClick(object sender, EventArgs e)
    {
        this.OptionsDocumentPopup.IsOpen = false;
        //this.LoaderPopup.IsOpen = true;
        await Task.Delay(1000);
        //await Navigation.PushAsync(new NewDocumentPageMobile());
        //this.LoaderPopup.IsOpen = false;
    }

    private async void SingDocumentClick(object sender, EventArgs e)
    {
        this.OptionsDocumentPopup.IsOpen = false;
        //this.LoaderPopup.IsOpen = true;
        await Task.Delay(1000);
        //await Navigation.PushAsync(new ListProceses());
        //this.LoaderPopup.IsOpen = false;
    }

    #endregion
}