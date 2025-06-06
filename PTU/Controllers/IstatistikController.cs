using Microsoft.AspNetCore.Mvc;
using PTU.Context;

public class IstatistikController : Controller
{
    private readonly MyContext _context;

    public IstatistikController(MyContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        // Başvuru durum dağılımı
        ViewBag.Bekleyen = _context.TayinTalepleri.Count(x => x.TalepDurumu == "Bekliyor");
        ViewBag.Onaylanan = _context.TayinTalepleri.Count(x => x.TalepDurumu == "Onaylandı");
        ViewBag.Reddedilen = _context.TayinTalepleri.Count(x => x.TalepDurumu == "Reddedildi");

        // Son 6 ayda aylık başvuru sayıları
        var bugun = DateTime.Now;
        var aylar = Enumerable.Range(0, 6)
            .Select(i => bugun.AddMonths(-i).ToString("yyyy-MM"))
            .Reverse()
            .ToList();

        var aylikBasvuru = Enumerable.Range(0, 6)
            .Select(i =>
            {
                var hedefAy = bugun.AddMonths(-i);
                return _context.TayinTalepleri
                    .Count(x => x.BasvuruTarihi.Year == hedefAy.Year && x.BasvuruTarihi.Month == hedefAy.Month);
            })
            .Reverse()
            .ToList();

        // En çok bulunan ilk 5 unvan
        var unvanlar = _context.Personeller
            .GroupBy(x => x.Unvan)
            .Select(g => new { Unvan = g.Key, Sayisi = g.Count() })
            .OrderByDescending(x => x.Sayisi)
            .Take(5)
            .ToList();

        ViewBag.Unvanlar = unvanlar.Select(x => x.Unvan).ToList();
        ViewBag.UnvanSayilari = unvanlar.Select(x => x.Sayisi).ToList();



        ViewBag.Aylar = aylar;
        ViewBag.AylikBasvuru = aylikBasvuru;

        // En çok tercih edilen ilk 5 adliye
        var populerAdliyeler = _context.TayinTalepleri
            .GroupBy(x => x.TercihAdliye)
            .Select(g => new { Adliye = g.Key, Sayisi = g.Count() })
            .OrderByDescending(x => x.Sayisi)
            .Take(5)
            .ToList();

        ViewBag.PopulerAdliyeler = populerAdliyeler.Select(x => x.Adliye).ToList();
        ViewBag.AdliyeSayilari = populerAdliyeler.Select(x => x.Sayisi).ToList();

        return View();
    }
}
