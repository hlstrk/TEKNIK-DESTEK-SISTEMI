using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Entittes.PocoModels
{
    public class PocoTeklifDetayListesi
    {
        public int TeklifDetayID { get; set; }
        public int TeklifID { get; set; }
        public string TeklifDetayAdi { get; set; }

        public string AdSoyad { get; set; }
        public string Email { get; set; }
        public string TeklifDetayGrubu { get; set; }
        public string Birimi { get; set; }
    }
}
