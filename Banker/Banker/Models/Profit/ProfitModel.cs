using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Banker.Models.Profit
{
    public class ProfitModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int Profit { get; set; }

        public double Amount { get; set; }

        public string Description { get; set; }

        public int CardId { get; set; }
    }
}