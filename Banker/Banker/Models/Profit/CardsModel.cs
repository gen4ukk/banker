using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Banker.Models.Profit
{
    public class CardsModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }

        public string Description { get; set; }

        public double Balans { get; set; }
    }
}