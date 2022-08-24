using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Entittes.PocoModels
{
    public class PocoTeknikListesi
    {
        public int TeknikServisID { get; set; }
        public string TeknikAdi { get; set; }

        public string AdSoyad { get; set; }
        public string Email { get; set; }
        public string TeknikGrubu { get; set; }
        public string Birimi { get; set; }

        public int? KullaniciID { get; set; }

    }
}
