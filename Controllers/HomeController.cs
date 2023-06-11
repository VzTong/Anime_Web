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
            List<Anime> _anime = _db.Animes.OrderBy(m => m.id).ToList();
            return View(_anime);
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