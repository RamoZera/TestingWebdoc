using System.IO;
using Telerik.Maui.Controls;

namespace WebDocMobile.Pages.Mobile
{
    public partial class ConsultRegistrationPageMobile : ContentPage
    {
        public ConsultRegistrationPageMobile(Stream streamImage)
        {
            InitializeComponent();
            //memoryStream.Dispose();

           
           
        }
        private async void ReturnToBack(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        
    }
}
