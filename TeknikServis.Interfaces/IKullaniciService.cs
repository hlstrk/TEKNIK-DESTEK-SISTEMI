using TeknikServis.Entittes.Models;
using TeknikServis.Entittes.PocoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Interfaces
{
    [ServiceContract]
    public interface IKullaniciService : IGenericService<Kullanici>
    {
        [OperationContract]
        PocoKullanici KullaniciGiris(string kullaniciAdi, string parola);

       


        //Kullanici KullaniciListele(string kullaniciAdi, string parola);

    }
}
