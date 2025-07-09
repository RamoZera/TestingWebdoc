using CommunityToolkit.Mvvm.ComponentModel;

namespace WebDocMobile.PageModels.StandardViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private bool _title;
    }
}
