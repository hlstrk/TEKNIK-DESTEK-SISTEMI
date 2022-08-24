using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Entittes.PocoModels
{
    public class PocoTanimListesi
    {
        public int TanimID { get; set; }

        public int TanimGrupID { get; set; }

        [StringLength(50)]
        public string TanimAdi { get; set; }

        public bool? Aktif { get; set; }

        public int? Sira { get; set; }

        public int? KullaniciID { get; set; }

        public DateTime? KayitZamani { get; set; }

        public int? IsyeriID { get; set; }

        public long? N11Kodu { get; set; }

        public int? UstGrupID { get; set; }

        [StringLength(10)]
        public string Kodu { get; set; }

        public double AlisDegeri { get; set; }

        public double SatisDegeri { get; set; }

        [StringLength(10)]
        public string Sembol { get; set; }

        public bool DepoOtoKartAcilsin { get; set; }

        public bool DepoETicaretToplaGonder { get; set; }


    }
}
