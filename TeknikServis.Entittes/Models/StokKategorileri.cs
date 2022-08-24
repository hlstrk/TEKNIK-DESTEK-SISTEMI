namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("StokKategorileri")]
    public partial class StokKategorileri
    {
       


        [Key]
        public int StokKategoriID { get; set; }

        [StringLength(100)]
        public string StokKategoriAdi { get; set; }

        public string BarkodNo { get; set; }
        public int EkleyenKullaniciID { get; set; }    
       


    }
}
