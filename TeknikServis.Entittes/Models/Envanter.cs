namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("Envanter")]
    public partial class Envanter
    {
        [Key]
        public int EnvanterID { get; set; }

        public int KullaniciID { get; set; }

   

        public string Aciklama { get; set; }



            public int EkleyenKullaniciID { get; set; }
        public string IP { get; set; }
        public string IP2 { get; set; }
        public string WLANIP { get; set; }




        public string MAC { get; set; }

        public string BarkodNo { get; set; }


        public int EnvanterTuruID { get; set; }



        public string Marka { get; set; }
        public string MAC2 { get; set; }

        public string WLANMAC { get; set; }

        public string AnyDesk { get; set; }


        public string AntiVirüs { get; set; }


        public int FirmaID { get; set; }

        public bool Kapsamici { get; set; }

        public bool DonanimMi { get; set; }
         
        public bool FirmaEnvanterimi { get; set; }


        public int MarkaID { get; set; }    

          public string TeamViewer { get; set; }


        public string OS { get; set; }






    }
}
