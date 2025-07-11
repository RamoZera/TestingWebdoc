using System.Collections.ObjectModel;
using WebDocMobile.Models;
using WebDocMobile.Models.NewDocument;
using WebDocMobile.PageModels.PagesViewModels;
using WebDocMobile.Services;

namespace WebDocMobile.Pages.Mobile.ProcessExtra
{
    public partial class ProcessRelatedExtra : ContentPage
    {
        IAlertService _alertService;
        RelatedExtraViewModel _context;
        private int _processId;
        public ProcessRelatedExtra(int processId)
        {

            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            _context = new RelatedExtraViewModel(Navigation);
            BindingContext = _context;
            _processId = processId;
            _alertService = new AlertService();
            var response = _context.GetRelatedWithProcess(processId);



        }

        private async void HandleClickBackPage(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void HandleClickRemoverRelation(object sender, EventArgs e)
        {
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
                ModalRelatedDocument.FadeTo(0, 200),
                ModalRelatedProcessList.FadeTo(0, 200)
            );
            ModalAtentionAlert.IsVisible = false;
            ModalRelatedDocument.IsVisible = false;
            ModalRelatedProcessList.IsVisible = false;
        }

        private async void HandleClickRelatedDocument(object sender, EventArgs e)
        {
            await Task.WhenAny<bool>
            (
                MainContent.FadeTo(0.09, 200),
                ModalRelatedDocument.FadeTo(1, 200)
            );
            ModalRelatedDocument.IsVisible = true;
        }


        private async void HandleClickRelatedDocumentList(object sender, EventArgs e)
        {
            await Task.WhenAny<bool>
            (
                MainContent.FadeTo(0.09, 200),
                ModalRelatedProcessList.FadeTo(1, 200)
            );
            ModalRelatedProcessList.IsVisible = true;
            ModalRelatedDocument.IsVisible = false;
        }

      


        private void RelatedWithProcess_Clicked(object sender, EventArgs e)
        {
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
                    id = _processId
                };
                _context.AddRelatedWithProcess(obj);
                // await Navigation.PushAsync(new RelatedExtra(_documentId));
                var response = _context.GetRelatedWithProcess(_processId);
            }
        }

        private async void NewProcess_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewProcessPageMobile());
        }


    }
}
