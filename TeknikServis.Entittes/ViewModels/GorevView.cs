namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("GorevView")]
    public partial class GorevView
    {
        public GorevView()
        {


        }


        [Key]
        public int GorevID { get; set; }

        public string AitKullaniciAdi { get; set; }

        public int AitOlanKullaniciID { get; set; }


        public string OlusturanKullaniciAdi { get; set; }

        public int OlusturanKullaniciID { get; set; }

        public string AitFirmaAdi { get; set; }

        public int? FirmaID { get; set; }   
        

        public int? TeknikServisID { get; set; }


        public string GorevIcerigi { get; set; }


        public string Konu { get; set; }

        public bool tamamlandimi { get; set; }

        public bool toplugorevmi { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy HH\\:mm}")]
        public DateTime? AtamaTarihi { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy HH\\:mm}")]
        public DateTime? BitirmeTarihi { get; set; }




        public bool silinmismi { get; set; }


        [NotMapped]

        public byte[] KullaniciResim { get; set; }




    }
}
