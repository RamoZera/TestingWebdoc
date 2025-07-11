using System.Diagnostics;
using WebDocMobile.PageModels;
using WebDocMobile.Services;

namespace WebDocMobile.Pages.Mobile;

public partial class SelectEntityCodePageMobile : ContentPage
{
	public SelectEntityCodePageMobile(SelectEntityCodePageViewModel viewModel)
	{
		InitializeComponent();

		this.BindingContext = viewModel;

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
}