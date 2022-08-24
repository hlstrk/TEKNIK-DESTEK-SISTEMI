namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Ilce")]
    public partial class ilce
    {
        public ilce()
        {


        }


        [Key]
        public int IlceID { get; set; }

      
      

        public int IlID { get; set; }


        public string IlceAdi { get; set; }

       




    }
}
