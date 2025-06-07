using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PTU.Context;
using PTU.Models;

public class KullanicilarController : Controller
{
    private readonly MyContext _context;
    public KullanicilarController(MyContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var personelId = HttpContext.Session.GetInt32("PersonelId");
        if (personelId == null)
            return RedirectToAction("Index", "Login");

        var admin = _context.Personeller.FirstOrDefault(x => x.Id == personelId);
        if (admin == null || !admin.IsAdmin)
            return RedirectToAction("Index", "Home");

        var kullanicilar = _context.Personeller.OrderBy(x => x.AdSoyad).ToList();
        return View(kullanicilar);
    }

    public IActionResult Ekle()
    {
        // Yalnızca admin görebilir
        var personelId = HttpContext.Session.GetInt32("PersonelId");
        if (personelId == null)
            return RedirectToAction("Index", "Login");
        var admin = _context.Personeller.FirstOrDefault(x => x.Id == personelId);
        if (admin == null || !admin.IsAdmin)
            return RedirectToAction("Index", "Home");
        return View();
    }

    [HttpPost]
    public IActionResult Ekle(Personel model)
    {
        var personelId = HttpContext.Session.GetInt32("PersonelId");
        if (personelId == null)
            return RedirectToAction("Index", "Login");
        var admin = _context.Personeller.FirstOrDefault(x => x.Id == personelId);
        if (admin == null || !admin.IsAdmin)
            return RedirectToAction("Index", "Home");

        if (_context.Personeller.Any(x => x.SicilNo == model.SicilNo))
        {
            ViewBag.Hata = "Aynı Sicil Numarasına sahip kullanıcı zaten var!";
            return View(model);
        }
        _context.Personeller.Add(model);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Duzenle(int id)
    {
        var personelId = HttpContext.Session.GetInt32("PersonelId");
        if (personelId == null)
            return RedirectToAction("Index", "Login");
        var admin = _context.Personeller.FirstOrDefault(x => x.Id == personelId);
        if (admin == null || !admin.IsAdmin)
            return RedirectToAction("Index", "Home");

        var model = _context.Personeller.Find(id);
        if (model == null) return NotFound();
        return View(model);
    }

    [HttpPost]
    public IActionResult Duzenle(Personel model)
    {
        var personelId = HttpContext.Session.GetInt32("PersonelId");
        if (personelId == null)
            return RedirectToAction("Index", "Login");
        var admin = _context.Personeller.FirstOrDefault(x => x.Id == personelId);
        if (admin == null || !admin.IsAdmin)
            return RedirectToAction("Index", "Home");

        _context.Personeller.Update(model);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Sil(int id)
    {
        var personelId = HttpContext.Session.GetInt32("PersonelId");
        if (personelId == null)
            return RedirectToAction("Index", "Login");
        var admin = _context.Personeller.FirstOrDefault(x => x.Id == personelId);
        if (admin == null || !admin.IsAdmin)
            return RedirectToAction("Index", "Home");

        var silinecek = _context.Personeller.Find(id);
        if (silinecek != null)
        {
            _context.Personeller.Remove(silinecek);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
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
    [HttpGet]
    public IActionResult TalepDuzenle(int id)
    {
        var personelId = HttpContext.Session.GetInt32("PersonelId");
        var talep = _context.TayinTalepleri.FirstOrDefault(x => x.Id == id && x.PersonelId == personelId && x.TalepDurumu == "Bekliyor");
        if (talep == null)
            return RedirectToAction("Taleplerim");
        return View(talep);
    }

    [HttpPost]
    public IActionResult TalepDuzenle(TayinTalebi model)
    {
        var personelId = HttpContext.Session.GetInt32("PersonelId");
        var talep = _context.TayinTalepleri.FirstOrDefault(x => x.Id == model.Id && x.PersonelId == personelId && x.TalepDurumu == "Bekliyor");
        if (talep != null)
        {
            talep.TalepTuru = model.TalepTuru;
            talep.TercihAdliye = model.TercihAdliye;
            talep.Aciklama = model.Aciklama;
            _context.SaveChanges();
        }
        return RedirectToAction("Taleplerim");
    }

    [HttpPost]
    public IActionResult TalepSil(int id)
    {
        var personelId = HttpContext.Session.GetInt32("PersonelId");
        var talep = _context.TayinTalepleri.FirstOrDefault(x => x.Id == id && x.PersonelId == personelId && x.TalepDurumu == "Bekliyor");
        if (talep != null)
        {
            _context.TayinTalepleri.Remove(talep);
            _context.SaveChanges();
        }
        return RedirectToAction("Taleplerim");
    }

}
