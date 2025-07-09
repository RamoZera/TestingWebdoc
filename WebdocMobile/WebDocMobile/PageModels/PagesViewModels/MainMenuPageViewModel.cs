﻿﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using WebDocMobile.Pages.Mobile;
using CommunityToolkit.Mvvm.ComponentModel;
using WebDocMobile.Pages.Desktop;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WebDocMobile.Models;
using WebDocMobile.Helpers.WsMethods;
using WebDocMobile.Services;
using System.Globalization;
using System.Diagnostics;
using Microsoft.Maui.Graphics;

namespace WebDocMobile.PageModels
{
    public partial class MainMenuPageViewModel : ObservableObject
    {
        private readonly IAppStateService _appStateService;
        
        // Define brushes once to be reused by both chart data sets.
        private readonly Brush _comigoBrush = new SolidColorBrush(Color.FromArgb("#072B8E"));
        private readonly Brush _departamentoBrush = new SolidColorBrush(Color.FromArgb("#1161FC"));
        private readonly Brush _conhecimentoBrush = new SolidColorBrush(Color.FromArgb("#63B6E3"));
        private readonly IProcessService _processService;
        private readonly ISettingsService _settingsService;
 
        // These lists will hold the data for the two different chart states.
        private List<CategoricalData> _documentsChartData;
        private List<CategoricalData> _processesChartData;
 
        // This is the single collection that the UI will bind to for the chart.
        [ObservableProperty]
        private ObservableCollection<CategoricalData> _chartData;

        [ObservableProperty]
        private bool isShowingDocuments = true;

        [ObservableProperty]
        private string username;
        [ObservableProperty]
        private string date;
        [ObservableProperty]
        private string allDocumentsNumber;
        [ObservableProperty]
        private string myDocumentsNumber;
        [ObservableProperty]
        private string departmentDocumentsNumber;
        [ObservableProperty]
        private string knownDocumentsNumber;

        public MainMenuPageViewModel(IAppStateService appStateService, IProcessService processService, ISettingsService settingsService)
        {
            _appStateService = appStateService;
            _processService = processService;
            _settingsService = settingsService;

            Username = _appStateService.UserDetails?.strName ?? "Unknown User";
            Date = DateTime.Now.ToString("dd MMMM yyyy", new CultureInfo("PT-pt"));

            LoadDocumentData();
            // Asynchronously load process data without blocking the constructor.
            _ = LoadProcessDataAsync();
        }

        /// <summary>
        /// Populates the document count labels and the chart data from the AppStateService.
        /// </summary>
        private void LoadDocumentData()
        {
            var myDocsCount = _appStateService.MyDocuments?.Count ?? 0;
            var deptDocsCount = _appStateService.DepartmentDocuments?.Count ?? 0;
            var knownDocsCount = _appStateService.KnownDocuments?.Count ?? 0;

            // Set the properties for the text labels
            MyDocumentsNumber = myDocsCount.ToString();
            DepartmentDocumentsNumber = deptDocsCount.ToString();
            KnownDocumentsNumber = knownDocsCount.ToString();
            AllDocumentsNumber = (_appStateService.AllDocuments?.Count ?? 0).ToString();

            // Prepare the data for the documents chart.
            _documentsChartData = new List<CategoricalData>
            {
                new CategoricalData { Category = "Comigo", Value = myDocsCount, Color = _comigoBrush },
                new CategoricalData { Category = "Departamento", Value = deptDocsCount, Color = _departamentoBrush },
                new CategoricalData { Category = "Conhecimento", Value = knownDocsCount, Color = _conhecimentoBrush }
            };
            // Initialize the chart with the document data.
            ChartData = new ObservableCollection<CategoricalData>(_documentsChartData);
        }

        private async Task LoadProcessDataAsync()
        {
            // TODO: Replace this with real data fetching from the process service.
            // This would involve calling a method like `_processService.GetProcessSummary()`.
            // For now, we use placeholder data to keep the UI functional.
            await Task.Delay(10); // Simulate async work
            _processesChartData = new List<CategoricalData>
            {
                new CategoricalData { Category = "Comigo", Value = 22, Color = _comigoBrush },
                new CategoricalData { Category = "Departamento", Value = 268, Color = _departamentoBrush },
                new CategoricalData { Category = "Conhecimento", Value = 43, Color = _conhecimentoBrush }
            };
        }

        [RelayCommand]
        private void ToggleChart(string chartType)
        {
            IsShowingDocuments = chartType == "documents";
        }

        /// <summary>
        /// This partial method is automatically called by the MVVM Toolkit's source generator
        /// whenever the IsShowingDocuments property changes.
        /// </summary>
        partial void OnIsShowingDocumentsChanged(bool value)
        {
            var sourceData = value ? _documentsChartData : _processesChartData;
            if (sourceData != null)
            {
                // Re-create the collection to trigger the UI update.
                ChartData = new ObservableCollection<CategoricalData>(sourceData);
            }
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
            // Clear all persisted settings using the service
            _settingsService.ClearAllData();
            // Clear the application state service
            _appStateService.ClearState();

            // Navigate back to the beginning of the app flow
#if ANDROID || IOS
            await Shell.Current.GoToAsync($"//{nameof(SelectEntityCodePageMobile)}");
#else
            await Shell.Current.GoToAsync($"//{nameof(SelectEntityCodePageDesktop)}");
#endif
        }
    }
}
