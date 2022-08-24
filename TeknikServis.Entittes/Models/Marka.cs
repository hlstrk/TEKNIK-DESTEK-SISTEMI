namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    [Table("Marka")]
    public partial class Markalar
    {
        [Key]
        public int MarkaID { get; set; }

        public string MarkaAdi { get; set; }

      




    }
}
