﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PTU.Context;

public class AdminController : Controller
{
    private readonly MyContext _context;

    public AdminController(MyContext context)
    {
        _context = context;
    }

    public IActionResult Index(string arama = "")
    {
        var personelId = HttpContext.Session.GetInt32("PersonelId");
        if (personelId == null)
            return RedirectToAction("Index", "Login");

        var admin = _context.Personeller.FirstOrDefault(x => x.Id == personelId);
        if (admin == null || !admin.IsAdmin)
            return RedirectToAction("Index", "Home");

        
        ViewBag.ToplamPersonel = _context.Personeller.Count();
        ViewBag.ToplamTalep = _context.TayinTalepleri.Count();
        ViewBag.BekleyenTalep = _context.TayinTalepleri.Count(x => x.TalepDurumu == "Bekliyor");
        ViewBag.OnaylananTalep = _context.TayinTalepleri.Count(x => x.TalepDurumu == "Onaylandı");
        ViewBag.ReddedilenTalep = _context.TayinTalepleri.Count(x => x.TalepDurumu == "Reddedildi");

        var talepler = _context.TayinTalepleri
            .Include(x => x.Personel)
            .OrderByDescending(x => x.BasvuruTarihi)
            .AsQueryable();

        // Arama varsa filtrele!
        if (!string.IsNullOrWhiteSpace(arama))
        {
            arama = arama.ToLower().Trim();
            talepler = talepler.Where(x =>
                x.Personel.AdSoyad.ToLower().Contains(arama) ||
                x.TalepTuru.ToLower().Contains(arama) ||
                x.TercihAdliye.ToLower().Contains(arama) ||
                x.TalepDurumu.ToLower().Contains(arama)
            );
        }

        return View(talepler.ToList());


    }

    [HttpPost]
    public IActionResult DurumGuncelle(int id, string durum)
    {
        var personelId = HttpContext.Session.GetInt32("PersonelId");
        if (personelId == null)
            return RedirectToAction("Index", "Login");

        var personel = _context.Personeller.FirstOrDefault(x => x.Id == personelId);
        if (personel == null || !personel.IsAdmin)
            return RedirectToAction("Index", "Home");

        var talep = _context.TayinTalepleri.FirstOrDefault(x => x.Id == id);
        if (talep != null)
        {
            talep.TalepDurumu = durum;
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    public IActionResult Loglar(int page = 1, int pageSize = 10)
    {
        var totalLogs = _context.Loglar.Count();
        var loglar = _context.Loglar
            .OrderByDescending(x => x.Tarih)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        ViewBag.CurrentPage = page;
        ViewBag.PageSize = pageSize;
        ViewBag.TotalLogs = totalLogs;
        ViewBag.TotalPages = (int)Math.Ceiling(totalLogs / (double)pageSize);

        return View(loglar);
    }

}
