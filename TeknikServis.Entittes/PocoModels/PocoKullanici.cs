using TeknikServis.Entittes.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Entittes.PocoModels
{
    [NotMapped]//sınıfı veri tabanından hariç tut.
    public class PocoKullanici/*:Kullanici*/
    {      
        public int KullaniciID { get; set; }

        public bool? Aktif { get; set; }
        public string KullaniciAdi { get; set; }
        public string AdSoyad { get; set; }
       
        public string KullaniciParola { get; set; }
        public string Yetkiler { get; set; }

        public string ResimURL { get; set; }
        public string Departman { get; set; }
        public string AltDepartman { get; set; }

        public string Bolum { get; set; }
        public string Sifre { get; set; }
        public string CalistigiFirma { get; set; }
        public byte[] ProfilResmi { get; set; }
 
        public int? FirmaID { get; set; }

     


    }
}
