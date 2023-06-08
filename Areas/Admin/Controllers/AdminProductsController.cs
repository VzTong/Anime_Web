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
            try
            {
                // Lấy danh sách các studio từ database để hiển thị trên form
                //var studios = _db.studios.ToList();
                //ViewBag.Studios = new SelectList(studios, "id", "name");

                // Lấy danh sách các thể loại từ database để hiển thị trên form
                var categories = _db.categories.ToList();
                ViewBag.Categories = new MultiSelectList(categories, "id", "name");

                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }


        [AdminAuthorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Anime _Anime, HttpPostedFileBase Img, List<int> categories, int studio)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Lưu ảnh cover vào database
                    saveImg(_Anime, Img);

                    // Lưu thông tin của Anime vào database
                    _Anime.categories = new List<category>();

                    // Lấy danh sách các categories từ database và ánh xạ các categories được chọn vào list của Anime

                    foreach (var category_id in _db.categories)
                    {
                        var category = _db.categories.Find(category_id);

                        if (category == null)
                        {
                            Console.WriteLine("Không tìm thấy thông tin category với id = " + category_id);
                        }
                        else
                        {
                            _Anime.categories.Add(category);
                        }
                    }

                    // Lấy thông tin studio từ database và ánh xạ vào studio của Anime
                    //var studio_object = _db.studios.Find(studio);
                    //_Anime.studios = (ICollection<studio>)studio_object;

                    // Lưu Anime vào database
                    _db.Animes.Add(_Anime);
                    _db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi khi lưu Anime và ảnh cover vào database: " + ex.Message);
                }
            }

            // Lấy danh sách các studio từ database để hiển thị trên form
            //List<studio> _studios = _db.studios.ToList();
            //ViewBag.Studios = new SelectList(_studios, "id", "name");

            // Lấy danh sách các thể loại từ database để hiển thị trên form
            List<category> _categories = _db.categories.ToList();
            ViewBag.Categories = new MultiSelectList(_db.categories.ToList(), "id", "name");

            return View(_Anime);

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


        //    [AdminAuthorize]
        //[HttpPost]
        //public ActionResult Create(Anime _Anime, HttpPostedFileBase Image)
        //{
        //    bool isUploaded = false;
        //    string filePath = string.Empty;
        //    try
        //    {

        //        _db.Configuration.ValidateOnSaveEnabled = false;

        //        Anime anime = new Anime();
        //        anime.name = _Anime.name;
        //        anime.episode_count = _Anime.episode_count;
        //        anime.year = _Anime.year;
        //        anime.description = _Anime.description;
        //        anime.rating = _Anime.rating;
        //        anime.Anime_episode = _Anime.Anime_episode;
        //        anime.characters = _Anime.characters;

        //        //covers = _Anime.covers
        //        //trailers = _Anime.trailers
        //        //categories = _Anime.categories
        //        //studios = _Anime.studios


        //        _db.Animes.Add(anime);
        //        _db.SaveChanges();

        //        ModelState.Clear();

        //        foreach (string fileName in Request.Files)
        //        {
        //            HttpPostedFileBase file = Request.Files[fileName];
        //            if (file != null)
        //            {
        //                string path = Server.MapPath("~/Content/Upload/");  // Khai báo đường dẫn thư mục Upload trên máy chủ
        //                string extension = Path.GetExtension(file.FileName);    // Lấy phần mở rộng của tệp được tải lên trong yêu cầu
        //                string fileNameOnly = Path.GetFileNameWithoutExtension(file.FileName);  //Lấy tên tệp được tải lên (không bao gồm phần mở rộng)
        //                string newFileName = Guid.NewGuid().ToString(); //Tạo một tên mới cho tệp bằng cách sử dụng GUID
        //                string fullPath = path + newFileName + extension;   //Tạo đường dẫn đầy đủ cho tệp mới
        //                file.SaveAs(fullPath);  // Lưu tệp được tải lên tại đường dẫn đầy đủ với tên mới
        //                //filePath = fullPath;    // Lưu đường dẫn đầy đủ của tệp đã được tải lên để có thể sử dụng sau này
        //            }
        //        }
        //         isUploaded = true;  // Cập nhật biến `isUploaded` thành `true` để cho biết tệp đã được tải lên thành công

        //        return RedirectToAction("Index", "AdminHome");

        //    }
        //    catch (Exception)
        //    {
        //        return RedirectToAction("Create", "AdminProducts", new { success = false, message = "kiểm tra lại lại thông tin"});
        //    }
        //}


    }
}

