using Microsoft.AspNetCore.Mvc;
using PTU.Context;
using PTU.Models;

namespace PTU.Controllers
{
    public class TayinTalebiController : Controller
    {
        private readonly MyContext _context;

        public TayinTalebiController(MyContext context)
        {
            _context = context;
        }

        // GET: TayinTalebi/Ekle
        public IActionResult Ekle()
        {
            var personelId = HttpContext.Session.GetInt32("PersonelId");
            if (personelId == null)
                return RedirectToAction("Index", "Login");

            // Talep türleri ve adliye listesini ViewBag ile View'e taşıyoruz
            ViewBag.TalepTurleri = new List<string> { "Sağlık", "Aile Birliği", "Kendi İsteği", "Diğer" };
            ViewBag.Adliyeler = AdliyeListesi(); 

            return View();
        }

        // POST: TayinTalebi/Ekle
        [HttpPost]
        public IActionResult Ekle(TayinTalebi talep)
        {
            ModelState.Remove("Personel");
            ModelState.Remove("TalepDurumu");
            var personelId = HttpContext.Session.GetInt32("PersonelId");
            var kisi = HttpContext.Session.GetString("AdSoyad");
            var buYil = DateTime.Now.Year;
            bool ayniYildaTalepVarMi = _context.TayinTalepleri
                .Any(x => x.PersonelId == personelId && x.BasvuruTarihi.Year == buYil);
            Console.WriteLine("Session PersonelId: " + personelId);
            if (personelId == null)
                return RedirectToAction("Index", "Login");

            talep.PersonelId = (int)HttpContext.Session.GetInt32("PersonelId");
            talep.BasvuruTarihi = DateTime.Now;
            talep.TalepDurumu = "Bekliyor";

            if (ModelState.IsValid)
            {
                if (ayniYildaTalepVarMi)
                {
                    ModelState.AddModelError("", "Bu yıl zaten bir tayin talebiniz var. Lütfen önceki talebinizi (Onaylanmadıysa) iptal edin.");
                }
                else
                {
                    _context.Loglar.Add(new Log
                    {
                        PersonelId = (int)personelId,
                        Islem = "Tayin Talebi Ekleme",
                        Tarih = DateTime.Now,
                        Aciklama = kisi + " tarafından tayin talebi eklendi: " + talep.TalepTuru + " - " + talep.TercihAdliye
                    });
                    _context.TayinTalepleri.Add(talep);
                    _context.SaveChanges();
                    return RedirectToAction("Taleplerim");
                }
            }


            ViewBag.TalepTurleri = new List<string> { "Sağlık", "Aile Birliği", "Kendi İsteği", "Diğer" };
            ViewBag.Adliyeler = AdliyeListesi();
            return View(talep);
        }

        public IActionResult Taleplerim()
        {
            var personelId = HttpContext.Session.GetInt32("PersonelId");
            if (personelId == null)
                return RedirectToAction("Index", "Login");

            var talepler = _context.TayinTalepleri
                .Where(x => x.PersonelId == personelId)
                .OrderByDescending(x => x.BasvuruTarihi)
                .ToList();

            return View(talepler);
        }


        private List<string> AdliyeListesi()
        {
            return new List<string>
        {
            "Adana Adliyesi","Adıyaman Adliyesi","Afyonkarahisar Adliyesi","Ağrı Adliyesi","Aksaray Adliyesi",
            "Yalova Adliyesi","Yozgat Adliyesi","Zonguldak Adliyesi"
        };
        }
    }
}
