namespace TeknikServis.Entittes.Models
{

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EnvanterView")]
    public partial class EnvanterView
    {
        [Key]
        public int EnvanterID { get; set; }

        public string AitOlanKullaniciAdi { get; set; }

        public string EnvanterTuru { get; set; }

        public string AitOlduguFirmaAdi { get; set; }

        public string Aciklama { get; set; }

        public string OlusturanKullaniciAdi { get; set; }
        
        public int EkleyenKullaniciID { get; set; }


        public string IP { get; set; }
        public string IP2 { get; set; }
        public string WLANIP { get; set; }




        public string MAC { get; set; }

 

        public string MarkaAdi { get; set; }
        public string MAC2 { get; set; }

        public string WLANMAC { get; set; }

        public string AnyDesk { get; set; }


        public string AntiVirüs { get; set; }


     

        public bool Kapsamici { get; set; }

        public bool DonanimMi { get; set; }

        public int KullaniciID { get; set; }


        public string TeamViewer { get; set; }

        public int FirmaID { get; set; }

        public string OS { get; set; }

        public bool? FirmaEnvanterimi { get; set; }

        public string BarkodNo { get; set; }




    }
}
