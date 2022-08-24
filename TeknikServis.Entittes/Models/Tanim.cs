namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tanim")]
    public partial class Tanim
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tanim()
        {
            
        }

        public int TanimID { get; set; }

        public int TanimGrupID { get; set; }

        [StringLength(50)]
        public string TanimAdi { get; set; }

        public bool? Aktif { get; set; }

        public int? Sira { get; set; }

        public int? KullaniciID { get; set; }

        public DateTime? KayitZamani { get; set; }

        public int? IsyeriID { get; set; }

        public long? N11Kodu { get; set; }

        public int? UstGrupID { get; set; }

        [StringLength(10)]
        public string Kodu { get; set; }

        public double AlisDegeri { get; set; }

        public double SatisDegeri { get; set; }

        [StringLength(10)]
        public string Sembol { get; set; }

        public bool DepoOtoKartAcilsin { get; set; }

        public bool DepoETicaretToplaGonder { get; set; }

       

        public virtual TanimGrup TanimGrup { get; set; }
    }
}
