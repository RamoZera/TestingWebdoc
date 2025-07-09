﻿using CommunityToolkit.Mvvm.ComponentModel;

namespace WebDocMobile.PageModels
{
    public partial class ProcessesPageViewModel : ObservableObject
    {
        public ProcessesPageViewModel()
        {
            // The ViewModel is now correctly decoupled from the View's navigation service.
            // All navigation should be handled via Shell commands.
        }
    }
}
