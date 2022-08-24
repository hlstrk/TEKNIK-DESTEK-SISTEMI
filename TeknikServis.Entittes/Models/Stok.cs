namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("Stoklar")]
    public partial class Stok
    {
        [Key]
        public int StokID { get; set; }

        public string BarkodNo { get; set; }



        public int EkleyenKullaniciID { get; set; }
   
        public string Aciklama { get; set; }


        public int FirmaID { get; set; }

        public double AlisFiyati { get; set; }


        public string IP { get; set; }

        public int StokKategoriID { get; set; }

        public int StokMarkaID { get; set; }

        public int StokTuruID { get; set; }


        public string Birim { get; set; }


        public int? KacTane { get; set; }

        public string MAC { get; set; }

        public string AnyDesk { get; set; }


        public string TeamViewer { get; set; }

        public string OS { get; set; }


        public string VarsaGarantiSuresi { get; set; }

        public bool DonanimMi { get; set; }

        public string Marka { get; set; }

        public double? Fiyat { get; set; }

        public int KDVOrani { get; set; }



        public string hizmetturu { get; set; }


        public string ParaBirimi { get; set; }

        public string AntiVirüs { get; set; }






      


   






    }
}
