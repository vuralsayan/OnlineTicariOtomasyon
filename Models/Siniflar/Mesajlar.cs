using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Models.Siniflar
{
    public class Mesajlar
    {
        [Key]
        public int MesajID { get; set; }

        [Display(Name = "Gönderen")]
        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string Gonderen { get; set; }

        [Display(Name = "Alıcı")]
        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string Alici { get; set; }

        [Display(Name = "Konu")]
        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string Konu { get; set; }

        [Display(Name = "Mesaj")]
        [Column(TypeName = "Varchar")]
        [StringLength(2000)]
        public string Mesaj { get; set; }

        [Display(Name = "Tarih")]
        [Column(TypeName = "Smalldatetime")]
        public DateTime Tarih { get; set; }



    }
}