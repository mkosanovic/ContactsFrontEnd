using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApp.Models.Database
{
    public class ContactContext : DataContext
    {
        public ContactContext(string connectionString) : base(connectionString) { }

        public Table<Contact> ContactsTable;

        public Table<PhoneType> PhoneTypesTable;

        public Table<City> CitiesTable;

        public Table<Country> CountryTable;
    }

    [Table]
    public class Contact :AbstractEntity
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Id { get; set; }

        [Column]
        public override DateTime TimeStamp { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public string PhoneNumber { get; set; }

        [Column]
        public string City { get; set; }

        [Column]
        public string Address { get; set; }

        [Column]
        public string PhoneType { get; set; }

        [Column]
        public int PhoneTypeId { get; set; }

        [Column]
        public int? CityId { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    [Table]
    public class PhoneType : AbstractEntity
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Id { get; set; }

        [Column]
        public override DateTime TimeStamp { get; set; }

        [Column]
        public string Name { get; set; }
    }

    [Table]
    public class City : AbstractEntity
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Id { get; set; }

        [Column]
        public override DateTime TimeStamp { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public int CountryId { get; set; }
    }

    [Table]
    public class Country : AbstractEntity
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Id { get; set; }

        [Column]
        public override DateTime TimeStamp { get; set; }

        [Column]
        public string Name { get; set; }
    }

    public abstract class AbstractEntity : INotifyPropertyChanged
    {
        public AbstractEntity(){
            TimeStamp = DateTime.Now;
        }

        public abstract DateTime TimeStamp { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    //[Table]
    //public class ContactTable : INotifyPropertyChanged
    //{
    //    [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
    //    public int Id { get; set; }

    //    [Column]
    //    public string Name { get; set; }

    //    [Column]
    //    public int MailId { get; set; }

    //    //[Association(Storage = "_email", ThisKey = "MailId", OtherKey = "Id", IsForeignKey = true)]
    //    //public EMailTable EMail { get; set; }

    //    //[Column]
    //    //public int TelephoneId { get; set; }

    //    //[Association(Storage = "_telephone", ThisKey = "TelephoneId", OtherKey = "Id", IsForeignKey = true)]
    //    //public TelephoneTable Telephone { get; set; }

    //    //[Column]
    //    //public int CityId { get; set; }

    //    //[Association(Storage = "_city", ThisKey = "CityId", OtherKey = "Id", IsForeignKey = true)]
    //    //public CityTable City { get; set; }

    //    //private EntityRef<EMailTable> _email;

    //    //private EntityRef<TelephoneTable> _telephone;

    //    //private EntityRef<CityTable> _city;

    //    public event PropertyChangedEventHandler PropertyChanged;
    //}

    //[Table]
    //public class EMailTable : INotifyPropertyChanged
    //{
    //    [Column(IsPrimaryKey = true, IsDbGenerated=true, DbType="INT NOT NULL Identity", CanBeNull=false, AutoSync=AutoSync.OnInsert)]
    //    public int Id { get; set; }        

    //    [Column]
    //    public string Address { get; set; }

    //    //[Association(Storage = "_contact", OtherKey = "MailId",)]
    //    //public ContactTable Contact { get; set; }

    //    private EntityRef<ContactTable> _contact;

    //    public event PropertyChangedEventHandler PropertyChanged;
    //}

    //[Table]
    //public class PhoneTypeTable : INotifyPropertyChanged
    //{
    //    [Column(IsPrimaryKey=true, IsDbGenerated=true, DbType="INT NOT NULL Identity", CanBeNull=false, AutoSync=AutoSync.OnInsert)]
    //    public int Id { get; set; }

    //    [Column]
    //    public string Name { get; set; }

    //    public event PropertyChangedEventHandler PropertyChanged;
    //}

    //[Table]
    //public class TelephoneTable : INotifyPropertyChanged
    //{
    //    [Column(IsPrimaryKey=true, IsDbGenerated=true, DbType="INT NOT NULL Identity", CanBeNull=false, AutoSync=AutoSync.OnInsert)]
    //    public int Id { get; set; }

    //    [Column]
    //    public string PhoneNumber { get; set; }

    //    [Column]
    //    public int PhoneTypeId { get; set; }

    //    [Association(Storage = "_phoneType", ThisKey = "PhoneTypeId", OtherKey = "Id", IsForeignKey = true)]
    //    public PhoneTypeTable PhoneType { get; set; }

    //    private EntityRef<PhoneTypeTable> _phoneType;

    //    public event PropertyChangedEventHandler PropertyChanged;
    //}

    //[Table]
    //public class CityTable : INotifyPropertyChanged
    //{
    //    [Column(IsPrimaryKey=true, IsDbGenerated=true, DbType="INT NOT NULL Identity", CanBeNull=false, AutoSync=AutoSync.OnInsert)]
    //    public int Id{get;set;}

    //    [Column]
    //    public string Name { get; set; }

    //    [Column]
    //    public string PostalCode { get; set; }

    //    [Column]
    //    public int CountryId { get; set; }

    //    [Association(Storage = "_country", ThisKey = "CountryId", OtherKey = "Id", IsForeignKey = true)]
    //    public CountryTable Country { get; set; }

    //    private EntityRef<CountryTable> _country;

    //    public event PropertyChangedEventHandler PropertyChanged;
    //}

    //[Table]
    //public class CountryTable : INotifyPropertyChanged
    //{
    //    [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
    //    public int Id { get; set; }

    //    [Column]
    //    public string Name { get; set; }

    //    //[Association(Storage="_cities", OtherKey="CountryId", ThisKey="Id")]
    //    //public EntitySet<CityTable> Cities { get; set; }

    //    private EntitySet<CityTable> _cities;

    //    public event PropertyChangedEventHandler PropertyChanged;
    //}

}
