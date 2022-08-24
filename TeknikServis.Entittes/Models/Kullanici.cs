namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Kullanici")]
    public partial class Kullanici
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kullanici()
        {
           

        }
        [Key]
        public int KullaniciID { get; set; }

    
        public string KullaniciAdi { get; set; }

    
        public string AdSoyad { get; set; }

  
        public string KullaniciKodu { get; set; }


        public bool? SilinmisMi { get; set; }





        public string KullaniciParola { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy HH\\:mm}")]
        public DateTime? KayitZamani { get; set; }

        public bool Aktif { get; set; }
       
        public string Yetkiler { get; set; }

        public int? IsyeriID { get; set; }
        








        [StringLength(128)]
        
        public string Sifre { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy HH\\:mm}")]
        public DateTime? EnSonGirisZamani { get; set; }

        public string ResimURL { get; set; }

        
        public string CepTelNo { get; set; }

      
    
        public string EMail { get; set; }

       
    
   




        [StringLength(100)]
        public string SifreSifirlaToken { get; set; }





        [StringLength(11)]
        public string TCKimlikNo { get; set; }
        public byte[] ProfilResmi { get; set; }

       

        



       
        public int BolumID { get; set; }
      

        
        public int DepartmanID { get; set; }

        
        public int AltDepartmanID { get; set; }
      
        public int FirmaID { get; set; }
       





        //[NotMapped]//veri tabanýndan hariç tut

        //public string AdiSoyadi
        //{
        //    get { return KullaniciAdi + " " + AdSoyad; }
        //}
    }
}
