﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocMobile.Pages.Mobile;
using WebDocMobile.Pages.Desktop;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WebDocMobile.Models;
using WebDocMobile.Helpers.WsMethods;
using WebDocMobile.Services;
using System.Globalization;
using System.Diagnostics;
using Newtonsoft.Json;

namespace WebDocMobile.PageModels
{
    public partial class MainMenuPageViewModel
    {
        public ObservableCollection<CategoricalData> Documents { get; set; } 
        public ObservableCollection<CategoricalData> Processes { get; set; }

        public string username { get; set; }
        public string date { get; set; }
        public string AllDocumentsNumber { get; set; }
        public string MyDocumentsNumber { get; set; }
        public string DepartmentDocumentsNumber { get; set; }
        public string KnownDocumentsNumber { get; set; }

        private DocumentService _documentService;


        private INavigation _navigationService;
        public MainMenuPageViewModel(INavigation navigation)
        {
            this._navigationService = navigation;

            Documents = GetDocumentsData();
            Processes = GetProcessesData();
            username = App.UserDetails.strName;
            date = DateTime.Now.ToString("dd MMMM yyyy", new CultureInfo("PT-pt"));
            _documentService = new DocumentService();

            SetDocumentsNumbers();
        }

        

        private void SetDocumentsNumbers()
        {
            AllDocumentsNumber = App.allDocuments.Count.ToString();
            MyDocumentsNumber = App.myDocuments.Count.ToString();
            DepartmentDocumentsNumber = App.departmentDocuments.Count.ToString();
            KnownDocumentsNumber = App.knownDocuments.Count.ToString();
        }

        private static ObservableCollection<CategoricalData> GetDocumentsData()
        {
            var data = new ObservableCollection<CategoricalData>
            {
                new CategoricalData { Category = "Comigo", Value = 22 },
                new CategoricalData { Category = "Departamento", Value = 268 },
                new CategoricalData { Category = "Conhecimento", Value = 43}
            };
            return data;
        }

        private static ObservableCollection<CategoricalData> GetProcessesData()
        {
            var data = new ObservableCollection<CategoricalData>
            {
                new CategoricalData { Category = "Comigo", Value = 22 },
                new CategoricalData { Category = "Departamento", Value = 268 },
                new CategoricalData { Category = "Conhecimento", Value = 43}
            };
            return data;
        }


        [RelayCommand]
        public async Task HandleSeeDocumentsButton()
        {
#if ANDROID || IOS
            await _navigationService.PushAsync(new DocumentsPageMobile());
#else
            await _navigationService.PushAsync(new DocumentsPageDesktop());
#endif
        }
        [RelayCommand]
        public async Task HandleSeeProcessesButton()
        {
#if ANDROID || IOS
            await _navigationService.PushAsync(new ProcessesPageMobile());
#else
            await _navigationService.PushAsync(new ProcessesPageDesktop());
#endif
        }
        [RelayCommand]
        public void HandleSignOutButton()
        {
            if (Preferences.ContainsKey(nameof(App.UserDetails)))
            {
                Preferences.Remove(nameof(App.UserDetails));
            }
#if ANDROID || IOS
            var list = _navigationService.NavigationStack;
            int x = 0;
            while (x < list.Count)
            {
                Page p = list[x];
                if (!(list[x] is FirstPageMobile))
                {
                    if (list[x] is MainMenuPageMobile)
                    {
                        _navigationService.InsertPageBefore(new FirstPageMobile(), list[x]);
                    }
                    _navigationService.RemovePage(p);
                }
                else
                {
                    x++;
                }
            }
#else
            var list = _navigationService.NavigationStack;
            int x = 0;
            while(x < list.Count)
            {
                Page p = list[x];
                if (!(list[x] is FirstPageDesktop))
                {
                    if (list[x] is MainMenuPageDesktop)
                    {
                        _navigationService.InsertPageBefore(new FirstPageDesktop(), list[x]);
                    }
                    _navigationService.RemovePage(p);
                }
                else
                {
                    x++;
                }
            }
#endif

        }
    }
}
