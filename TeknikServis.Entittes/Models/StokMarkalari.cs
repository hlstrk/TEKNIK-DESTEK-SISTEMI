namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("StokMarkalari")]
    public partial class StokMarkalari
    {
       


        [Key]
        public int StokMarkaID { get; set; }

        [StringLength(100)]
        public string StokMarkaAdi { get; set; }

        public string BarkodNo { get; set; }
        public int EkleyenKullaniciID { get; set; }    
       


    }
}
