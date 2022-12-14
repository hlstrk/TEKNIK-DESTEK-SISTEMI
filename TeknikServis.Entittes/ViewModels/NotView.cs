namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("NotView")]
    public partial class NotView
    {
        public NotView()
        {


        }


        [Key]
        public int NotID { get; set; }

        [StringLength(50)]
        public string KullaniciAdi { get; set; }

        public int FirmaID { get; set; }   
        
        public int KullaniciID { get; set; }    
        public bool DuyuruMu { get; set; }

        public string OZamani { get; set; }

        public bool YapilacakMi { get; set; }


        public bool YapildiMi { get; set; }


        public bool MTXDuyurusuMu { get; set; }

        public string duyurubasligi { get; set; }



        public string onemderecesi { get; set; }

        



        public string AitFirmaAdi { get; set; }

        [StringLength(200)]
        public string NotIcerigi { get; set; }




    }
}
