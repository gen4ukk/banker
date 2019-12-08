using SmsSynchronizer.Model;
using SmsSynchronizer.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using SQLiteNetExtensions.Extensions;

namespace SmsSynchronizer.Utils.DB
{    

    public class SettingsSchemaDB
    {
        private SQLiteConnection sqlconnection;

        public SettingsSchemaDB()
        {
            sqlconnection = DependencyService.Get<ISQLite>().GetConnection();
            sqlconnection.CreateTable<SettingsSchemaModel>();
            SetDefaultScheme();
        }

        public void SetDefaultScheme()
        {
            var res = GetDefaultScheme();
            if (res != null)
                return;

            var keyProfitWordDB = new KeyProfitWordDB();

            var model = new SettingsSchemaModel()
            {
                SchemaName = "Default Schema",
                ShowAllMessages = false,
                BankName = "OTP Bank",
                PatternForAmount = @"Suma:\s+(-?\d+(?:\.\d+)?)\sUAH",
                UserSchema = false,
                KeyProfitWords = new List<KeyProfitWordModel>(){
                    new KeyProfitWordModel() { Name = "Popovnennya" },
                    new KeyProfitWordModel() { Name = "Zarahuvannia" }}
            };

            sqlconnection.InsertWithChildren(model, recursive: true);
        }

        public SettingsSchemaModel GetDefaultScheme()
        {
            return (from t in sqlconnection.GetAllWithChildren<SettingsSchemaModel>(recursive: true) where t.SchemaName == "Default Schema" select t).FirstOrDefault();
        }

        public int DropTable()
        {
            return sqlconnection.DropTable<SettingsSchemaModel>();
        }

        public IEnumerable<SettingsSchemaModel> GetSettingsSchemas()
        {
            return (from t in sqlconnection.GetAllWithChildren<SettingsSchemaModel>(recursive: true) select t).ToList();
        }

        public SettingsSchemaModel GetSettingsSchema(int id)
        {
            return sqlconnection.GetAllWithChildren<SettingsSchemaModel>(recursive: true).FirstOrDefault(t => t.Id == id);
        }

        public void DeleteSettingsSchema(int id)
        {
            sqlconnection.Delete<SettingsSchemaModel>(id);
        }

        public void AddSettingsSchema(SettingsSchemaModel schema)
        {
            sqlconnection.InsertWithChildren(schema, recursive: true);
        }

        public SettingsSchemaModel GetUsersSchema()
        {
            var query = (from schema in sqlconnection.GetAllWithChildren<SettingsSchemaModel>(recursive: true).ToList()
                         join user in sqlconnection.Table<UserModel>().ToList()
                         on schema.Id equals user.SettingsSchemaId
                         select schema).FirstOrDefault();

            return query;
        }
    }
}
