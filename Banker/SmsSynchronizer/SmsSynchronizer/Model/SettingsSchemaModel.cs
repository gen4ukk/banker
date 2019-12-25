using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SmsSynchronizer.Model
{
    public class SettingsSchemaModel
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement, SQLite.Column("_id")]
        public int Id { get; set; }

        public string SchemaName { get; set; }

        public bool ShowAllMessages { get; set; }

        public string BankName { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<KeyProfitWordModel> KeyProfitWords { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<SMSModel> SMSs { get; set; }

        public string PatternForAmount { get; set; }

        public bool UserSchema { get; set; }

        public bool Use { get; set; }
    }
}
