using TeknikServis.Entittes.Models;
using TeknikServis.Entittes.PocoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Interfaces
{
    public interface ITanimService : IGenericService<Tanim>
    {
        List<Tanim> TanimListele(int tanimGrubuId);

        List<PocoTanimListesi> TanimListesi();
        IQueryable TanimListesi(int tanimGrubuId);

        bool TanimSil(int tanimId);
        bool TanimGuncelle(Tanim Tanim);

        List<PocoTanimListesi> TanimListele2(int tanimGrubuId);


    }
}
