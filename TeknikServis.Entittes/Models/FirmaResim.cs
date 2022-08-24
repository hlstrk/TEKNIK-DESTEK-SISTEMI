namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FirmaResim")]
    public partial class FirmaResim
    {
        public int FirmaResimID { get; set; }

        public int FirmaID { get; set; }

        [Required]
        [StringLength(300)]
        public string Url { get; set; }

        [StringLength(300)]
        public string OkumaUrl { get; set; }

        [Required]
        [StringLength(300)]
        public string ResimAdi { get; set; }

        [StringLength(300)]
        public string OrjinalResimAdi { get; set; }

        public byte? Sira { get; set; }

        public bool Aktif { get; set; }

        public DateTime? KayitZamani { get; set; }

        public int? KullaniciID { get; set; }

        [StringLength(30)]
        public string MaccAdress { get; set; }

        public virtual Firma Firma { get; set; }
    }
}
