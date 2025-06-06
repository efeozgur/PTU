using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PTU.Context;
using PTU.Models;

namespace PTU.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        MyContext _context;

        public HomeController(ILogger<HomeController> logger, MyContext context)
        {
            _logger = logger;
            _context = context; // Context'i burada baþlatýyoruz
        }

        public IActionResult Index()
        {
            var personelId = HttpContext.Session.GetInt32("PersonelId");
            if (personelId == null)
                return RedirectToAction("Index", "Login");

            var personel = _context.Personeller.FirstOrDefault(p => p.Id == personelId);
            return View(personel);
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation("Privacy page accessed at {Time}", DateTime.UtcNow);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
