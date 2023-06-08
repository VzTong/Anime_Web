using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Anime_Web.Models;

namespace Anime_Web.Areas.Admin.Controllers
{
    public class AdminHomeController : Controller
    {
        // GET: Admin/AdminHome
        [AdminAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        [Route("Err/{statusCode}")]
        public ActionResult Err(int StatusCode)
        {
            return View(nameof(Err), StatusCode);
        }
    }
}