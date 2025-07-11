using WebDocMobile.Models.BookService;
using WebDocMobile.Models.NewDocument;
using WebDocMobile.PageModels.PagesViewModels;
using WebDocMobile.Pages.Mobile.ProcessExtra;
using WebDocMobile.Services;

namespace WebDocMobile.Pages.Mobile;

public partial class KnowledgeDepartmentExtra : ContentPage
{
    private List<int> groupList;
    IAlertService _alertService;
    KnowledgeExtraViewModel _context;
    private int _documentId;
    List<String> prefsTargetFolderList = new List<String>();
    private bool _isDocument;
    public KnowledgeDepartmentExtra(int documentId,bool isDocument)
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        groupList = new List<int>();
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

    private void GroupUsersCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var cv = (CollectionView)sender;
        GroupDto obj = cv.SelectedItem as GroupDto;
        foreach (var u in obj.Users)
            u.Selected = true;
        groupList.Add(obj.Id);
    }

    private void UserSelectG_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
       
    }

    private async void SaveKnowledgeG_Clicked(object sender, EventArgs e)
    {
        ModalLoader.IsVisible = true;
        await Task.Delay(1000);
        int[] userIds = { };
        KnowledgeDocumentRequest obj = new KnowledgeDocumentRequest
        {
            id = _documentId,
            comments = Comments.Text,
            observations = Observations.Text,
            usersIds = userIds,
            groupsIds = groupList.ToArray(),

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

    private void HandleClickOpenModalAddKnowledge(object sender, EventArgs e)
    {
        ModalAddKnowledgeG.IsVisible = true;
    }
    private void HandleClickCloseModalAddKnowledge(object sender, EventArgs e)
    {
        ModalAddKnowledgeG.IsVisible = false;
    }
}