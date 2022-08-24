namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Il")]
    public partial class il
    {
        public il()
        {


        }


        [Key]
        public int IlID { get; set; }

      
        public int  IlKodu { get; set; }


        public string IlAdi { get; set; }

       




    }
}
