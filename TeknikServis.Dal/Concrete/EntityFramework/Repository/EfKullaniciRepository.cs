using TeknikServis.Dal.Abstract;
using TeknikServis.Entittes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Dal.Concrete.EntityFramework.Repository
{
    public class EfKullaniciRepository : EfGenericRepository<Kullanici>, IKullaniciRepository
    {
        public EfKullaniciRepository():base()
        {

        }
        public KullaniciView KullaniciGiris(string kullaniciAdi, string sifre)
        {
            //select * from Kullanici where kullanicikodu=x and sifre=y
           
            return context.KullaniciView.Where(x => (x.KullaniciAdi == kullaniciAdi && x.KullaniciParola == sifre)|| (x.EMail == kullaniciAdi && x.KullaniciParola == sifre) || (x.KullaniciAdi == kullaniciAdi && x.Sifre == sifre) || (x.EMail == kullaniciAdi && x.Sifre == sifre)).SingleOrDefault();
         
           


        }
    }
}
