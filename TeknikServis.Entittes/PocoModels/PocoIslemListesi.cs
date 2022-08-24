namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
 
    public partial class PocoIslemListesi
    {
        [Key]
        public int IslemServisID { get; set; }

      
     

    

        public int TeknikServisID { get; set; }

     

        public int? Miktar { get; set; }

       

       public string EkleyenKisi { get; set; }




        
        public string KayitZamani { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy HH\\:mm}")]
        public DateTime? SonIslemZamani { get; set; }

        
      

      


        public string CalisanYorumu { get; set; }

        public string MusteriTuru { get; set; }

      

        [StringLength(2000)]
        public string IslemAdi { get; set; }


        public int? IslemBirimID { get; set; }


        public int? IslemGrubuID { get; set; }
      
       

        public bool? Aktif { get; set; }
       

        public string AitOlduguFirma { get; set; }

        public string ArizaTuru { get; set; }

        public bool Anlasmali { get; set; }

        public string YapilanIslem { get; set; }


     


        public double? Fiyat { get; set; }

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
