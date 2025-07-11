using Telerik.Maui.Controls;
using WebDocMobile.Models;
using WebDocMobile.Models.BookService;
using WebDocMobile.Models.NewDocument;
using WebDocMobile.PageModels.PagesViewModels;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Pages.Mobile.ProcessExtra;
using WebDocMobile.Services;
using User = WebDocMobile.Models.BookService.User;
namespace WebDocMobile.Pages.Mobile;

public partial class KnowledgeUsersExtra : ContentPage
{
    private List<int> userList;
    IAlertService _alertService;
    KnowledgeExtraViewModel _context;
    private int _documentId;
    private bool _isDocument;
    public KnowledgeUsersExtra(int documentId,bool isDocument)
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        userList = new List<int>();
        NavigationPage.SetHasNavigationBar(this, false);
        _context = new KnowledgeExtraViewModel(Navigation);
        BindingContext = _context;
        _documentId = documentId;
        _isDocument = isDocument;
        _alertService = new AlertService();

    }

    private async void HandleClickBackPage(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private void UsersCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var previous = e.PreviousSelection;
        var cv = (CollectionView)sender;
        if (cv.SelectedItem == null)
            return;
     User obj= cv.SelectedItem as User;
        obj.Selected = true;
        if(!userList.Contains(obj.Id))
        userList.Add(obj.Id);

    }
    private void HandleClickOpenModalAddKnowledge(object sender, EventArgs e)
    {
        ModalAddKnowledge.IsVisible = true;
    }
    private void HandleClickCloseModalAddKnowledge(object sender, EventArgs e)
    {
        ModalAddKnowledge.IsVisible = false;
    }
    private async void SaveKnowledge_Clicked(object sender, EventArgs e)
    {
        ModalLoader.IsVisible = true;
        await Task.Delay(1000);
        int[] groupId= { };
        KnowledgeDocumentRequest obj = new KnowledgeDocumentRequest
        {
            id = _documentId,
            comments = Comments.Text,
            observations = Observations.Text,
            usersIds = userList.ToArray(),
            groupsIds =  groupId,

        };
        if (_isDocument)
        {
            _context.SaveKnowLedge(obj);
            await Navigation.PopAsync();
            //await Navigation.PushAsync(new KnowledgeExtra(_documentId));
        }
        else
        {
            _context.SaveKnowLedgeProcess(obj);
            await Navigation.PushAsync(new ProcessKnowledgeExtra(_documentId));
        }
        ModalLoader.IsVisible = false;
    }
}