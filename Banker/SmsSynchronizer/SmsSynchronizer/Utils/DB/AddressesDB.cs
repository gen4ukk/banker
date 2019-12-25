using SmsSynchronizer.Model;
using SmsSynchronizer.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace SmsSynchronizer.Utils.DB
{
    class AddressesDB
    {
        private SQLiteConnection sqlconnection;

        public AddressesDB(SQLiteConnection connection)
        {
            sqlconnection = connection;
            sqlconnection.CreateTable<AddressesModel>();
        }

        public int DropTable()
        {
            return sqlconnection.DropTable<AddressesModel>();
        }

        public int DeleteAllData()
        {
            return sqlconnection.DeleteAll<AddressesModel>();
        }

        public List<AddressesModel> GetAddresses()
        {
            return (from t in sqlconnection.Table<AddressesModel>() select t).ToList();
        }

        public void AddAddressesd(List<AddressesModel> list)
        {
            foreach (var item in list)
            {
                sqlconnection.Insert(item);
            }           
        }
    }
}
