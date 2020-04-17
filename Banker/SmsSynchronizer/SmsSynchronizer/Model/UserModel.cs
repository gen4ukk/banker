using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmsSynchronizer.Model
{
    public class UserModel
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement, SQLite.Column("_id")]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Language { get; set; }

        [ForeignKey(typeof(SettingsSchemaModel))]
        public int SettingsSchemaId { get; set; }
    }
}
