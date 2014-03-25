using Caliburn.Micro;
using ContactsApp.Managers;
using ContactsApp.Models;
using ContactsApp.Models.Database;
using ContactsApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApp.Bootstrapper
{
    public class Bootstrapper : PhoneBootstrapper
    {
        public const string DBConnectionString = "Data Source=isostore:/Contacts2.sdf"; 

        PhoneContainer container;

        protected override void Configure()
        {
            InitDatabase();

            container = new PhoneContainer();

            container.RegisterPhoneServices(RootFrame);
            container.Singleton<MainViewModel>();
            container.Singleton<ContactDetailViewModel>();
            container.PerRequest<DatabaseManager>();

            AddCustomConventions();
        }

        static void AddCustomConventions() { }

        protected override object GetInstance(Type service, string key)
        {
            // reference instance
            var instance = container.GetInstance(service, key);

            // if instance is initializable call its initialize method
            if(instance is IInitializable)
                (instance as IInitializable).Init();

            return instance;
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        private void InitDatabase()
        {
            try
            {
                using (var db = new ContactContext(DBConnectionString))
                {
                    //db.DeleteDatabase(); // temporary for testing

                    if (db.DatabaseExists() == false)
                    {
                        // 
                        db.CreateDatabase();

                        db.CountryTable.InsertOnSubmit(new Country { Id = 1, Name = "Hrvatska" });

                        db.CitiesTable.InsertOnSubmit(new City { CountryId = 1, Name = "Zagreb" });

                        db.PhoneTypesTable.InsertAllOnSubmit(new List<PhoneType>{ 
                        new PhoneType { Name = "Mobile"},
                        new PhoneType { Name = "Work"},
                        new PhoneType { Name = "Home"}
                    });

                        db.SubmitChanges();
                    }
                }               
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
