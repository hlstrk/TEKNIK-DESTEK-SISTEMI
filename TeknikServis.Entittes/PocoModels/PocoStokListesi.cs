using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Entittes.PocoModels
{
    public class PocoStokListesi
    {
        public int StokID { get; set; }
        public string StokAdi { get; set; }

        public string AdSoyad { get; set; }
        public string Email { get; set; }
        public string StokGrubu { get; set; }
        public string Birimi { get; set; }
    }
}
