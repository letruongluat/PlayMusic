using PlayMusic.Models;
using System.Linq;
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



        public ActionResult Contribute()
        {
            return View();
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
