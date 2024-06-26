using System.Linq;
using System.Web.Mvc;
using PlayMusic.Models;

namespace PlayMusic.Controllers
{
    public class SongController : Controller
    {
        private MusicWebEntities1 db = new MusicWebEntities1();

        // GET: Songs
        public ActionResult Index()
        {
            var songs = db.tblMusics.ToList(); // Lấy danh sách bài hát từ cơ sở dữ liệu

            // Debugging
            System.Diagnostics.Debug.WriteLine("Number of songs: " + songs.Count);

            return View(songs); // Truyền danh sách bài hát đến view
        }




        // GET: Songs/Details/5
        public ActionResult Details(int id)
        {
            var song = db.tblMusics.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }
    }
}
