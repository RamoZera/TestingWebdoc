using CommunityToolkit.Mvvm.ComponentModel;

namespace WebDocMobile.PageModels
{
    /// <summary>
    /// Represents the visual state of a single digit in the PIN entry UI.
    /// </summary>
    public partial class PinDigitViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isFilled; // True if a digit has been entered for this position.
    }
}