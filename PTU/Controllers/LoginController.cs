using Microsoft.AspNetCore.Mvc;
using PTU.Context;

namespace PTU.Controllers
{
    public class LoginController : Controller
    {
        private MyContext _context;

        public LoginController(MyContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var adminMi = HttpContext.Session.GetInt32("IsAdmin")==1;
            // Giriş yapan varsa otomatik yönlendir
            if ((HttpContext.Session.GetInt32("PersonelId") != null  )) {

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Index(string sicilNo, string sifre)
        {
            
            var user = _context.Personeller.FirstOrDefault(x => x.SicilNo == sicilNo && x.Sifre == sifre);
            if (user != null)
            {
                HttpContext.Session.SetInt32("PersonelId", user.Id);
                HttpContext.Session.SetString("AdSoyad", user.AdSoyad);
                HttpContext.Session.SetInt32("IsAdmin", user.IsAdmin ? 1 : 0);
                if (user.IsAdmin)
                {
                    return RedirectToAction("Index", "Admin");
                } else 
                    return RedirectToAction("Index", "Home");
            }
            ViewBag.Hata = "Sicil numarası veya şifre hatalı!";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Login");
        }
    }
}
