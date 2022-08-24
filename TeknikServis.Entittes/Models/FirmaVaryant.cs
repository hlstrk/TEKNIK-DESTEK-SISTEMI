namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FirmaVaryant")]
    public partial class FirmaVaryant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FirmaVaryant()
        {
           
        }

        public int FirmaVaryantID { get; set; }

        public int FirmaID { get; set; }

       

        public int? AktifServis { get; set; }

        public int? NumaraID { get; set; }

        public int? MevcutFirmaAdeti { get; set; }

        public int? KritikFirmaAdeti { get; set; }

        public byte? SatisKdvDurumID { get; set; }

        public int? IndirimTuruID { get; set; }

        public decimal? Indirim { get; set; }

        public string Email1 { get; set; }

        public string Email2 { get; set; }

        public string Email3 { get; set; }

        public string Email4 { get; set; }

        public string Email5 { get; set; }

        public int? DepoID { get; set; }

        public int IsyeriID { get; set; }

        public int? KullaniciID { get; set; }

        public DateTime? KayitZamani { get; set; }

        [StringLength(30)]
        public string MaccAdress { get; set; }

        public bool Aktif { get; set; }

        public int? UzunlukID { get; set; }

        public int? BedenID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
         

        public virtual Firma Firma { get; set; }

        
    
    }
}
