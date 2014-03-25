using ContactsApp.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApp.Managers
{
    public class DatabaseManager
    {
        private string dbConnectionString;

        public DatabaseManager() { this.dbConnectionString = ContactsApp.Bootstrapper.Bootstrapper.DBConnectionString; }

        public ContactContext CreateContactContext()
        {
            return new ContactContext(dbConnectionString);
        }
    }
}
