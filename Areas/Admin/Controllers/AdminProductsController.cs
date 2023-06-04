using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Anime_Web.Areas.Admin.Controllers
{
    public class AdminProductsController : Controller
    {
        // GET: Admin/AdminProducts
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create()
        {
            // Lưu trữ các file tải lên
            foreach (string fileName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[fileName];
                if (file != null && file.ContentLength > 0 && IsAllowedFile(file))
                {
                    var ext = System.IO.Path.GetExtension(file.FileName).ToLower();
                    var fileId = Guid.NewGuid().ToString();
                    var filePath = Server.MapPath(string.Format("~/Uploads/{0}{1}", fileId, ext));
                    file.SaveAs(filePath);
                    // Làm gì đó với file ở đây
                }
            }

            return RedirectToAction("Index", "AdminHome");
        }

        public bool IsAllowedFile(HttpPostedFileBase file)
        {
            // Hàm kiểm tra các định dạng file được phép tải lên
            var contentType = file.ContentType.ToLower();
            if (file.ContentLength > 0 &&
                (contentType == "text/plain" ||
                 contentType.Contains("image/") ||
                 contentType.Contains("video/")))
            {
                return true;
            }
            return false;
        }

    }
}