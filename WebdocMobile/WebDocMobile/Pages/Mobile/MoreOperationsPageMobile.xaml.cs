using Telerik.Windows.Documents.Flow.Model;
using WebDocMobile.Models.NewDocument;
using WebDocMobile.Models.NewProcesses;
using WebDocMobile.PageModels.PagesViewModels;
using WebDocMobile.Pages.Mobile.ProcessExtra;
using WebDocMobile.Services;

namespace WebDocMobile.Pages.Mobile
{
    public partial class MoreOperationsPageMobile : ContentPage
    {
        IAlertService _alertService;
        MoreOperationsViewModel _context;
        private int documentId;
        private DocumentMetadataRequest _metadata;
        private bool _isDocument;
        public DocumentMetadataResponse obj { get; set; }
        public ProcessMetadataResponse objP { get; set; }

        public MoreOperationsPageMobile(DocumentMetadataRequest metadata, bool isDocument)
        {
            _metadata = metadata;
            _isDocument = isDocument;
            documentId = _metadata.id;
            InitializeComponent();
            _context = new MoreOperationsViewModel(Navigation);
            BindingContext = _context;
            _alertService = new AlertService();
            NavigationPage.SetHasNavigationBar(this, false);
            AutoMaping(metadata);
            if (isDocument == true)
            {
                obj = _context.GetMetadata(metadata);
                NumberText.Text = obj.number;
                WorkflowAtual.Text = obj.WorkflowState;
            }
            else
            {
                objP = _context.GetProcessMetadata(metadata);
                NumberText.Text = objP.number;
               
            }
        }

        private async void ReturnButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void MovimentoButtonClicked(object sender, EventArgs e)
        {
            ModalLoader.IsVisible = true;
            await Task.Delay(100);
            await Navigation.PushAsync(new WorkflowPageMobile(_metadata, _isDocument));
            ModalLoader.IsVisible = false;
        }

        private async void TappedMovimentos(object sender, TappedEventArgs e)
        {
            ModalLoader.IsVisible = true;
            await Task.Delay(100);
            await Navigation.PushAsync(new MovementsExtras(_metadata, _isDocument));
            ModalLoader.IsVisible = false;
        }

        private async void TappedRelacionados(object sender, TappedEventArgs e)
        {
            ModalLoader.IsVisible = true;
            await Task.Delay(100);
            if (_isDocument)
                await Navigation.PushAsync(new RelatedExtra(documentId));
            else
                await Navigation.PushAsync(new ProcessRelatedExtra(documentId));
            ModalLoader.IsVisible = false;
        }

        private async void TappedAnexos(object sender, TappedEventArgs e)
        {
            ModalLoader.IsVisible = true;
            await Task.Delay(100);
            await Navigation.PushAsync(new AnnexesExtra());
            ModalLoader.IsVisible = false;
        }

        private async void TappedContributos(object sender, TappedEventArgs e)
        {
            ModalLoader.IsVisible = true;
            await Task.Delay(100);
            await Navigation.PushAsync(new ContributionsExtra());
            ModalLoader.IsVisible = false;
        }

        private async void TappedConhecimento(object sender, TappedEventArgs e)
        {
            ModalLoader.IsVisible = true;
            await Task.Delay(100);
            if (_isDocument)
                await Navigation.PushAsync(new KnowledgeExtra(documentId));
            else
                await Navigation.PushAsync(new ProcessKnowledgeExtra(documentId));
            ModalLoader.IsVisible = false;
        }

        private async void TappedLocalizacoes(object sender, TappedEventArgs e)
        {
            ModalLoader.IsVisible = true;
            await Task.Delay(100);
            await Navigation.PushAsync(new LocationsExtra());
            ModalLoader.IsVisible = false;
        }

        private async void TappedCamposDinamicos(object sender, TappedEventArgs e)
        {
            ModalLoader.IsVisible = true;
            await Task.Delay(100);
            await Navigation.PushAsync(new DynamicFieldsExtra());
            ModalLoader.IsVisible = false;

        }

        private async void TappedSeguranca(object sender, TappedEventArgs e)
        {
            ModalLoader.IsVisible = true;
            await Task.Delay(100);
            await Navigation.PushAsync(new SecurityExtra());
            ModalLoader.IsVisible = false;
        }

        private async void TappedVersoes(object sender, TappedEventArgs e)
        {
            ModalLoader.IsVisible = true;
            await Task.Delay(100);
            await Navigation.PushAsync(new VersioningExtra());
            ModalLoader.IsVisible = false;
        }

        private async void TappedEntidades(object sender, TappedEventArgs e)
        {
            ModalLoader.IsVisible = true;
            await Task.Delay(100);
            await Navigation.PushAsync(new EntitiesExtra());
            ModalLoader.IsVisible = false;
        }

        private async void TappedMensagens(object sender, TappedEventArgs e)
        {
            ModalLoader.IsVisible = true;
            await Task.Delay(100);
            await Navigation.PushAsync(new MessagesExtra());
            ModalLoader.IsVisible = false;
        }

        private void AutoMaping(DocumentMetadataRequest _metadata)
        {
            if (_isDocument)
            {
                // var result = _context.Movements(_metadata);
                CountMovements.Text = _context.Movements(_metadata).Count.ToString();
                CountRelated.Text = _context.RelatedDocuments(_metadata).Count.ToString();
                CountKnowledge.Text = _context.Knowledges(_metadata).Count.ToString();
                listMovement.ItemsSource = _context.movements;
            }
            else
            {
                CountMovements.Text = _context.MovementsProcess(_metadata).Count.ToString();
                CountRelated.Text = _context.RelatedProcess(_metadata).Count.ToString();
                CountKnowledge.Text = _context.KnowledgesProcess(_metadata).Count.ToString();
                listMovement.ItemsSource = _context.movementsProcess;
            }
        }
    }
}
