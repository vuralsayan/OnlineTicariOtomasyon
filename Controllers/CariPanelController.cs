using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OnlineTicariOtomasyon.Models.Siniflar;

namespace OnlineTicariOtomasyon.Controllers
{
    public class CariPanelController : Controller
    {
        // GET: CariPanel
        Context c = new Context();
        [Authorize]
        public ActionResult Index()
        {
            var cariMail = (string)Session["CariMail"];
            ViewBag.carimail = cariMail;

            var degerler = c.Mesajlars.Where(x => x.Alici == cariMail).ToList();

            var mailID = c.Carilers.Where(x => x.CariMail == cariMail).Select(y => y.CariID).FirstOrDefault();
            ViewBag.mailid = mailID;

            var toplamSatis = c.SatisHarekets.Where(x => x.Cariid == mailID).Count();
            ViewBag.toplamsatis = toplamSatis;

            var satisVarMi = c.SatisHarekets.Any(x => x.Cariid == mailID);

            if (satisVarMi)
            {
                var toplamTutar = c.SatisHarekets.Where(x => x.Cariid == mailID).Sum(y => y.ToplamTutar);
                ViewBag.toplamtutar = toplamTutar;
                var toplamUrunSayisi = c.SatisHarekets.Where(x => x.Cariid == mailID).Sum(y => y.Adet);
                ViewBag.urunsayisi = toplamUrunSayisi;
            }
            else
            {
                ViewBag.toplamtutar = 0.0; // Veya başka bir varsayılan değer
                ViewBag.urunsayisi = 0.0;
            }

            var adSoyad = c.Carilers.Where(x => x.CariMail == cariMail).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.adsoyad = adSoyad;
            return View(degerler);
        }

        public ActionResult Siparislerim()
        {
            var cariMail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == cariMail.ToString()).Select(y => y.CariID).FirstOrDefault();
            var degerler = c.SatisHarekets.Where(x => x.Cariid == id).ToList();
            return View(degerler);
        }

        public ActionResult GelenMesajlar()
        {
            var cariMail = (string)Session["CariMail"];
            var mesajlar = c.Mesajlars.Where(x => x.Alici == cariMail).OrderByDescending(x => x.MesajID).ToList();

            var gelenSayisi = c.Mesajlars.Count(x => x.Alici == cariMail).ToString();
            ViewBag.gelenMail = gelenSayisi;
            var gidenSayisi = c.Mesajlars.Count(x => x.Gonderen == cariMail).ToString();
            ViewBag.gidenMail = gidenSayisi;
            return View(mesajlar);
        }

        public ActionResult GidenMesajlar()
        {
            var cariMail = (string)Session["CariMail"];
            var mesajlar = c.Mesajlars.Where(x => x.Gonderen == cariMail).OrderByDescending(y => y.MesajID).ToList();

            var gelenSayisi = c.Mesajlars.Count(x => x.Alici == cariMail).ToString();
            ViewBag.gelenMail = gelenSayisi;
            var gidenSayisi = c.Mesajlars.Count(x => x.Gonderen == cariMail).ToString();
            ViewBag.gidenMail = gidenSayisi;
            return View(mesajlar);
        }

        public ActionResult MesajDetay(int id)
        {
            var degerler = c.Mesajlars.Where(x => x.MesajID == id).ToList();
            var cariMail = (string)Session["CariMail"];
            var mesajlar = c.Mesajlars.Where(x => x.Gonderen == cariMail).ToList();

            var gelenSayisi = c.Mesajlars.Count(x => x.Alici == cariMail).ToString();
            ViewBag.gelenMail = gelenSayisi;
            var gidenSayisi = c.Mesajlars.Count(x => x.Gonderen == cariMail).ToString();
            ViewBag.gidenMail = gidenSayisi;
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniMesaj()
        {
            var cariMail = (string)Session["CariMail"];
            var mesajlar = c.Mesajlars.Where(x => x.Gonderen == cariMail).ToList();

            var gelenSayisi = c.Mesajlars.Count(x => x.Alici == cariMail).ToString();
            ViewBag.gelenMail = gelenSayisi;
            var gidenSayisi = c.Mesajlars.Count(x => x.Gonderen == cariMail).ToString();
            ViewBag.gidenMail = gidenSayisi;

            return View();
        }

        [HttpPost]
        public ActionResult YeniMesaj(Mesajlar m)
        {
            var cariMail = (string)Session["CariMail"];
            m.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            m.Gonderen = cariMail;
            c.Mesajlars.Add(m);
            c.SaveChanges();
            return View();
        }

        public ActionResult KargoTakip(string p)
        {
            var kargo = from x in c.KargoDetays select x;
            kargo = kargo.Where(y => y.TakipKodu.Equals(p));

            return View(kargo.ToList());
        }

        public ActionResult CariKargoTakip(string id)
        {
            var degerler = c.KargoTakips.Where(x => x.TakipKodu == id).ToList();
            return View(degerler);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

        public PartialViewResult Partial1()
        {
            var cariMail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == cariMail).Select(y => y.CariID).FirstOrDefault();
            var caribul = c.Carilers.Find(id);
            return PartialView("Partial1", caribul);
        }

        public PartialViewResult Partial2()
        {
            var veriler = c.Mesajlars.Where(x => x.Gonderen == "admin").ToList();
            return PartialView(veriler);
        }

        public ActionResult CariBilgiGuncelle(Cariler cr)
        {
            var cari = c.Carilers.Find(cr.CariID);
            cari.CariAd = cr.CariAd;
            cari.CariSoyad = cr.CariSoyad;
            cari.CariSifre = cr.CariSifre;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}