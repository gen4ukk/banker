using SmsSynchronizer.Model;
using SmsSynchronizer.Services;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace SmsSynchronizer.Utils.DB
{
    public class UserDB
    {
        private SQLiteConnection sqlconnection;

        public UserDB(SQLiteConnection connection)
        {
            sqlconnection = connection;
            sqlconnection.CreateTable<UserModel>();
            SetDefaultUser();
        }

        public void SetDefaultUser()
        {
            var res = GetDefaultUser();
            if (res != null && res.SettingsSchemaId !=0)
                return;

            if (res == null)
            {
                res = new UserModel() { Name = "Evgen", Language = "en-US" };
            }

            var schema = new SettingsSchemaDB(sqlconnection).GetDefaultScheme();

            res.SettingsSchemaId = schema.Id;

            sqlconnection.Insert(res);
        }

        public UserModel GetDefaultUser()
        {
            return (from t in sqlconnection.GetAllWithChildren<UserModel>(recursive: true) select t).FirstOrDefault();
        }

        public List<UserModel> GetAllUser()
        {
            return (from t in sqlconnection.GetAllWithChildren<UserModel>(recursive: true) select t).ToList();
        }

        public int DropTable()
        {
            return sqlconnection.DropTable<UserModel>();
        }

        public void UpdateUser(UserModel user)
        {
            sqlconnection.Update(user);
        }
    }
}
