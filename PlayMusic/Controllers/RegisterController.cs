using System;
using System.Linq;
using System.Web.Mvc;
using PlayMusic.Models;

namespace PlayMusic.Controllers
{
    public class RegisterController : System.Web.Mvc.Controller
    {
        private MusicWebEntities1 db = new MusicWebEntities1();

        // GET: Register
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(tblAccount model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Check if the username or email already exists in the database
                    if (db.tblAccounts.Any(a => a.IDacount == model.IDacount || a.email == model.email))
                    {
                        ModelState.AddModelError("", "Tên đăng nhập hoặc email đã tồn tại.");
                        return View(model); // Return to registration page with error message
                    }

                    var account = new tblAccount
                    {
                        IDacount = model.IDacount,
                        Password = model.Password,
                        Name = model.Name,
                        email = model.email,
                        ngaysinh = model.ngaysinh,
                        gioitinh = model.gioitinh
                    };

                    db.tblAccounts.Add(account);
                    db.SaveChanges();

                    // Registration successful, redirect to login page with success message
                    TempData["SuccessMessage"] = "Đăng ký thành công! Vui lòng đăng nhập.";

                    return RedirectToAction("Index", "Login");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message; // or custom error message
                return View("Error"); // Return an error view
            }

            // If ModelState is not valid, return to registration page with validation errors
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
