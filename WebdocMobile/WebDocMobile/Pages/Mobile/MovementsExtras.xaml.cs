using System.Diagnostics;
using WebDocMobile.Models.NewDocument;
using WebDocMobile.Models.NewProcesses;
using WebDocMobile.PageModels.PagesViewModels;
using WebDocMobile.Services;
namespace WebDocMobile.Pages.Mobile;

public partial class MovementsExtras : ContentPage
{
    IAlertService _alertService;
    MovementExtraViewModel _context;
    private bool _isDocument;
    public DocumentMetadataResponse obj { get; set; }
    public ProcessMetadataResponse objP { get; set; }
    public MovementsExtras(DocumentMetadataRequest _metadata, Boolean isDocument)
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _context = new MovementExtraViewModel(Navigation);
        BindingContext = _context;
        _alertService = new AlertService();
        _context.Movements(_metadata);
        if (isDocument == true)
        {
            obj = _context.GetMetadata(_metadata);
            NumberText.Text = obj.number;
        }
        else
        {
            objP = _context.GetProcessMetadata(_metadata);
            NumberText.Text = objP.number;

        }
    }


    private async void HandleClickBackPage(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void HandleClickApagarMovimento(object sender, EventArgs e) {
        await Task.WhenAny<bool>
        (
            MainContent.FadeTo(0.09, 200),
            ModalAtentionAlert.FadeTo(1, 200)
        );
        ModalAtentionAlert.IsVisible = true;
    }

    private async void HandleClickVoltaRegisto(object sender, EventArgs e)
    {
        await Task.WhenAny<bool>
        (
            MainContent.FadeTo(1, 200),
            ModalAtentionAlert.FadeTo(0, 200),
            ModalEditarMovimento.FadeTo(0, 200)
        );
        ModalAtentionAlert.IsVisible = false;
        ModalEditarMovimento.IsVisible = false;
    }

    private async void HandleClickEditarMovimento(object sender, EventArgs e)
    {
        await Task.WhenAny<bool>
        (
            MainContent.FadeTo(0.09, 200),
            ModalEditarMovimento.FadeTo(1, 200)
        );
        ModalEditarMovimento.IsVisible = true;
    }
}