namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("TeklifDetay")]
    public partial class TeklifDetay
    {
        [Key]
        public int TeklifDetayID { get; set; }

        public string FiyatTuru { get; set; }

        public string UrunAciklama { get; set; }

        public string AitOlduguFirma { get; set; }

        public string EkleyenKisi { get; set; }

        public int TeklifID { get; set; }

        public string Birim { get; set; }

        public int? Miktar { get; set; }

        public string EklenmeTarihi { get; set; }

        public int EkleyenKisiID { get; set; }  
        public bool Anlasmali { get; set; }



        [DataType(DataType.Currency)]
        public double? Fiyat { get; set; }

        [DataType(DataType.Currency)]
        public double? BirimFiyat { get; set; }


        [DataType(DataType.Currency)]
        public double? ToplamFiyat { get; set; }

        [DataType(DataType.Currency)]
        public double? GenelToplam { get; set; }

        [DataType(DataType.Currency)]
        public double? KDVDahilFiyat { get; set; }

        public double? KdvOrani { get; set; }

        [DataType(DataType.Currency)]
        public double? KDVFiyat { get; set; }








































    }
}
