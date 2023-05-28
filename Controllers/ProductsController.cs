using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Anime_Web.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Categories()
        {
            return View();
        }

        public ActionResult Anime_details()
        {
            return View();
        }

        public ActionResult Anime_Watching()
        {
            return View();
        }
    }
}