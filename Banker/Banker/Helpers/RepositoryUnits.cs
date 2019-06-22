using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Banker.Repositories.Profit;

namespace Banker.Helpers
{
    public class RepositoryUnits
    {
        private static RepositoryUnits instance = null;
        private static string connectionString;
 
        public ProfitRepository ProfitRepository;
        public CardsRepository CardsRepository;

        private RepositoryUnits()
        {
            connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            ProfitRepository = new ProfitRepository(connectionString);
            CardsRepository = new CardsRepository(connectionString);
        }

        public static RepositoryUnits Instance()
        {
            if (instance==null)
            {
                instance = new RepositoryUnits();
            }
            return instance;
        }
    }
}