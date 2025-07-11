
using WebDocMobile.CustomControls;
using WebDocMobile.Models.DashBoard;
using WebDocMobile.Models.NewDocument;
using WebDocMobile.PageModels;
namespace WebDocMobile.Pages.Mobile
{
    public partial class ListProceses : ContentPage
    {
        public ListProceses()
        {
            InitializeComponent();
            this.BindingContext = new ProcessesPageViewModel(this.Navigation);

            NavigationPage.SetHasNavigationBar(this, false);
            var pageXAML = new Navbar();

            Label DashboardText = pageXAML.FindByName<Label>("DashboardText");
            DashboardText.IsVisible = false;
            Label ConsulRegisterText = pageXAML.FindByName<Label>("ConsulRegisterText");
            ConsulRegisterText.IsVisible = true;
            bar.Add((View)pageXAML);
            pageXAML.active();
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
}
