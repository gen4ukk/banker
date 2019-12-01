using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Banker.Helpers;
using Banker.Models.Profit;
using Microsoft.AspNet.Identity;

namespace Banker.Controllers
{
    public class ProfitController : Controller
    {
        public ActionResult Index()
        {
            ProfitsModel model = new ProfitsModel();
            model.Cards = RepositoryUnits.Instance().CardsRepository.Select(User.Identity.GetUserId());
            model.Profit = RepositoryUnits.Instance().ProfitRepository.Select(User.Identity.GetUserId());
            return View(model);
        }

        public ActionResult Profit()
        {
            List<ProfitModel> model = RepositoryUnits.Instance().ProfitRepository.Select(User.Identity.GetUserId());
            return View(model);
        }

        public ActionResult Cards()
        {
            List<CardsModel> model = RepositoryUnits.Instance().CardsRepository.Select(User.Identity.GetUserId());
            return View(model);
        }
    }
}