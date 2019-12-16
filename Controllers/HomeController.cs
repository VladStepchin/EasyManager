using EasyManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyManager.Controllers
{
    public class HomeController : Controller
    {

        EasyManagerEntities db = new EasyManagerEntities();

        [Authorize(Roles ="Manager")]
        public ActionResult Index()
        {     
            return View();
        }

        [Authorize(Roles = "Manager")]
        public ActionResult Chart()
        {
            return View();
        }
    }
}