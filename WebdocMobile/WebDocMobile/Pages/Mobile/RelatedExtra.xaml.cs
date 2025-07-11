using System.Collections.ObjectModel;
using WebDocMobile.Models;
using WebDocMobile.Models.NewDocument;
using WebDocMobile.PageModels.PagesViewModels;
using WebDocMobile.Services;

namespace WebDocMobile.Pages.Mobile;

public partial class RelatedExtra : ContentPage
{
    IAlertService _alertService;
    RelatedExtraViewModel _context;
    private int _documentId;
    public RelatedExtra(int documentId)
    {

        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _context = new RelatedExtraViewModel(Navigation);
        BindingContext = _context;
        _documentId = documentId;
        _alertService = new AlertService();
        _context.RelatedDocuments(documentId);
        _context.GetRelatedDocumentsWithProcess(documentId);
        Active(true);


    }

    private async void HandleClickBackPage(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private void HandleClickShowProcessRelated(object sender, EventArgs e)
    {
        Active(false);
    }

    private void HandleClickShowDocumentRelated(object sender, EventArgs e)
    {
        Active(true);
    }

    private async void HandleClickRemoverRelation(object sender, EventArgs e)
    {
        ModalAtentionAlert.IsVisible = true;
    }

    private async void HandleClickVoltaRegisto(object sender, EventArgs e)
    {
        await Task.WhenAny<bool>
        (
            MainContent.FadeTo(1, 200),
            ModalAtentionAlert.FadeTo(0, 200),
            ModalRelatedDocument.FadeTo(0, 200),
            ModalRelatedDocumentList.FadeTo(0, 200),
            ModalRelatedProcessList.FadeTo(0, 200)
        );
        ModalAtentionAlert.IsVisible = false;
        ModalRelatedDocument.IsVisible = false;
        ModalRelatedDocumentList.IsVisible = false;
        ModalRelatedProcessList.IsVisible = false;
        list.ItemsSource = _context.relatedDocuments;
        listP.ItemsSource = _context.relatedDocumentsWithProcess;
    }

    private async void HandleClickShowRelatedDocument(object sender, EventArgs e)
    {
        ModalRelatedDocument.IsVisible = true;
    }

    private async void HandleClickHideRelatedDocument(object sender, EventArgs e)
    {
        ModalRelatedDocument.IsVisible = false;
    }

    private async void HandleClickShowComNovoRelated(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NewDocumentPageMobile());
    }


    private async void HandleClickShowRelatedDocumentList(object sender, EventArgs e)
    {
        ModalRelatedDocument.IsVisible = false;
        ModalRelatedDocumentList.IsVisible = true;
    }

    private async void HandleClickHideRelatedDocumentList(object sender, EventArgs e)
    {
        ModalRelatedDocumentList.IsVisible = false;
        ModalRelatedDocument.IsVisible = true;
    }

    private async void HandleClickShowRelatedDocumentWithProcessList(object sender, EventArgs e)
    {
        ModalRelatedDocument.IsVisible = false;
        ModalRelatedProcessList.IsVisible = true;
    }

    private async void HandleClickHideRelatedDocumentWithProcessList(object sender, EventArgs e)
    {
        ModalRelatedProcessList.IsVisible = false;
        ModalRelatedDocument.IsVisible = true;
        
    }

    private async void RelatedWithDocument_Clicked(object sender, EventArgs e)
    {
        ModalLoader.IsVisible = true;
        await Task.Delay(1000);
        int selectedIndex = ListDoc.SelectedIndex;
        if (selectedIndex != -1)
        {
            ObservableCollection<int> c = new();
            foreach (DocumentSelectedResponse b in ListDoc.SelectedItems)
            {
                c.Add(b.id);
            }

            var obj = new AddRelatedDocumentRequest
            {
                targetIds = c.ToArray(),
                id = _documentId
            };
            _context.AddRelatedDocumentRequest(obj);
            // await Navigation.PushAsync(new RelatedExtra(_documentId));
            var response = _context.RelatedDocuments(_documentId);
        }
        ModalRelatedDocumentList.IsVisible = false;
        ModalRelatedProcessList.IsVisible = false;
        ModalRelatedDocument.IsVisible = false;
        ModalLoader.IsVisible = false;
    }


    private async void RelatedWithProcess_Clicked(object sender, EventArgs e)
    {
        ModalLoader.IsVisible = true;
        await Task.Delay(1000);
        int selectedIndex = ListProcess.SelectedIndex;
        if (selectedIndex != -1)
        {
            ObservableCollection<int> c = new();
            foreach (DocumentSelectedResponse b in ListProcess.SelectedItems)
            {
                c.Add(b.id);
            }

            var obj = new AddRelatedDocumentRequest
            {
                targetIds = c.ToArray(),
                id = _documentId
            };
            _context.AddRelatedProcessRequest(obj);
            // await Navigation.PushAsync(new RelatedExtra(_documentId));
            var response = _context.GetRelatedDocumentsWithProcess(_documentId);
        }
        ModalRelatedDocumentList.IsVisible = false;
        ModalRelatedProcessList.IsVisible = false;
        ModalRelatedDocument.IsVisible = false;
        ModalLoader.IsVisible = false;
    }



    private void Active(bool doc)
    {
        if (doc)
        {
            list.ItemsSource = _context.relatedDocuments;
            Process.IsVisible = false;
            Document.IsVisible = true;
            documentsButton.BorderColor = Color.FromArgb("#0074C8");
            processesButton.BorderColor = Color.FromArgb("#40FFFFFF");
        }
        else
        {
            listP.ItemsSource = _context.relatedDocumentsWithProcess;
            Process.IsVisible = true;
            Document.IsVisible = false;
            documentsButton.BorderColor = Color.FromArgb("#40FFFFFF");
            processesButton.BorderColor = Color.FromArgb("#0074C8");
            
        }
    }
}