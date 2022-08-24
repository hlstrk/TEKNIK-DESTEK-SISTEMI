using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Entittes.PocoModels
{
    public class PocoBildirimListesi
    {

      
        public int bildirimID { get; set; }



        public string bildirimIcerigi { get; set; }
        public string olusturanFirma { get; set; }

        public string arizaTuru { get; set; }
        public string olusturanKullanici { get; set; }
        public string musteriBeyani { get; set; }
        public int sayac { get; set; }



    }
}
