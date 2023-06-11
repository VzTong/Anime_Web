using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Anime_Web.Models;
using System.Security.Cryptography;
using System.Text;
using System.IO;


namespace Anime_Web.Areas.Admin.Controllers
{
    public class AdminProductsController : Controller
    {
        // GET: Admin/AdminProducts
        private readonly WEB_Anime_ASPEntities _db = new WEB_Anime_ASPEntities();

        public void saveImg(Anime _Anime, HttpPostedFileBase Img)
        {
            if (ModelState.IsValid && Img != null && Img.ContentLength > 0)
            {
                var fileName = Path.GetFileName(Img.FileName);
                var fileNameExtension = Path.GetExtension(Img.FileName); // lấy đuôi mở rộng

                if (fileNameExtension != ".jpg" && fileNameExtension != ".png")
                {
                    fileName = Path.ChangeExtension(fileName, ".jpg"); // đổi đuôi webp thành jpg
                }
                // tạo 1 folder để quản lý ảnh cover
                var filePath = Path.Combine(Server.MapPath("~/Upload/"), fileName);
                Img.SaveAs(filePath);

                // lưu ảnh cover vào database và cập nhật id của ảnh cover trong bảng Animes
                var image = new Anime{ Anime_covers= fileName }; //????
                //_db.Animes.Add(image);
                _Anime.Anime_covers = fileName;
                //_Anime.id = image.Anime_id_cover;
                _db.Animes.Add(_Anime);
                _db.SaveChanges();
            }
        }

        [AdminAuthorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [AdminAuthorize]
        [HttpPost]
        //[ValidateAntiFo?rgeryToken]
        public ActionResult Create(Anime _anime, HttpPostedFileBase covers, FormCollection _form)
        {
            if (ModelState.IsValid)
            {
                try
                {
           
                    // Lưu ảnh cover vào database
                    saveImg(_anime, covers);

                    Categogy listCate = new Categogy();
                    string id = Session["id"].ToString() ;

                    List<Categogy> _list = new List<Categogy>();
                    // Lấy danh sách các categories từ database và ánh xạ các categories được chọn vào list của Anime
                
                    
                        foreach(var item in _form["check"])
                        {
                            listCate.name = item.ToString();
                            listCate.Anime_id_category = int.Parse(id);
                            _db.Categogies.Add(listCate);
                            _db.SaveChanges();
                        }
                    
  

                    // Lưu Anime vào database
                    _db.Animes.Add(_anime);
                    _db.SaveChanges();

                    return RedirectToAction("/");
                }
                catch (Exception ex)
                {
                    //ModelState.AddModelError("", "Lỗi khi lưu Anime và ảnh cover vào database: " + ex.Message);
                    return RedirectToAction("Create");
                }
            }

            // Lấy danh sách các thể loại từ database để hiển thị trên form
            List<Categogy> _categories = _db.Categogies.ToList();
            ViewBag.Categories = new MultiSelectList(_db.Categogies.ToList(), "id", "name");

            return View("index");

        }

        // Giải phóng tài nguyên khi sử dụng xong
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}

