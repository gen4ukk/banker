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
    public class KeyProfitWordDB
    {
        private SQLiteConnection sqlconnection;

        public KeyProfitWordDB()
        {
            sqlconnection = DependencyService.Get<ISQLite>().GetConnection();
            sqlconnection.CreateTable<KeyProfitWordModel>();
        }

        public int DropTable()
        {
            return sqlconnection.DropTable<KeyProfitWordModel>();
        }

        public IEnumerable<KeyProfitWordModel> GetKeyProfitWords()
        {
            return (from t in sqlconnection.Table<KeyProfitWordModel>() select t).ToList();
        }

        public KeyProfitWordModel GetKeyProfitWord(int id)
        {
            return sqlconnection.Table<KeyProfitWordModel>().FirstOrDefault(t => t.Id == id);
        }

        public void DeleteKeyProfitWord(int id)
        {
            sqlconnection.Delete<KeyProfitWordModel>(id);
        }

        public void AddKeyProfitWord(KeyProfitWordModel word)
        {
            sqlconnection.Insert(word);
        }
    }
}
