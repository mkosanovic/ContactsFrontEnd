using Caliburn.Micro;
using ContactsApp.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ContactsApp.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected INavigationService NavigationService { get; set; }
        protected DatabaseManager DbManager { get; set; }

        public BaseViewModel(INavigationService navigationService) : this(navigationService, null) { }

        public BaseViewModel(INavigationService navigationService, DatabaseManager dbManager)
        {
            Debug.WriteLine("New instance created");

            NavigationService = navigationService;

            this.DbManager = dbManager;
        }

        public void NewContact() 
        {
            // this will trigger event in contactdetailviewmodel that clears form
            NavigationService.UriFor<ContactDetailViewModel>()
                .WithParam(x => x.Id, -1)
                .Navigate();
        }

        public void Settings()
        {
            //NavigationExtensions.UriFor<SettingsViewModel>().Navigate();
        }

        /// <summary>
        /// To do
        /// </summary>
        public void Help()
        {
            //NavigationExtensions.UriFor<HelpViewModel>().Navigate();
        }

        public void FillWithRandomData()
        {
            
        }

        protected void NotifyOfPropertyChanged(string prop)
        {
            if (this.PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
