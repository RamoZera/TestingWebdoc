using WebDocMobile.Models;
using WebDocMobile.PageModels.StandardViewModels;
using WebDocMobile.Pages.Mobile;

namespace WebDocMobile;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        this.BindingContext = new AppShellViewModel();

        //Mobile Pages Routing

		Routing.RegisterRoute(nameof(LoginPageMobile), typeof(LoginPageMobile));
        Routing.RegisterRoute(nameof(DocumentsPageMobile) , typeof(DocumentsPageMobile));
        Routing.RegisterRoute(nameof(ProcessesPageMobile) , typeof(ProcessesPageMobile));
        Routing.RegisterRoute(nameof(FirstPageMobile) , typeof(FirstPageMobile));
    }
}
