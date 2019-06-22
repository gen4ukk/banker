using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Banker.Helpers;
using Banker.Models.Profit;

namespace Banker.Repositories.Profit
{
    public class CardsRepository
    {
        private readonly string connectionString;

        public CardsRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Insert(CardsModel model)
        {
            string commandText = @"
Insert into dbo.Cards (UserId, Name, Number, Description, Balans)
values (@UserId, @Name, @Number, @Description, @Balans)
";
            var parameters = new Dictionary<string, object>()
            {
                { "@UserId", model.UserId},
                { "@Profit", model.Name},
                { "@Amount", model.Number},
                { "@Description", model.Description},
                { "@CardId", model.Balans}
            };

            DbHelper.ExecuteNonQuery(connectionString, commandText, parameters);
        }

        public List<CardsModel> Select(string userId)
        {
            List<CardsModel> ret = new List<CardsModel>();
            string commandText = @"
Select Id, UserId, Name, Number, Description, Balans
from dbo.Cards 
where UserId = @UserId
";
            var parameters = new Dictionary<string, object>()
            {
                { "@UserId", userId},
            };

            using (SqlConnection sqlConnection = new SqlConnection())
            {
                using (var reader = DbHelper.ExecuteReader(sqlConnection, connectionString, commandText, parameters))
                {
                    while (reader.Read())
                    {
                        ret.Add(CardsToRow(reader));
                    }
                }
            }

            return ret;
        }

        public CardsModel CardsToRow(IDataReader reader)
        {
            CardsModel model = new CardsModel();

            model.Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0;
            model.UserId = reader["UserId"] != DBNull.Value ? reader["UserId"].ToString() : string.Empty;
            model.Name = reader["Name"] != DBNull.Value ? reader["Name"].ToString() : string.Empty;
            model.Number = reader["Number"] != DBNull.Value ? reader["Number"].ToString() : string.Empty;
            model.Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : string.Empty;
            model.Balans = reader["Balans"] != DBNull.Value ? Convert.ToDouble(reader["Balans"]) : 0;

            return model;
        }
    }
}