namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("EnvanterTurleri")]
    public partial class EnvanterTurleri
    {
        [Key]
        public int EnvanterTurID { get; set; }

        public string EnvanterTuru  { get; set; }

        public int KullaniciID { get; set; }

        public string BarkodNo { get; set; }
    }
}
