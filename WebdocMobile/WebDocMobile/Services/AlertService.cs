﻿using System.Threading.Tasks;

namespace WebDocMobile.Services
{
    public class AlertService : IAlertService
    {
        public Task ShowAlert(string title, string message)
        {
            return Shell.Current.DisplayAlert(title, message, "OK");
        }
    }
}