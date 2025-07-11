using WebDocMobile.PageModels;

namespace WebDocMobile.Pages.Mobile;

public partial class ReLoginPageMobile : ContentPage
{
    private int currentDigit;
    private readonly Microsoft.Maui.Controls.Shapes.Path[] selectedCircles;
    private readonly Microsoft.Maui.Controls.Shapes.Path[] selectionCircles;
    public ReLoginPageMobile()
	{
		InitializeComponent();

        this.BindingContext = new ReLoginPageViewModel(this.Navigation);

        NavigationPage.SetHasNavigationBar(this, false);
        selectedCircles = [SelectedCircle1, SelectedCircle2, SelectedCircle3, SelectedCircle4, SelectedCircle5, SelectedCircle6];
        selectionCircles = [SelectionCircle1, SelectionCircle2, SelectionCircle3, SelectionCircle4, SelectionCircle5, SelectionCircle6];

        InitialPINSet();
    }
    public void BiometricButtonClicked(Object sender, EventArgs e)
    {
        // Dont no if it is needed
    }

    public void AddDigit(Object sender, EventArgs e)
    {
        currentDigit++;
        SetPINCircles();
    }
    public void RemoveDigitPressed(Object sender, EventArgs e)
    {
        if (currentDigit > 0)
        {
            currentDigit--;
            SetPINCircles();
        }
    }
    private void SetPINCircles()
    {
        for(int i = 0; i < selectedCircles.Length; i++)
        {
            selectedCircles[i].IsVisible = i < currentDigit;
            selectionCircles[i].IsVisible = i == currentDigit;
        }
    }

    private void InitialPINSet()
    {
        currentDigit = 0;
        SetPINCircles();
    }
}