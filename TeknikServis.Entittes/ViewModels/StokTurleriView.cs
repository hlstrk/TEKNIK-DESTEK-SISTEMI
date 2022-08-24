namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("StokTurleriView")]
    public partial class StokTurleriView
    {
       


        [Key]
        public int StokTuruID { get; set; }

        [StringLength(100)]
        public string StokTuruAdi { get; set; }

       public int StokKategoriID { get; set; }
        public string StokKategoriAdi { get; set; }


        public string BarkodNo { get; set; }
     
       


    }
}
