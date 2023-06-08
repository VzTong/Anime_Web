using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Anime_Web.Models;

namespace Anime_Web.Controllers
{
    public class HomeController : Controller
    {
        WEB_Anime_ASPEntities _db = new WEB_Anime_ASPEntities();

        public ActionResult Index()
        {
            //var list = _db.Animes.ToList();
            return View();
        }
        public ActionResult Blog()
        {
            return View();
        }

        public ActionResult Blog_details()
        {
            return View();
        }
    }
}