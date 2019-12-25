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

        public SettingsSchemaDB(SQLiteConnection connection)
        {
            sqlconnection = connection;
            sqlconnection.CreateTable<SettingsSchemaModel>();
            SetDefaultScheme();
        }

        public void SetDefaultScheme()
        {
            var keyProfitWordDB = new KeyProfitWordDB(sqlconnection);
            var SMSDB = new SMSDB(sqlconnection);

            var res = GetDefaultScheme();
            if (res != null)
                return;                       

            var model = new SettingsSchemaModel()
            {
                SchemaName = "Default Schema",
                ShowAllMessages = true,
                //BankName = "Bank Name",
                Use = true,
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
            return (from t in sqlconnection.GetAllWithChildren<SettingsSchemaModel>(recursive: true) where t.UserSchema == false select t).FirstOrDefault();
        }

        public int DropTable()
        {
            return sqlconnection.DropTable<SettingsSchemaModel>();
        }

        public List<SettingsSchemaModel> GetSettingsSchemas()
        {
            return (from t in sqlconnection.GetAllWithChildren<SettingsSchemaModel>(recursive: true) select t).ToList();
        }

        public SettingsSchemaModel GetSettingsSchema(int id)
        {
            return sqlconnection.GetAllWithChildren<SettingsSchemaModel>(recursive: true).FirstOrDefault(t => t.Id == id);
        }

        public void UpdateSettingsSchema(SettingsSchemaModel schema)
        {
            sqlconnection.Update(schema);
        }

        public void DeleteSettingsSchema(int id)
        {
            sqlconnection.Delete<SettingsSchemaModel>(id);
        }

        public void AddSettingsSchema(SettingsSchemaModel schema)
        {
            sqlconnection.InsertWithChildren(schema, recursive: true);
        }

        public List<SettingsSchemaModel> GetUsersSchemas()
        {
            return (from schema in sqlconnection.GetAllWithChildren<SettingsSchemaModel>(recursive: true)
                    where schema.UserSchema == true
                    select schema)
                    .ToList();

        }
    }
}
