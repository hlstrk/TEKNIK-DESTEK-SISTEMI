using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Entittes.PocoModels
{ 
    public class PocoFirmaListesi
    {



        [Key]
        public int FirmaID { get; set; }
        public string FirmaAdi { get; set; }

        public string AdSoyad { get; set; }
        public string Email { get; set; }
 

        public string MusteriTuru { get; set; }

        public bool? Aktif { get; set; }


        [Phone()]
        public string CepTelefonu { get; set; }





        public string VergiKimlikNo { get; set; }
    }
}
