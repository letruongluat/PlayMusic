using PlayMusic.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlayMusic.Controllers
{
    public class HomeController : Controller
    {
        private MusicWebEntities1 db = new MusicWebEntities1();

        public ActionResult Index()
        {
            return View("~/Views/Login/Index.cshtml");
        }

        public ActionResult LoggedInHome()
        {
            return View("~/Views/Login/Index.cshtml");
        }

        public ActionResult NewMusic()
        {
            var newMusic = db.tblMusics.OrderByDescending(m => m.DisplayName).Take(10).ToList();
            return View(newMusic);
        }

        public ActionResult Genres()
        {
            var genres = db.Theloais.ToList();
            return View(genres);
        }

        public ActionResult Top100()
        {
            return View();
        }

        public ActionResult Artists()
        {
            var artists = db.tblMusics.Select(m => m.casi).Distinct().ToList();
            return View(artists);
        }


        public ActionResult ArtistSongs(string artistName)
        {
            var songs = db.tblMusics.Where(m => m.casi == artistName).ToList();
            ViewBag.ArtistName = artistName; // Optionally pass artist name via ViewBag
            return View(songs);
        }



      
        [HttpGet]
        public ActionResult Contribute()
        {
            ViewBag.Genres = GetMusicGenres();
            return View(new tblMusic());
        }

        [HttpPost]
        public ActionResult Contribute(tblMusic model, HttpPostedFileBase AudioFile, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                // Lưu file âm thanh
                if (AudioFile != null && AudioFile.ContentLength > 0)
                {
                    var audioFileName = System.IO.Path.GetFileName(AudioFile.FileName);
                    var audioPath = Server.MapPath("~/music/") + audioFileName;
                    AudioFile.SaveAs(audioPath);
                    model.Data = audioFileName;
                    model.Cotenttype = AudioFile.ContentType;
                }

                // Lưu file hình ảnh
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    var imageFileName = System.IO.Path.GetFileName(ImageFile.FileName);
                    var imagePath = Server.MapPath("~/image/") + imageFileName;
                    ImageFile.SaveAs(imagePath);
                    model.Image = imageFileName;
                }

                model.luotnghe = 0;

                // Lưu vào cơ sở dữ liệu
                db.tblMusics.Add(model);
                db.SaveChanges();

                ViewBag.Message = "Upload thành công!";
                ViewBag.Genres = GetMusicGenres();
                return View(new tblMusic());
            }

            ViewBag.Genres = GetMusicGenres();
            return View(model);
        }

        private IEnumerable<SelectListItem> GetMusicGenres()
        {
            return db.chudes.Select(t => new SelectListItem
            {

                Value = t.IDchude.ToString(),
                Text = t.theloai1
               

            }).ToList();
        }

        public ActionResult ContributedList()
        {
            return View();
        }

        public ActionResult ManageUserMusic()
        {
            var users = db.tblAccounts.ToList();

            return View(users);
        }

        public ActionResult ManageDisplayedSongs()
        {
            return View();
        }

        public ActionResult ManageViews()
        {
            return View();
        }

        public ActionResult ManageFavoriteAlbums()
        {
            return View();
        }
      
    }
}
