namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Teknikler")]
    public partial class Teknik
    {
        [Key]
   
        public int TeknikServisID { get; set; }
        public int AitOlduguKullaniciID { get; set; }

  

        public bool mailgonderilsinmi { get; set; }



        public int AitOlduguFirmaID { get; set; }


        public string OdenecekTutar { get; set; }
        public double Fiyat { get; set; }

        public string ParaBirimi { get; set; }



        public bool Anlasmali { get; set; }
        public bool Garanti { get; set; }

        public string CalisanYorumu { get; set; }
        public int DurumID { get; set; }









        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy HH\\:mm}")]
        public DateTime KayitZamani { get; set; }

        public bool? Aktif { get; set; }





        public string MusteriBeyani { get; set; }



        public bool? SilinmisMi { get; set; }

















        public string TicketString { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy HH\\:mm}")]
        public DateTime? KapanisTarihi { get; set; }

        public int ArizaTuruID { get; set; }




        public int BolumID { get; set; }



        public int DepartmanID { get; set; }


        public int AltDepartmanID { get; set; }



        public int AtanmisPersonelID { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy HH\\:mm}")]
        public DateTime? SonIslemZamani { get; set; }






    }
}
