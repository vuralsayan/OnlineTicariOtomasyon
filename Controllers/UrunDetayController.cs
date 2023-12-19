using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Siniflar;

namespace OnlineTicariOtomasyon.Controllers
{
    public class UrunDetayController : Controller
    {
        // GET: UrunDetay
        Context c = new Context();
        public ActionResult Index()
        {
            Class1 cs = new Class1();
            //var degerler = c.Uruns.Where(x => x.UrunID == 1).ToList();
            cs.Deger1 = c.Uruns.Where(x => x.UrunID == 2).ToList();
            cs.Deger2 = c.Detays.Where(y => y.DetayID == 2).ToList();
            return View(cs);
        }
    }
}