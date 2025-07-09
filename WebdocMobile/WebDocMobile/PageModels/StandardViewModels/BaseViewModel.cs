﻿using CommunityToolkit.Mvvm.ComponentModel;

namespace WebDocMobile.PageModels.StandardViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private string title;
    }
}
