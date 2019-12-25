using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmsSynchronizer.Model
{
    public class AddressesModel
    {
        //[PrimaryKey, AutoIncrement, Column("_id")]
        //public int id { get; set; }

        public string Name { get; set; }
    }
}
