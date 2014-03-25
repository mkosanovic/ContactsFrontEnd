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
    public class SelectPhoneTypeViewModel : INotifyPropertyChanged, IInitializable
    {
        private DatabaseManager dbManager;

        public System.Collections.ObjectModel.ObservableCollection<PhoneType> PhoneTypes { get; set; }

        public PhoneType SelectedPhoneType { get; set; }

        private Action<SelectPhoneTypeViewModel> Action { get; set; }

        public SelectPhoneTypeViewModel(DatabaseManager dbManager) : this(dbManager, null) { }

        /// <summary>
        /// </summary>
        /// <param name="dbManager"></param>
        /// <param name="action">What to do with selected item</param>
        public SelectPhoneTypeViewModel(DatabaseManager dbManager, Action<SelectPhoneTypeViewModel> action)
        {
            this.dbManager = dbManager;

            this.PhoneTypes = new ObservableCollection<PhoneType>();

            this.Action = action;
        }

        public void Init()
        {
            this.PhoneTypes.Clear();

            using (var contactContext = dbManager.CreateContactContext())
            {
                contactContext.PhoneTypesTable.ToList().ForEach(this.PhoneTypes.Add);
            }
        }

        public void OnSelectedPhoneTypeChanged()
        {
            if (this.Action != null) this.Action(this);


        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
