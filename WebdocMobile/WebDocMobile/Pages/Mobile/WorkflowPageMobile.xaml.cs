using CommunityToolkit.Mvvm.Input;
using System.Reflection.Metadata.Ecma335;
using Telerik.Windows.Documents.Flow.FormatProviders.Docx.Validation;
using WebDocMobile.Models.NewDocument;
using WebDocMobile.Models.NewProcesses;
using WebDocMobile.Models.WorkflowService;
using WebDocMobile.PageModels.PagesViewModels;
using WebDocMobile.Services;

namespace WebDocMobile.Pages.Mobile
{
    public partial class WorkflowPageMobile : ContentPage
    {
        IAlertService _alertService;
        WorkflowPageViewModel _context;
        private int _documentId;
        private int _optionId;
        private bool _isDocument;
        private DocumentMetadataRequest _metadata;
        public DocumentMetadataResponse obj { get; set; }
        public ProcessMetadataResponse objP { get; set; }
        public WorkflowPageMobile(DocumentMetadataRequest metadata, bool isDocument)
        {

            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            _context = new WorkflowPageViewModel(Navigation);
            BindingContext = _context;
            _metadata = metadata;
            _documentId = metadata.id;
            _isDocument = isDocument;
           
            if (isDocument)
            {
                _context.GetWorkflowHystory(_documentId);
                workflowsList.ItemsSource = _context.worksflows;
                _context.GetWorflowkNavigation(_documentId);
                GroupRadioCollection.ItemsSource = _context.radioNavigates;

                obj = _context.GetMetadata(metadata);
                NumberText.Text = obj.number;
                WorkflowAtual.Text = obj.WorkflowState;
            }
            else
            {
                objP = _context.GetProcessMetadata(metadata);
                workflowsList.ItemsSource = _context.GetProcessWorkflowHystory(_documentId);
                _context.GetProcessWorflowkNavigation(_documentId);
                GroupRadioCollection.ItemsSource = _context.radioNavigatesProcess;
                NumberText.Text = objP.number;
            }



        }

        private async void ReturnButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void FecharModal_Clicked(object sender, EventArgs e)
        {
            ModalAtentionAlert.IsVisible = false;
        }

        private async void WorkflowConfirm_Clicked(object sender, EventArgs e)
        {
            if (GroupRadioCollection.SelectedItem == null)
            {
                ModalAtentionAlert.IsVisible = true;
                ModalAtentionIcon.Source = "icon_alerta";
                ModalAtentionTitle.Text = "Aviso";
                ModalAtentionText.Text = "Nenhum workflow selecionado";
                return;
            }
            ModalLoader.IsVisible = true;
            
            await Task.Delay(1000);
            WorkflowRequest obj = new WorkflowRequest
            {
                Id = _documentId,
                Comments = Comments.Text,
                WorkflowStateId = _optionId

            };
            if (_isDocument)
            {
                _context.AddWorkflowNavigation(obj);
                await Navigation.PushAsync(new MoreOperationsPageMobile(_metadata,_isDocument));
            }
            else
            {
                _context.AddProcessWorkflowNavigation(obj);
               await Navigation.PushAsync(new MoreOperationsPageMobile(_metadata,_isDocument));
            }
            ModalLoader.IsVisible = false;
        }
       

        private void GroupRadioCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cv = (CollectionView)sender;
            if (cv.SelectedItem == null)
                return;
            RadioNavigate obj = cv.SelectedItem as RadioNavigate;
            obj.Selected = true;
            _optionId = obj.Id;
        }

       
    }
}
