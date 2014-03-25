using ContactsApp.Managers;
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
    public class SelectCityViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<City> Cities { get; set; }

        public City SelectedCity { get; set; }

        private Action<SelectCityViewModel> Action { get; set; }

        private DatabaseManager dbManager { get; set; }

        public SelectCityViewModel(DatabaseManager dbManager, Action<SelectCityViewModel> action)
        {
            this.dbManager = dbManager;
            this.Action = action;

            this.Cities = new ObservableCollection<City>();
        }

        public void Init(Country country)
        {
            using (var context = dbManager.CreateContactContext())
            {
                context.CitiesTable.Where(x => x.CountryId == country.Id)
                                   .ToList().ForEach(this.Cities.Add);
            }
        }

        public void OnSelectedCityChanged()
        {
            if (this.Action != null) this.Action(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
