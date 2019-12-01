using SmsSynchronizer.Model;
using SmsSynchronizer.Services;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace SmsSynchronizer.Utils.DB
{
    class SMSDB
    {
        private SQLiteConnection sqlconnection;

        public SMSDB()
        {
            //Getting conection and Creating table  
            sqlconnection = DependencyService.Get<ISQLite>().GetConnection();
            sqlconnection.CreateTable<SMS>();
        }

        public int DropTable()
        {
            return sqlconnection.DropTable<SMS>();
        }

        //Get all sms  
        public IEnumerable<SMS> GetSMSs()
        {
            return (from t in sqlconnection.Table<SMS>() select t).ToList();
        }

        //Get specific sms  
        public SMS GetSMS(int id)
        {
            return sqlconnection.Table<SMS>().FirstOrDefault(t => t.id == id);
        }

        //Delete specific sms  
        public void DeleteSMS(int id)
        {
            sqlconnection.Delete<SMS>(id);
        }

        //Add new sms to DB  
        public void AddSMS(SMS sms)
        {
            sqlconnection.Insert(sms);
        }
    }
}
