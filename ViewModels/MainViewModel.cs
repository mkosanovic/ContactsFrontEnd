using Caliburn.Micro;
using ContactsApp.Models;
using ContactsApp.Models.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ContactsApp.ViewModels
{
    public class MainViewModel : BaseViewModel, INotifyPropertyChanged, IInitializable
    {
        public ObservableCollection<Contact> Contacts { get; set; }

        public Contact SelectedContact { get; set; }

        private ContactContext contactContext { get; set; }

        public MainViewModel(INavigationService navigationService) : base(navigationService)        
        {
            contactContext = new ContactContext(ContactsApp.Bootstrapper.Bootstrapper.DBConnectionString);
        }

        public void OnSelectedContactChanged()
        {
            NavigationService.UriFor<ContactDetailViewModel>()
                 .WithParam(x => x.Id, SelectedContact.Id)
                 .Navigate();
        }

        public void Init()
        {
            // fetch data from database
            var contacts = contactContext.ContactsTable.ToList();

            // init observable collection
            Contacts = new ObservableCollection<Contact>(contacts);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
