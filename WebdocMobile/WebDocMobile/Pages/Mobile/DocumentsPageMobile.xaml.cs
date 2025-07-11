using Microsoft.Maui.Controls.Shapes;
using Telerik.Maui.Controls;
using WebDocMobile.CustomControls;
using WebDocMobile.Models.DashBoard;
using WebDocMobile.Models.NewDocument;
using WebDocMobile.PageModels;
using WebDocMobile.Services;


namespace WebDocMobile.Pages.Mobile;

public partial class DocumentsPageMobile : ContentPage
{

    private void OnFrameTapped(object sender, EventArgs e)
    {
        // Handle the tap event here
        DisplayAlert("Tapped", "Frame was tapped!", "OK");
    }

    public partial class FirstLookView : ContentView
    {
        public FirstLookView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }
    }

    public DocumentsPageMobile()
    {
        InitializeComponent();


        this.BindingContext = new DocumentsPageViewModel(this.Navigation);

        NavigationPage.SetHasNavigationBar(this, false);

        var pageXAML = new Navbar();


        Label DashboardText = pageXAML.FindByName<Label>("DashboardText");
        DashboardText.IsVisible = false;
        Label ConsulRegisterText = pageXAML.FindByName<Label>("ConsulRegisterText");
        ConsulRegisterText.IsVisible = true;
        bar.Add((View)pageXAML);
        pageXAML.active();

    }

    public object ProximaPagina { get; private set; }

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var cv = (CollectionView)sender;
        if (cv.SelectedItem == null)
            return;
        Document c = (Document)cv.SelectedItem;

        try
        {
            var obj = new DocumentMetadataRequest
            {
                id = c.id,
               sizeRequest = new Sizerequest
                {
                    height = 250,
                    width = 250,
                    keepAspectRatio = true
                }

            };
            ModalLoader.IsVisible = true;
            await Task.Delay(100);
            await Navigation.PushAsync(new ComprovativoPageMobile(obj,true));
            NavigationPage.SetHasNavigationBar(this, false);
            ModalLoader.IsVisible = false;

        }
        catch (Exception ex)
        {
            //this.memoryStream.Dispose();
        }
        cv.SelectedItem = null;
    }

    private async void HandleClickShowFilter(object sender, EventArgs e)
    {
        await Task.WhenAny<bool>
        (
            MainContent.FadeTo(0.09, 200),
            ModalFilterList.FadeTo(1, 200)
        );
        ModalFilterList.IsVisible = true;
    }

    private async void HandleClickVoltaRegisto(object sender, EventArgs e)
    {
        await Task.WhenAny<bool>
        (
            MainContent.FadeTo(1, 200),
            ModalFilterList.FadeTo(0, 200)
        );
        ModalFilterList.IsVisible = false;
    }
}


// Outros métodos e manipuladores de eventos aqui

