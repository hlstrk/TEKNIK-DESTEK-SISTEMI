namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bolumler")]
    public partial class Bolumler
    {
        [Key]
        public int BolumID { get; set; }

     
        public string BolumAdi { get; set; }


        public int AltDepartmanID { get; set; }








    }
}
