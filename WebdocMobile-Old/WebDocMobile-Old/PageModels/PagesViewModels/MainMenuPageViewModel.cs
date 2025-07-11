﻿﻿﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Globalization;
using WebDocMobile.Models;
using WebDocMobile.Pages.Desktop;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Services;

namespace WebDocMobile.PageModels.PagesViewModels
{
    public partial class MainMenuPageViewModel : ObservableObject
    {
        private readonly IAppStateService _appStateService;
        private readonly ISettingsService _settingsService;
        private readonly IAlertService _alertService;

        [ObservableProperty]
        private ObservableCollection<CategoricalData> chartData = new();

        [ObservableProperty]
        private string username = string.Empty;
        [ObservableProperty]
        private string date = string.Empty;
        [ObservableProperty]
        private string allDocumentsNumber = "0";
        [ObservableProperty]
        private string myDocumentsNumber = "0";
        [ObservableProperty]
        private string departmentDocumentsNumber = "0";
        [ObservableProperty]
        private string knownDocumentsNumber = "0";

        [ObservableProperty]
        private bool isDocumentsChartSelected = true;

        [ObservableProperty]
        private bool isProcessesChartSelected = false;
        public MainMenuPageViewModel(IAppStateService appStateService, ISettingsService settingsService, IAlertService alertService)
        {
            _appStateService = appStateService;
            _settingsService = settingsService;
            _alertService = alertService;

            LoadDashboardData();
        }

        private void LoadDashboardData()
        {
            Username = _appStateService.UserDetails?.strName ?? "Utilizador";
            Date = DateTime.Now.ToString("dd MMMM yyyy", new CultureInfo("PT-pt"));

            AllDocumentsNumber = _appStateService.AllDocuments?.Count.ToString() ?? "0";
            MyDocumentsNumber = _appStateService.MyDocuments?.Count.ToString() ?? "0";
            DepartmentDocumentsNumber = _appStateService.DepartmentDocuments?.Count.ToString() ?? "0";
            KnownDocumentsNumber = _appStateService.KnownDocuments?.Count.ToString() ?? "0";

            ToggleChart("documents");
        }

        [RelayCommand]
        private void ToggleChart(string chartType)
        {
            IsDocumentsChartSelected = chartType == "documents";
            IsProcessesChartSelected = chartType == "processes";

            // In the future, you would load the correct data here.
            // For now, we just update the UI state.
            ChartData = new ObservableCollection<CategoricalData>
            {
                new CategoricalData { Category = "Comigo", Value = _appStateService.MyDocuments?.Count ?? 0 },
                new CategoricalData { Category = "Departamento", Value = _appStateService.DepartmentDocuments?.Count ?? 0 },
                new CategoricalData { Category = "Conhecimento", Value = _appStateService.KnownDocuments?.Count ?? 0 }
            };
        }



        [RelayCommand]
        public async Task HandleSeeDocumentsButton()
        {
#if ANDROID || IOS
            await Shell.Current.GoToAsync(nameof(DocumentsPageMobile));
#else
            await Shell.Current.GoToAsync(nameof(DocumentsPageDesktop));
#endif
        }

        [RelayCommand]
        public async Task HandleSeeProcessesButton()
        {
#if ANDROID || IOS
            await Shell.Current.GoToAsync(nameof(ProcessesPageMobile));
#else
            await Shell.Current.GoToAsync(nameof(ProcessesPageDesktop));
#endif
        }

        [RelayCommand]
        public async Task HandleSignOutButton()
        {
            _settingsService.UserInfo = null;
            _appStateService.ClearAllState();

#if ANDROID || IOS
            await Shell.Current.GoToAsync($"//{nameof(SelectEntityCodePageMobile)}");
#else
            await Shell.Current.GoToAsync($"//{nameof(SelectEntityCodePageDesktop)}");
#endif
        }
    }
}
