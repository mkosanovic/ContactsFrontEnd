using ContactsApp.Managers;
using ContactsApp.Models;
using ContactsApp.Models.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApp.ViewModels
{
    public class SelectCountryViewModel : INotifyPropertyChanged, IInitializable
    {
        private DatabaseManager dbManager;

        public ObservableCollection<Country> Countries { get; set; }

        public Country SelectedCountry {get;set;}

        private Action<Country> Action { get; set; }

        public SelectCountryViewModel(DatabaseManager dbManager){}

        public SelectCountryViewModel(DatabaseManager dbManager, Action<Country> action){

            this.dbManager = dbManager;

            this.Action = action;

            this.Countries = new ObservableCollection<Country>();
        }
        

        public void Init()
        {
            this.Countries.Clear();

            using (var contactContext = dbManager.CreateContactContext())
            {
                contactContext.CountryTable.ToList().ForEach(this.Countries.Add);
            }
        }

        public void OnSelectedCountryChanged()
        {
            if (this.Action != null) this.Action(SelectedCountry);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
