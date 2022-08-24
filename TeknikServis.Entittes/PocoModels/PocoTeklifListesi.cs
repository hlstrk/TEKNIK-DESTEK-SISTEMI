using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Entittes.PocoModels
{
    public class PocoTeklifListesi
    {
        public int TeklifID { get; set; }
        public string TeklifAdi { get; set; }

        public string AdSoyad { get; set; }
        public string Email { get; set; }
        public string TeklifGrubu { get; set; }
        public string Birimi { get; set; }
    }
}
