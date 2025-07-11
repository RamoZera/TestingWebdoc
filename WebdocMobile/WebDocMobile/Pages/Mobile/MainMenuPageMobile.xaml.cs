using System.Collections.ObjectModel;
using System.Diagnostics;
using WebDocMobile.CustomControls;
using WebDocMobile.Models.DashBoard;
using WebDocMobile.Models.NewDocument;
using WebDocMobile.PageModels;

namespace WebDocMobile.Pages.Mobile;

public partial class MainMenuPageMobile : ContentPage
{
    private MainMenuPageViewModel _context;
    public ObservableCollection<GetCategoricalData> DataNew { get; set; }
    public MainMenuPageMobile(bool BiometricsConfigured = true)
    {

        InitializeComponent();
        _context = new MainMenuPageViewModel(this.Navigation, BiometricsConfigured);
        this.BindingContext = _context;

        NavigationPage.SetHasNavigationBar(this, false);
        df.ItemsSource = _context.Data;
    }

    public async void DocumentsButtonClicked(Object sender, EventArgs e)
    {
        DataNew = new ObservableCollection<GetCategoricalData>();
        DataNew = _context.Data;
        df.ItemsSource = DataNew;
        await Task.WhenAny<bool>
        (
            documentsSelected.FadeTo(1, 300),
            processesSelected.FadeTo(0, 300)
        );
    }

    public async void ProcessesButtonClicked(Object sender, EventArgs e)
    {
        DataNew = new ObservableCollection<GetCategoricalData>();
        DataNew = _context.DataProceses;
        df.ItemsSource = DataNew;
        await Task.WhenAny<bool>
        (
            documentsSelected.FadeTo(0, 300),
            processesSelected.FadeTo(1, 300)
        );
    }
    private  async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
            await Navigation.PushAsync(new ComprovativoPageMobile(obj, true));
            NavigationPage.SetHasNavigationBar(this, false);
            ModalLoader.IsVisible = false;

        }
        catch (Exception ex)
        {
            //this.memoryStream.Dispose();
        }
        cv.SelectedItem = null;
    }

    private async void CollectionView_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
    {
        var cv = (CollectionView)sender;
        if (cv.SelectedItem == null)
            return;
        Processes c = (Processes)cv.SelectedItem;

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
            await Navigation.PushAsync(new ComprovativoPageMobile(obj, false));
            NavigationPage.SetHasNavigationBar(this, false);
            ModalLoader.IsVisible = false;
        }
        catch (Exception ex)
        {
            //this.memoryStream.Dispose();
        }
        cv.SelectedItem = null;
    }
}