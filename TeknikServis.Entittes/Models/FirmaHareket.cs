namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FirmaHareket")]
    public partial class FirmaHareket
    {
        public int FirmaHareketID { get; set; }

        public int FirmaID { get; set; }

        public int DepoID { get; set; }

        [Column(TypeName = "date")]
        public DateTime IslemTarihi { get; set; }

        public byte? IslemTuruID { get; set; }

        public decimal? Miktar { get; set; }

        public int? FaturaDetayID { get; set; }

        public int? SatisDetayID { get; set; }

        [StringLength(10)]
        public string BelgeNo { get; set; }

        [StringLength(250)]
        public string Aciklama { get; set; }

        public int? KullaniciID { get; set; }

        public DateTime? KayitZamani { get; set; }

        [StringLength(30)]
        public string MACCAdress { get; set; }

     

        public virtual Firma Firma { get; set; }
    }
}
