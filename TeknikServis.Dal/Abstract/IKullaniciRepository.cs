using TeknikServis.Entittes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Dal.Abstract
{
    public interface IKullaniciRepository:IGenericRepository<Kullanici>
    {
        KullaniciView KullaniciGiris(string kullaniciAdi, string sifre);
    }
}
