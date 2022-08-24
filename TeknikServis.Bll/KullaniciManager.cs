using TeknikServis.Dal.Abstract;
using TeknikServis.Entittes.Models;
using TeknikServis.Entittes.PocoModels;
using TeknikServis.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Bll
{
    public class KullaniciManager : GenericManager<Kullanici>, IKullaniciService
    {
        IKullaniciRepository kullaniciRepository;

        public KullaniciManager(IKullaniciRepository kullaniciRepository) : base(kullaniciRepository)
        {
            this.kullaniciRepository = kullaniciRepository;
        }

        public PocoKullanici KullaniciGiris(string kullaniciAdi, string parola)
        {

            if (string.IsNullOrEmpty(kullaniciAdi.Trim()))
            {
                throw new Exception("Kullanıcı Adı Boş Geçilemez.");
            }

            if (string.IsNullOrEmpty(parola.Trim()))
            {
                throw new Exception("Parola Boş Geçilemez.");
            }
            


            var sifre = new ToPasswordRepository().Md5(parola);
            var kullanici = kullaniciRepository.KullaniciGiris(kullaniciAdi, sifre);

            if (kullanici == null)
            {
                throw new Exception("Kullanıcı Adınızı veya Parolanızı Kontrol Ediniz.");
               
            }
            else
            {
                return new PocoKullanici()
                {
                    KullaniciID = kullanici.KullaniciID,
                    KullaniciAdi = kullanici.KullaniciAdi,
                    AdSoyad = kullanici.AdSoyad,
                    CalistigiFirma = kullanici.CalistigiFirma,
                    Yetkiler = kullanici.Yetkiler,
          
                    Aktif=kullanici.Aktif,
                    Departman=kullanici.Departman,
                    ProfilResmi=kullanici.ProfilResmi,
                    KullaniciParola = kullanici.KullaniciParola


                };
            }
        }
    }
}
