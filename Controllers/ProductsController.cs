using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Anime_Web.Models;


namespace Anime_Web.Controllers
{
    public class detail
    {
        public string name { set; get; }
        public string content { set; get; }
        public string genre { set; get; }
        public string image { set; get; }
        public string status { set; get; }
        public string year { set; get; }
        public string rating { set; get; }
    }
   
    public class ProductsController : Controller
    {
        WEB_Anime_ASPEntities _db =new WEB_Anime_ASPEntities();
        // GET: Products
        public ActionResult Categories()
        {
            List<Anime> _anime = _db.Animes.OrderBy(m => m.id).ToList();
            return View(_anime);
        }

        public ActionResult Anime_details(int id) //int id
        {
            detail _listDetail = new detail();

            List<Categogy> _gender = _db.Categogies.Where(m => m.Anime_id_category == id).ToList();

            //List<Anime> data = _db.Animes.Where(x => x.id == id).ToList();
            var Listdata = _db.Animes.Where(x => x.id == id).ToList();

            ViewBag.list = Listdata;

            foreach(var item in _gender)
            {

                ViewBag.gender += item.name;
            }

            var Anime = _db.Animes.FirstOrDefault(m => m.id == id);

            return View(Anime);
        }

        public ActionResult Anime_Watching()
        {
            return View();
        }
    }
}