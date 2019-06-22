using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Banker.Models.Profit
{
    public class ProfitsModel
    {
        public List<ProfitModel> Profit { get; set; }

        public List<CardsModel> Cards { get; set; }
    }
}