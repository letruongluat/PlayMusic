using System.Linq;
using System.Web.Mvc;
using PlayMusic.Models;
using System;

namespace PlayMusic.Controllers
{
    public class LoginController : Controller
    {
        private MusicWebEntities1 db = new MusicWebEntities1(); // Replace with your DbContext

        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.ErrorMessage = "Username and password are required";
                return View();
            }

            try
            {
                // Kiểm tra xem username (IDacount) và password có khớp trong cơ sở dữ liệu không
                var user = db.tblAccounts.FirstOrDefault(u => u.IDacount == username && u.Password == password);

                if (user != null)
                {
                    TempData["SuccessMessage"] = "Đăng nhập thành công!";
                    return RedirectToAction("Index", "Song"); // Chuyển hướng đến trang chủ của ứng dụng
                }
                else
                {
                    ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng";
                    return View();
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                ViewBag.ErrorMessage = "Đã xảy ra lỗi: " + ex.Message;
                if (ex.InnerException != null)
                {
                    ViewBag.ErrorMessage += " Lỗi nội bộ: " + ex.InnerException.Message;
                }
                System.Diagnostics.Debug.WriteLine($"Exception: {ex.Message}");
                if (ex.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine($"Lỗi nội bộ: {ex.InnerException.Message}");
                }
                return View();
            }
        }


    }
}
