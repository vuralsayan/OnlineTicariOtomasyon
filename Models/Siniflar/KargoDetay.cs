using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Models.Siniflar
{
    public class KargoDetay
    {
        [Display(Name = "Kargo ID")]
        [Key]
        public int KargoDetayid { get; set; }

        [Display(Name = "Açıklama")]
        [Column(TypeName = "Varchar")]
        [StringLength(300)]
        public string Aciklama { get; set; }

        [Display(Name = "Takip Kodu")]
        [Column(TypeName = "Varchar")]
        [StringLength(10)]
        public string TakipKodu { get; set; }

        [Display(Name = "Personel")]
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string Personel { get; set; }

        [Display(Name = "Alıcı")]
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string Alici { get; set; }

        public DateTime Tarih { get; set; }

    }
}