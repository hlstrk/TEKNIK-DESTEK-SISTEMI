using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Bll
{
    public static class Enums
    {
        public enum CacheKey
        {
           
            Kategoriler,
         
               Personeller,
                Arizalar,
                Durumlar,
            Departmanlar,
            AltDepartmanlar,
           Bolumler,
            KategoriGrup,
            Kullanicilar,
            Firmalar,
            Ekategori,
          il,
          ilce,
          Markalar

                
        }

        public enum TanimGrup
        {


            
            FirmaGrubu = 18,
            Arizalar=50,
            Durumlar=51,
            Departmanlar=52,
            AltDepartmanlar = 53,
            Bolumler = 54,
            Kategoriler =56,
                Ekategori=60,
                il=61,
                ilce=62,
                Markalar=63
               
               
        }
    }
}
