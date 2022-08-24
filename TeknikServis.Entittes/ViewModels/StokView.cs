namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("StokView")]
    public partial class StokView
    {
        [Key]
        public int StokID { get; set; }

        public string BarkodNo { get; set; }

        public string EkleyenKullaniciAdi { get; set; }

        public int EkleyenKullaniciID { get; set; }
        public string Turu { get; set; }
        public string Aciklama { get; set; }

        public string AitOlanFirmaAdi { get; set; }

        public string Kategorisi { get; set; }


        public string Markasi { get; set; }


     
 
        public double? Fiyat { get; set; }


        public string IP { get; set; }


        public int? KacTane { get; set; }

        public string Birim { get; set; }



        public string AnyDesk { get; set; }


   

        public string OS { get; set; }



        public bool DonanimMi { get; set; }

        public string hizmetturu { get; set; }


        public string ParaBirimi { get; set; }
        public int KDVOrani { get; set; }













      


   






    }
}
