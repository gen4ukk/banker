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
    public class ProfitRepository
    {
        private readonly string connectionString;

        public ProfitRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Insert(ProfitModel model)
        {
            string commandText = @"
Insert into dbo.Profit (UserId, Profit, Amount, Description, CardId)
values (@UserId, @Profit, @Amount, @Description, @CardId)
";
            var parameters = new Dictionary<string, object>()
            {
                { "@UserId", model.UserId},
                { "@Profit", model.Profit},
                { "@Amount", model.Amount},
                { "@Description", model.Description},
                { "@CardId", model.CardId}
            };

            DbHelper.ExecuteNonQuery(connectionString, commandText, parameters);
        }

        public List<ProfitModel> Select(string userId)
        {
            List<ProfitModel> ret = new List<ProfitModel>();
            string commandText = @"
Select Id, UserId, Profit, Amount, Description, CardId 
from dbo.Profit 
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
                        ret.Add(ProfitToRow(reader));
                    }
                }
            }

            return ret;
        }

        public ProfitModel ProfitToRow(IDataReader reader)
        {
            ProfitModel model = new ProfitModel();

            model.Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0;
            model.UserId = reader["UserId"] != DBNull.Value ? reader["UserId"].ToString() : string.Empty;
            model.Profit = reader["Profit"] != DBNull.Value ? Convert.ToInt32(reader["Profit"]) : 0;
            model.Amount = reader["Amount"] != DBNull.Value ? Convert.ToDouble(reader["Amount"]) : 0;
            model.Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : string.Empty;
            model.CardId = reader["CardId"] != DBNull.Value ? Convert.ToInt32(reader["CardId"]) : 0;

            return model;
        }
    }
}