using WebDocMobile.Models;
using WebDocMobile.PageModels.StandardViewModels;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Pages.Desktop;

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

        //Desktop Pages Routing

        Routing.RegisterRoute(nameof(LoginPageDesktop), typeof(LoginPageDesktop));
        Routing.RegisterRoute(nameof(DocumentsPageDesktop), typeof(DocumentsPageDesktop));
        Routing.RegisterRoute(nameof(ProcessesPageDesktop), typeof(ProcessesPageDesktop));
        Routing.RegisterRoute(nameof(FirstPageDesktop), typeof(FirstPageDesktop));
    }
}
