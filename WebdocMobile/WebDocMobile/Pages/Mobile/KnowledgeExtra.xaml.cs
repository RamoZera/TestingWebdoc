using Microsoft.Maui.Layouts;
using Telerik.Maui.Controls;
using WebDocMobile.PageModels.PagesViewModels;
using WebDocMobile.Services;

namespace WebDocMobile.Pages.Mobile;

public partial class KnowledgeExtra : ContentPage
{
    IAlertService _alertService;
    KnowledgeExtraViewModel _context;
    private int _documentId;

    public KnowledgeExtra(int documentId)
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _context = new KnowledgeExtraViewModel(Navigation);
        BindingContext = _context;
        _documentId = documentId;
        _alertService = new AlertService();
         
        knowledgeList.ItemsSource= _context.GetKnowLedgeDocuments(documentId);
        if(_context.knoledgebyDocuments.Count>0)
        {
            TextEmpty.IsVisible = false;
            DescEmpty.IsVisible = false;
        }

    }

    private async void HandleClickBackPage(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }


    private void HandleClickOpenModalAddKnowledge(object sender, EventArgs e)
    {
        ModalAddKnowledge.IsVisible = true;
    }
    private void HandleClickCloseModalAddKnowledge(object sender, EventArgs e)
    {
        ModalAddKnowledge.IsVisible = false;
    }

    private async void HandleClickAddUsers(object sender, EventArgs e)
    {
        ModalLoader.IsVisible = true;
        await Navigation.PushAsync(new KnowledgeUsersExtra(_documentId,true));
        ModalAddKnowledge.IsVisible = false;
        ModalLoader.IsVisible = false;
    }

    private async void HandleClickAddDepartament(object sender, EventArgs e)
    {
        ModalLoader.IsVisible = true;
        await Navigation.PushAsync(new KnowledgeDepartmentExtra(_documentId,true));
        ModalAddKnowledge.IsVisible = false;
        ModalLoader.IsVisible = false;
    }
}