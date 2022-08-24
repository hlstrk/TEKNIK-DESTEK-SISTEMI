namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("Teklifler")]
    public partial class Teklif
    {
        [Key]
        public int TeklifID { get; set; }

        public string TicketString { get; set; }    
        public int CariID { get; set; }


        public string KayitZamani { get; set; }
        public DateTime? TeklifTarihi { get; set; }
        

            public int AitOlduguFirmaID { get; set; }
        public int OlusturanKullaniciID { get; set; }
        public double? DolarKuru { get; set; }

      
        public string OlusturanKullanici { get; set; }

        public string YoneticiNotu { get; set; }

        public double Fiyat { get; set; }

        public string OlusturanFirma { get; set; }


        public string FiyaTuru { get; set; }

        public string AitOlduguFirma { get; set; }

        public string Durumu { get; set; }

        
        public string GecerlilikSuresi { get; set; }


       public int OlusturanFirmaID  { get; set; }
        public int KdvOrani { get; set; }

        public double KDVDahilFiyat { get; set; }
        public double KDVFiyat { get; set; }
        public double GenelToplam { get; set; }



        public bool? Aktif { get; set; }


    

     


    





    

    }
}
