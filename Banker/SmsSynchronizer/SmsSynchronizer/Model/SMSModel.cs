using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmsSynchronizer.Model
{
    public class SMSModel
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int id { get; set; }

        public int SMSId { get; set; }

        public string Address { get; set; }

        public string Body { get; set; }

        public double Amount { get; set; }

        [Ignore]
        public string UnixDate { get; set; }

        public DateTime Date { get; set; }

        public SmsType Type { get; set; }

        [Ignore]
        public bool Checked { get; set; }

        [ForeignKey(typeof(SettingsSchemaModel))]
        public int SettingsSchemaId { get; set; }

    }

    public enum SmsType
    {
        Profit = 0,
        Expense = 1,
        Another = 2
    }
}
