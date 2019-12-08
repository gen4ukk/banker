using SmsSynchronizer.Model;
using SmsSynchronizer.Services;
using SQLite;
using System;
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
            sqlconnection.CreateTable<SMSModel>();
        }

        public int DropTable()
        {
            return sqlconnection.DropTable<SMSModel>();
        }

        //Get all sms  
        public IEnumerable<SMSModel> GetSMSs()
        {
            return (from t in sqlconnection.Table<SMSModel>() select t).ToList();
        }

        //Get sms by period  
        public IEnumerable<SMSModel> GetSMSs(DateTime dtBeg, DateTime dtEnd)
        {
            var end = dtEnd.AddDays(1).AddSeconds(-1);
            return (from t in sqlconnection.Table<SMSModel>()
                    where t.Date >= dtBeg && t.Date <= end
                    select t
                    ).ToList();
        }

        //Get max sms code  
        public SMSModel GetMaxSMS()
        {
            return (from t in sqlconnection.Table<SMSModel>()
                    orderby t.SMSId descending
                    select t
                    ).FirstOrDefault();
        }

        //Get specific sms  
        public SMSModel GetSMS(int id)
        {
            return sqlconnection.Table<SMSModel>().FirstOrDefault(t => t.id == id);
        }

        //Delete specific sms  
        public void DeleteSMS(int id)
        {
            sqlconnection.Delete<SMSModel>(id);
        }

        //Add new sms to DB  
        public void AddSMS(SMSModel sms)
        {
            sqlconnection.Insert(sms);
        }
    }
}
