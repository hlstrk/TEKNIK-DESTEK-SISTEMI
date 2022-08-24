namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("ArizaTurleri")]
    public partial class ArizaTurleri
    {
        public ArizaTurleri()
        {


        }


        [Key]
        public int ArizaTuruID { get; set; }

      
        public string ArizaTuru { get; set; }


       

       




    }
}
