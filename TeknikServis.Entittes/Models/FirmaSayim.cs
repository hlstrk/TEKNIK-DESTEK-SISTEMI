namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FirmaSayim")]
    public partial class FirmaSayim
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FirmaSayim()
        {
            
        }

        public int FirmaSayimID { get; set; }
       
        [Column(TypeName = "date")]
        public DateTime Tarih { get; set; }

        [StringLength(20)]
        public string SayimKodu { get; set; }

        public int IsyeriID { get; set; }

        public int DepoID { get; set; }

        [StringLength(500)]
        public string Aciklama { get; set; }

        public int? KullaniciID { get; set; }

        public DateTime KayitZamani { get; set; }

        public bool StogaIslemeDurumu { get; set; }

        public DateTime? StogaIslemeZamani { get; set; }

       
    }
}
