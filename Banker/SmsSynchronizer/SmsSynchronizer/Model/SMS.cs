using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmsSynchronizer.Model
{
    public class SMS
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int id { get; set; }

        public int SMSId { get; set; }

        [Ignore]
        public string Address { get; set; }

        public string Body { get; set; }

        public double Amount { get; set; }

        [Ignore]
        public string UnixDate { get; set; }

        public DateTime Date { get; set; }

        public bool Profit { get; set; }

        [Ignore]
        public bool Checked { get; set; }

        [Ignore]
        public string Target
        {
            get { return Profit ? "Profit" : "Expense"; }
            set { }
        }
    }
}
