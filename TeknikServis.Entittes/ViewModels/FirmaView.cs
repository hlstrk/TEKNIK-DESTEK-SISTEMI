namespace TeknikServis.Entittes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FirmaView")]
    public partial class FirmaView
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FirmaView()
        {
            
        }
        [Key]
        public int FirmaID { get; set; }

    
        public string MusteriTuru { get; set; }



        public string BarkodNo { get; set; }
        public string FirmaAdi { get; set; }





        public string VergiKimlikNo { get; set; }


 
        public string AdSoyad { get; set; }

        public string Adres { get; set; }


        [Phone()]
        public string CepTelefonu { get; set; }

        public string Email { get; set; }



        public bool Aktif { get; set; }

  
        public string FirmaGrubu { get; set; }

        public string FirmaKasaBakiye { get; set; }

        public DateTime? KayitZamani { get; set; }
       

     

        public string Notlar { get; set; }

      





        [Column(TypeName = "image")]
        public byte[] Resim { get; set; }

   

        public int? AktifServis { get; set; }




       




        [StringLength(255)]
        public string FirmaKisaAciklama { get; set; }



        public string IlAdi { get; set; }

        public string IlceAdi { get; set; }


        public string vergidairesi { get; set; }



        public bool TedarikciFirmaMi { get; set; }







      

       






    }
}
