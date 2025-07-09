﻿﻿﻿using CommunityToolkit.Maui;
using WebDocMobile.Models;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Pages.Desktop;
using WebDocMobile.PageModels;
using WebDocMobile.Services;
using WebDocMobile.Helpers.WsMethods;
using Newtonsoft.Json;

namespace WebDocMobile;

public partial class App : Application
{
    // All static properties have been moved to IAppStateService and ISettingsService
    // to promote decoupling, testability, and better state management.
	public App(AppShell appShell)
	{
		InitializeComponent();
        // The correct way to start a Shell-based MAUI app.
        // The AppShell will now be responsible for handling the initial navigation logic.
        MainPage = appShell;
    }
}
