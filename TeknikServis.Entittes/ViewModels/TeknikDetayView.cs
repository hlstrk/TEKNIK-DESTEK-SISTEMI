namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("IslemView")]
    public partial class TeknikDetayView
    {
        [Key]
        public int IslemServisID { get; set; }
        public int KullaniciID { get; set; }

        public int FirmaID { get; set; }


        public string EkleyenKisi { get; set; }

        


        public int TeknikServisID { get; set; }

     

        public int? Miktar { get; set; }

        

        public string BarkodNo { get; set; }




        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy HH\\:mm}")]
        public DateTime? KayitZamani { get; set; }




        

      





     

       

        public bool? Aktif { get; set; }
       

        public string AitOlduguFirma { get; set; }



        public bool Anlasmali { get; set; }

        public string YapilanIslem { get; set; }


        public bool? Kapsamici { get; set; }



        public string IslemTuru { get; set; }


        public double? BirimFiyat { get; set; }



        public double? GenelToplam { get; set; }

   
        public double? KDVDahilFiyat { get; set; }
        
        public double? KdvOrani { get; set; }
        
     
        public double? KDVFiyat { get; set; }
       

        public string ParaBirimi { get; set; }

        public double? TLFiyat { get; set; }


        public double? DolarKuru { get; set; }
    }
}
