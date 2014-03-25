using Caliburn.Micro;
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
using System.Windows;
using ContactsApp.Utilities;

namespace ContactsApp.ViewModels
{
    public class ContactDetailViewModel : BaseViewModel, IInitializable
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        /// <summary>
        /// will contain one instance of these at a time:
        /// - SelectPhoneTypeViewModel
        /// - SelectCountryViewModel
        /// - SelectCityViewModel
        /// </summary>
        public object ActivePopup { get; set; }

        public SelectPhoneTypeViewModel SelectPhoneTypeViewModel { get; set; }

        public SelectCountryViewModel SelectCountryViewModel { get; set; }

        public SelectCityViewModel SelectCityViewModel { get; set; }

        public string SelectedPhoneTypeText
        {
            get { return SelectPhoneTypeViewModel.SelectedPhoneType == null ? "Select a phone type" : SelectPhoneTypeViewModel.SelectedPhoneType.Name; }
        }

        public string SelectedCountryText
        {
            get { return SelectCountryViewModel.SelectedCountry == null ? "Select a country" : SelectCountryViewModel.SelectedCountry.Name; }
        }

        public string SelectedCityText
        {
            get
            {
                return SelectCountryViewModel.SelectedCountry == null
                            ? "Select a country first"
                            : SelectCityViewModel.SelectedCity == null 
                                ? "Select a city"
                                : SelectCityViewModel.SelectedCity.Name;
            }
        }

        public bool CanSaveContact { get { return IsValid(); } }

        public bool CanSelectCity { get { return SelectCountryViewModel.SelectedCountry != null; } }

        public ContactDetailViewModel(INavigationService navigationService, DatabaseManager dbManager)
            : base(navigationService, dbManager) 
        {
            this.SelectPhoneTypeViewModel = new SelectPhoneTypeViewModel(dbManager, x =>
                {
                    PropertyChangeAndClearActivePopup(this.GetPropertyName(c => c.SelectedPhoneTypeText));
                    NotifyOfPropertyChanged(this.GetPropertyName(c => c.CanSaveContact));
                });

            this.SelectCountryViewModel = new SelectCountryViewModel(dbManager, x => 
                {
                    PropertyChangeAndClearActivePopup(this.GetPropertyName(c => c.SelectedCountryText));
                    NotifyOfPropertyChanged(this.GetPropertyName(c => c.SelectedCityText));
                    NotifyOfPropertyChanged(this.GetPropertyName(c => c.CanSelectCity));
                });

            this.SelectCityViewModel = new SelectCityViewModel(dbManager, x => PropertyChangeAndClearActivePopup(this.GetPropertyName(c => c.SelectedCityText)));
        }

        public void Init()
        {
            SelectPhoneTypeViewModel.Init();

            SelectCountryViewModel.Init();
        }

        private bool IsValid()
        {
            return !string.IsNullOrEmpty(PhoneNumber) &&
                   !string.IsNullOrEmpty(Name) &&
                   SelectPhoneTypeViewModel.SelectedPhoneType != null;
        }

        public void SaveContact()
        {
            // save contact to database and notify user
            try
            {
                using (var contactContext = DbManager.CreateContactContext())
                {
                    var newContact = new Contact
                    {
                        Name = this.Name,
                        PhoneNumber = this.PhoneNumber,
                        City = this.SelectCityViewModel.SelectedCity == null ? null : this.SelectCityViewModel.SelectedCity.Name,
                        PhoneType = this.SelectedPhoneTypeText,
                        PhoneTypeId = this.SelectPhoneTypeViewModel.SelectedPhoneType.Id
                    };

                    if (this.SelectCityViewModel.SelectedCity != null) { newContact.CityId = this.SelectCityViewModel.SelectedCity.Id; }

                    contactContext.ContactsTable.InsertOnSubmit(newContact);

                    contactContext.SubmitChanges();
                }

                MessageBox.Show("Contact added");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // form is cleared when passed id does not exist
        public void OnIdChanged()
        {
            using (var contactContext = DbManager.CreateContactContext())
            {
                var contactData = contactContext.ContactsTable.FirstOrDefault(x => x.Id == Id) ?? new Contact();

                Name = contactData.Name;
                Address = contactData.Address;
                PhoneNumber = contactData.PhoneNumber;
            }
        }
        
        public void SelectPhoneType()
        {
            ActivePopup = SelectPhoneTypeViewModel;
        }

        public void SelectCountry()
        {
            ActivePopup = SelectCountryViewModel;
        }

        public void SelectCity()
        {
            SelectCityViewModel.Init(this.SelectCountryViewModel.SelectedCountry);

            ActivePopup = SelectCityViewModel;
        }

        private void PropertyChangeAndClearActivePopup(string prop)
        {
            this.NotifyOfPropertyChanged(prop);

            this.ActivePopup = null;
        }

        //public event PropertyChangedEventHandler PropertyChanged;
    }
}
