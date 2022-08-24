namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Durumlar")]
    public partial class Durumlar

    {
        public Durumlar()
        {


        }


        [Key]
        public int DurumID { get; set; }

        [StringLength(50)]
        public string DurumAdi { get; set; }

   



    }
}
