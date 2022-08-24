namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("EnvanterMarkalari")]
    public partial class EnvanterMarkalari
    {
        [Key]
        public int EnvanterMarkaID { get; set; }

        public string EnvanterMarkaAdi { get; set; }

        public int KullaniciID { get; set; }

        public string BarkodNo { get; set; }

    }
}
