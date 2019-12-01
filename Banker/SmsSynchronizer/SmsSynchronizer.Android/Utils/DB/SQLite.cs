using System;
using System.IO;
using Xamarin.Forms;
using SmsSynchronizer.Services;
using SQLite;

[assembly: Dependency(typeof(SmsSynchronizer.Droid.Utils.DB.SQLite_Android))]
namespace SmsSynchronizer.Droid.Utils.DB
{
    public class SQLite_Android : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var filename = "Synchronizer.db3";
            var documentspath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentspath, filename);

            var connection = new SQLiteConnection(path);
            return connection;
        }
    }
}