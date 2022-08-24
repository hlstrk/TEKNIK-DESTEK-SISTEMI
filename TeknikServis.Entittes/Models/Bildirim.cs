namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Bildirimler")]
    public partial class Bildirim
    {
        public Bildirim()
        {


        }


        [Key]
        public int bildirimID { get; set; }



        public string bildirimIcerigi { get; set; }
        public string olusturanFirma { get; set; }

        public string arizaTuru { get; set; }
        public string olusturanKullanici { get; set; }
        public string musteriBeyani { get; set; }
        public int sayac { get; set; }
















    }
}
