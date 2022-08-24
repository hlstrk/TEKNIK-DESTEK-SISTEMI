namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Teknikler")]
    public partial class ServisRaporu
    {
        [Key]
        public int TeknikServisID { get; set; }











      


       

        [Column(TypeName = "date")]
        public DateTime? KayitTarihi { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy hh\\:mm}")]
        public DateTime? KayitZamani { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy hh\\:mm}")]
        public DateTime? SonIslemZamani { get; set; }





      

        [StringLength(2000)]
        public string MusteriBeyani { get; set; }
       

       
       

      

        [StringLength(2000)]
        public string TeknikAdi { get; set; }





        public int? TeknikGrubuID { get; set; }
        public string SonIslem { get; set; }

        public int? FirmaID { get; set; }

        public bool? Aktif { get; set; }
        public bool SilinmisMi { get; set; }
        public bool Garanti { get; set; }

        public string AitOlduguFirma { get; set; }

        public string ArizaTuru { get; set; }

        public bool Anlasmali { get; set; }


        public string OlusturanKullanici { get; set; }



        [DataType(DataType.Currency)]
        public double Fiyat { get; set; }





    }
}
