using SmsSynchronizer.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xamarin.Forms;

namespace SmsSynchronizer.Utils.DB
{
    class DBHelper
    {
        public SettingsSchemaDB SettingsSchemaDB { get; private set; }
        public KeyProfitWordDB KeyProfitWordDB { get; private set; }
        public SMSDB SMSDB { get; private set; }
        public UserDB UserDB { get; private set; }
        public AddressesDB AddressesDB { get; private set; }

        private DBHelper()
        {
            var sqlconnection = DependencyService.Get<ISQLite>().GetConnection();
            SettingsSchemaDB = new SettingsSchemaDB(sqlconnection);
            KeyProfitWordDB = new KeyProfitWordDB(sqlconnection);
            SMSDB = new SMSDB(sqlconnection);
            UserDB = new UserDB(sqlconnection);
            AddressesDB = new AddressesDB(sqlconnection);
        }

        private static DBHelper instance;

        public static DBHelper Instance()
        {
            if (instance == null)
                instance = new DBHelper();

            return instance;
        }
    }
}
