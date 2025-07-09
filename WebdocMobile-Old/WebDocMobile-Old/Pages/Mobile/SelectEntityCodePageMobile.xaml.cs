using System.Diagnostics;
using WebDocMobile.PageModels;
using WebDocMobile.Services;

namespace WebDocMobile.Pages.Mobile;

public partial class SelectEntityCodePageMobile : ContentPage
{
	IAlertService _alertService;
	public SelectEntityCodePageMobile()
	{
		InitializeComponent();

		_alertService = new AlertService();

		this.BindingContext = new SelectEntityCodePageViewModel(this.Navigation);

        NavigationPage.SetHasNavigationBar(this, false);

		SetLabelsText();
    }

	private void SetLabelsText()
	{
		label_1.Text = $"A forma como\ninterages com o<br/>teu <b>webdoc</b> mudou";
		label_1.TextType = TextType.Html;
		label_2.Text = $"Sincroniza a sua conta profissional e acede<br/>a <b>Documentos</b> e <b>Processos</b> a qualquer<br/>momento e lugar";
        label_2.TextType = TextType.Html;
    }

    private async void ValidateEntityCodeButtonPressed(object sender, EventArgs e)
    {
		LoadingScreen.IsVisible = true;
		await Task.WhenAny<bool>
		(
			MainContent.FadeTo(0.09, 200),
			LoadingScreen.FadeTo(1, 200)
		);
        await Task.Delay(1000); //Testing Purposes
        if (App.codigoEntidade == null || App.codigoEntidade == "")
        {
            IncorrectCodeEntity.IsVisible = true;
            await Task.WhenAny<bool>
            (
                LoadingScreen.FadeTo(0, 200),
                IncorrectCodeEntity.FadeTo(1, 200)
            );
            LoadingScreen.IsVisible = false;
        }
		else
		{
            await Task.WhenAny<bool>
			(
				MainContent.FadeTo(1, 200),
				LoadingScreen.FadeTo(0, 200)
			);
            LoadingScreen.IsVisible = false;
        }

	}

	private async void ErroInserirCodigoEntidadeClicked(object sender, EventArgs e)
    {
        await Task.WhenAny<bool>
            (
                MainContent.FadeTo(1, 200),
                LoadingScreen.FadeTo(0, 200),
                IncorrectCodeEntity.FadeTo(0, 200)
            );
        LoadingScreen.IsVisible = false;
        IncorrectCodeEntity.IsVisible = false;
    }
}