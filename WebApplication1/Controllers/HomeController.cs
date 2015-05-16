using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoToCO.Models;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace GoToCO.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Results(string activityItem, string eventItem, string consumerItem)
        {
            var model = new Questions();
            model.Activity = activityItem;
            model.Event = eventItem;
            model.ConsumerItem = consumerItem;
            return View(model);
        }

        [OutputCache(VaryByParam="*", Duration=0, NoStore=true)]
        public ActionResult Quiz()
        {
            return View();
        }
    }
}