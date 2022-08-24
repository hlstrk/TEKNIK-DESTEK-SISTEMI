using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Entittes.PocoModels
{
    public class PocoEnvanterListesi
    {
        public int EnvanterID { get; set; }
        public string KullaniciID { get; set; }
 
        public string Turu { get; set; }
        public string Firma { get; set; }
        public string Aciklama { get; set; }
        public string EkleyenKullanici { get; set; }

        public bool Kapsamici { get; set; }

        public bool DonanimMi { get; set; }

        public string IP { get; set; }
        public string MAC { get; set; }
        public string AnyDesk  { get; set; }

        public string TeamViewer { get; set; }

        public string OS { get; set; }

    }
}
