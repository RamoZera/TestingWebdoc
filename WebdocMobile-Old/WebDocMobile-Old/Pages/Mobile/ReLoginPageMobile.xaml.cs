using WebDocMobile.PageModels;

namespace WebDocMobile.Pages.Mobile;

public partial class ReLoginPageMobile : ContentPage
{
    private int currentDigit;
    public ReLoginPageMobile()
	{
		InitializeComponent();

        this.BindingContext = new ReLoginPageViewModel(this.Navigation);

        NavigationPage.SetHasNavigationBar(this, false);

        InitialPINSet();
    }
    public void BiometricButtonClicked(Object sender, EventArgs e)
    {
        // Dont no if it is needed
    }

    public async void AddDigit(Object sender, EventArgs e)
    {
        currentDigit++;
        await SetPINCircles();
    }
    public async void RemoveDigitPressed(Object sender, EventArgs e)
    {
        if (currentDigit > 0)
        {
            currentDigit--;
            await SetPINCircles();
        }
    }
    private async Task SetPINCircles()
    {
        switch (currentDigit)
        {
            case 0:
                SelectedCircle1.IsVisible = false;
                SelectedCircle2.IsVisible = false;
                SelectedCircle3.IsVisible = false;
                SelectedCircle4.IsVisible = false;
                SelectedCircle5.IsVisible = false;
                SelectedCircle6.IsVisible = false;
                SelectionCircle1.IsVisible = true;
                SelectionCircle2.IsVisible = false;
                SelectionCircle3.IsVisible = false;
                SelectionCircle4.IsVisible = false;
                SelectionCircle5.IsVisible = false;
                SelectionCircle6.IsVisible = false;
                break;
            case 1:
                SelectedCircle1.IsVisible = true;
                SelectedCircle2.IsVisible = false;
                SelectedCircle3.IsVisible = false;
                SelectedCircle4.IsVisible = false;
                SelectedCircle5.IsVisible = false;
                SelectedCircle6.IsVisible = false;
                SelectionCircle1.IsVisible = false;
                SelectionCircle2.IsVisible = true;
                SelectionCircle3.IsVisible = false;
                SelectionCircle4.IsVisible = false;
                SelectionCircle5.IsVisible = false;
                SelectionCircle6.IsVisible = false;
                break;
            case 2:
                SelectedCircle1.IsVisible = true;
                SelectedCircle2.IsVisible = true;
                SelectedCircle3.IsVisible = false;
                SelectedCircle4.IsVisible = false;
                SelectedCircle5.IsVisible = false;
                SelectedCircle6.IsVisible = false;
                SelectionCircle1.IsVisible = false;
                SelectionCircle2.IsVisible = false;
                SelectionCircle3.IsVisible = true;
                SelectionCircle4.IsVisible = false;
                SelectionCircle5.IsVisible = false;
                SelectionCircle6.IsVisible = false;
                break;
            case 3:
                SelectedCircle1.IsVisible = true;
                SelectedCircle2.IsVisible = true;
                SelectedCircle3.IsVisible = true;
                SelectedCircle4.IsVisible = false;
                SelectedCircle5.IsVisible = false;
                SelectedCircle6.IsVisible = false;
                SelectionCircle1.IsVisible = false;
                SelectionCircle2.IsVisible = false;
                SelectionCircle3.IsVisible = false;
                SelectionCircle4.IsVisible = true;
                SelectionCircle5.IsVisible = false;
                SelectionCircle6.IsVisible = false;
                break;
            case 4:
                SelectedCircle1.IsVisible = true;
                SelectedCircle2.IsVisible = true;
                SelectedCircle3.IsVisible = true;
                SelectedCircle4.IsVisible = true;
                SelectedCircle5.IsVisible = false;
                SelectedCircle6.IsVisible = false;
                SelectionCircle1.IsVisible = false;
                SelectionCircle2.IsVisible = false;
                SelectionCircle3.IsVisible = false;
                SelectionCircle4.IsVisible = false;
                SelectionCircle5.IsVisible = true;
                SelectionCircle6.IsVisible = false;
                break;
            case 5:
                SelectedCircle1.IsVisible = true;
                SelectedCircle2.IsVisible = true;
                SelectedCircle3.IsVisible = true;
                SelectedCircle4.IsVisible = true;
                SelectedCircle5.IsVisible = true;
                SelectedCircle6.IsVisible = false;
                SelectionCircle1.IsVisible = false;
                SelectionCircle2.IsVisible = false;
                SelectionCircle3.IsVisible = false;
                SelectionCircle4.IsVisible = false;
                SelectionCircle5.IsVisible = false;
                SelectionCircle6.IsVisible = true;
                break;
            case 6:
                SelectedCircle1.IsVisible = true;
                SelectedCircle2.IsVisible = true;
                SelectedCircle3.IsVisible = true;
                SelectedCircle4.IsVisible = true;
                SelectedCircle5.IsVisible = true;
                SelectedCircle6.IsVisible = true;
                SelectionCircle1.IsVisible = false;
                SelectionCircle2.IsVisible = false;
                SelectionCircle3.IsVisible = false;
                SelectionCircle4.IsVisible = false;
                SelectionCircle5.IsVisible = false;
                SelectionCircle6.IsVisible = false;
                await Task.Delay(1500);
                InitialPINSet();
                break;
        }
    }

    private void InitialPINSet()
    {
        currentDigit = 0;

        SelectedCircle1.IsVisible = false;
        SelectedCircle2.IsVisible = false;
        SelectedCircle3.IsVisible = false;
        SelectedCircle4.IsVisible = false;
        SelectedCircle5.IsVisible = false;
        SelectedCircle6.IsVisible = false;
        SelectionCircle1.IsVisible = true;
        SelectionCircle2.IsVisible = false;
        SelectionCircle3.IsVisible = false;
        SelectionCircle4.IsVisible = false;
        SelectionCircle5.IsVisible = false;
        SelectionCircle6.IsVisible = false;
    }
}