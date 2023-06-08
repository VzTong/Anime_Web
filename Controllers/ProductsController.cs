using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Anime_Web.Models;


namespace Anime_Web.Controllers
{
   
    public class ProductsController : Controller
    {
        WEB_Anime_ASPEntities _db =new WEB_Anime_ASPEntities();
        // GET: Products
        public ActionResult Categories()
        {
            //List

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